import os
import uuid
import base64
import json

import boto3

from flask import Flask, request, send_from_directory, redirect
from werkzeug.utils import secure_filename


S3_BUCKET                 = os.environ.get("S3_BUCKET_NAME")
S3_KEY                    = os.environ.get("S3_ACCESS_KEY")
S3_SECRET                 = os.environ.get("S3_SECRET_ACCESS_KEY")
S3_LOCATION               = 'http://{}.s3.amazonaws.com/'.format(S3_BUCKET)

s3 = boto3.client(
   "s3",
   aws_access_key_id=S3_KEY,
   aws_secret_access_key=S3_SECRET
)

app = Flask(__name__)


@app.route('/')
def hello():
    return send_from_directory('./static', 'upload.html')


@app.route('/<path>', methods=['GET'])
def get_html(path):
    return send_from_directory('./static', path)


@app.route('/letter', methods=['POST'])
def upload():
    letter_id = base64.urlsafe_b64encode(uuid.uuid4().bytes).decode('utf8').strip('=')
    for filename, file in request.files.items():
        if file.filename == '':
            print("File has no filename!")
            raise ValueError("no filename")
        if file.filename != secure_filename(file.filename):
            print("Invalid filename")
            raise ValueError("Invalid filename")
        upload_file_to_s3(file, S3_BUCKET, letter_id)
    return letter_id


@app.route('/compose', methods=['POST'])
def compose():
    letter_id = base64.urlsafe_b64encode(uuid.uuid4().bytes).decode('utf8').strip('=')

    # build / upload letter json
    letter = {
        'SenderName': request.form['SenderName'],
        'RecipientName': request.form['RecipientName'],
        'Pages': [compose_page(i) for i in range(1,7)]
    }
    letter_json = json.dumps(letter)
    upload_json_to_s3(letter_json, 'letter.json', S3_BUCKET, letter_id)

    # upload images
    for filename, file in request.files.items():
        if filename != secure_filename(filename):
            print("Invalid filename")
            raise ValueError("Invalid filename")
        upload_file_to_s3(file, S3_BUCKET, letter_id + '/' + filename)

    return '''
    <html><body><p>Send this link to your loved one: <a href="https://s3.amazonaws.com/ymmfah/static/index.html#{0}">https://s3.amazonaws.com/ymmfah/static/index.html#{0}</a></p></body></html>
    '''.format(letter_id)


def compose_page(i):
    return {
        'Layout': request.form['Page{0}_layout'.format(i)],
        'Text': request.form['Page{0}_text'.format(i)],
        'Image': 'Page{0}_image'.format(i)
    }


def upload_json_to_s3(letter_json, filename, bucket_name, letter_id, acl='public-read'):
    try:
        filename = letter_id + '/' + filename
        s3.put_object(ACL=acl, Body=letter_json.encode('utf8'), Bucket=bucket_name, Key=filename)

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
                "ContentType": file.content_type
            }
        )

    except Exception as e:
        # This is a catch all exception, edit this part to fit your needs.
        print("S3 Upload failed: ", e)
        raise

