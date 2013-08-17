import json,httplib
connection = httplib.HTTPSConnection('api.parse.com', 443)
connection.connect()
connection.request('POST', '/1/classes/PlayerProfile', json.dumps({
       "name": "Andrew",
       "picture": {
         "name": "...images/1649.png",
         "__type": "File"
       }
     }), {
       "X-Parse-Application-Id": "XrxZBmLNdt5CYCNzQbUSl7gYktlYYrpHFGVxm3fw",
       "X-Parse-REST-API-Key": "5ZEYqnPrnEpMkUiJTeFMZKWmTAoIUMic1OH5oCnj",
       "Content-Type": "application/json"
     })
result = json.loads(connection.getresponse().read())
print result