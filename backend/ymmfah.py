import os
import uuid
import base64
import json
from datetime import datetime, timedelta

import boto3

from flask import Flask, request, send_from_directory, redirect
from werkzeug.utils import secure_filename


S3_BUCKET                 = os.environ.get("S3_BUCKET_NAME")
S3_KEY                    = os.environ.get("S3_ACCESS_KEY")
S3_SECRET                 = os.environ.get("S3_SECRET_ACCESS_KEY")
S3_LOCATION               = 'https://{}.s3.amazonaws.com/letters/'.format(S3_BUCKET)

BASE_URL = "https://ymmfah.poe.gg/index.html"

s3 = boto3.client(
   "s3",
   aws_access_key_id=S3_KEY,
   aws_secret_access_key=S3_SECRET
)

app = Flask(__name__)


@app.route('/')
def hello():
    return send_from_directory('./static', 'upload.html')


@app.route('/<path:path>', methods=['GET'])
def get_html(path):
    return send_from_directory('./static', path)



@app.route('/compose', methods=['POST'])
def compose():
    letter_id = base64.urlsafe_b64encode(uuid.uuid4().bytes).decode('utf8').strip('=')

    # build / upload letter json
    letter = {
        'SenderName': request.form['SenderName'],
        'RecipientName': request.form['RecipientName'],
        'Pages': [compose_page(i, letter_id) for i in range(1,7)]
    }
    letter_json = json.dumps(letter)
    upload_json_to_s3(letter_json, 'letter.json', S3_BUCKET, letter_id)

    # upload images
    for filename, file in request.files.items():
        if filename != secure_filename(filename):
            print("Invalid filename")
            raise ValueError("Invalid filename")
        upload_file_to_s3(file, S3_BUCKET, 'letters/{0}/{1}'.format(letter_id, filename))

    return '''
    <html><body style="position:relative; margin:0; height:100vh;"><p style="position:absolute; width:100%; text-align:center; top:50%; transform: translateY(-50%); font-size:20pt;">Send this link to your loved one:<br> <a href="{1}#{0}">{1}#{0}</a></p></body></html>
    '''.format(letter_id, BASE_URL)


def compose_page(i, letter_id):
    return {
        'Layout': request.form['Page{0}_layout'.format(i)],
        'Text': request.form['Page{0}_text'.format(i)],
        'ImageUrl': 'https://{0}.s3.amazonaws.com/letters/{1}/Page{2}_image'.format(S3_BUCKET, letter_id, i)
    }


def upload_json_to_s3(letter_json, filename, bucket_name, letter_id, acl='public-read'):
    try:
        filename = 'letters/{0}/{1}'.format(letter_id, filename)
        s3.put_object(ACL=acl, Body=letter_json.encode('utf8'), Bucket=bucket_name, Key=filename, Expires=datetime.now() + timedelta(days=7))

    except Exception as e:
        print("S3 Upload failed", e)
        raise


def upload_file_to_s3(file, bucket_name, filename, acl="public-read"):
    try:
        s3.upload_fileobj(
            file,
            bucket_name,
            filename,
            ExtraArgs={
                "ACL": acl,
                "ContentType": file.content_type,
                "Expires": datetime.now() + timedelta(days=7)
            }
        )

    except Exception as e:
        # This is a catch all exception, edit this part to fit your needs.
        print("S3 Upload failed: ", e)
        raise

