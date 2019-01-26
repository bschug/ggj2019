import os
import uuid
import base64

import boto3

from flask import Flask, request, send_from_directory
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
    return "HELLO"


@app.route('/test', methods=['GET'])
def get_html():
    return send_from_directory('.', 'test.html')


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


def upload_file_to_s3(file, bucket_name, letter_id, acl="public-read"):
    try:
        filename = letter_id + '/' + file.filename

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
        return e

