#!/bin/bash
AZCOPY_CRED_TYPE="Anonymous";
AZCOPY_DEFAULT_SERVICE_API_VERSION="2019-07-07";
AZCOPY_CONCURRENCY_VALUE="AUTO";
azcopy copy "ServerlessTodoUi/js" "http://azurite:10000/devstoreaccount1/devstoreaccount1/?sv=2018-03-28&se=2025-04-01T07%3A49%3A44Z&sr=c&sp=rwl&sig=yttqTqhB%2BU%2FJjHiUQs2M3y6zJ3wTHpdS3COYeVulA%2Bk%3D" --overwrite=true --from-to=LocalBlob --blob-type Detect --follow-symlinks --check-length=true --put-md5 --follow-symlinks --disable-auto-decoding=false --recursive --log-level=INFO;
azcopy copy "ServerlessTodoUi/index.html" "http://azurite:10000/devstoreaccount1/devstoreaccount1/?sv=2018-03-28&se=2025-04-01T07%3A49%3A44Z&sr=c&sp=rwl&sig=yttqTqhB%2BU%2FJjHiUQs2M3y6zJ3wTHpdS3COYeVulA%2Bk%3D" --overwrite=true --from-to=LocalBlob --blob-type Detect --follow-symlinks --check-length=true --put-md5 --follow-symlinks --disable-auto-decoding=false --log-level=INFO;