# Twilio.CLR

SQL CLR for calling the [Twilio REST API](http://www.twilio.com/docs/api/rest/)

## Available Methods

Currently it has a single method for sending SMS messages. Please help me implement the other API methods.

To use any of these functions first compile this CLR, deploy it to your SQL server, and execute the procedure you want to run

## Sending a message

	declare @sid varchar(34)
	exec SendSMSMessage 'api_version', 'account_id', 'auth_token', 'from_number', 'to_number', 'message_body', @sid output
	print(@sid)
	
## Make an outgoing call

	declare @sid varchar(34)
	exec MakeOutgoingCall 'api_version', 'account_id', 'auth_token', 'from_number', 'to_number', 'url', @sid output
	print(@sid)