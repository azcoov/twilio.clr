# Twilio.CLR

SQL CLR for calling the [Twilio REST API](http://www.twilio.com/docs/api/rest/)

## Available Methods

Currently it has a single method for sending SMS messages. Please help me implement the other API methods.

## Sending a message

Compile this CLR, deploy it to your SQL server, and execute the procedure:

	declare @sid varchar()
	exec SendSMSMessage 'api_version', 'account_id', 'auth_token', 'from_number', 'to_number', 'message_body', @sid output
	print(@sid)

