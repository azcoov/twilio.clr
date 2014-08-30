# Twilio.CLR

SQL CLR for calling the [Twilio REST API](http://www.twilio.com/docs/api/rest/)

## Available Methods

To use any of these functions first compile this CLR, deploy it to your SQL server, and execute the procedure you want to run.
You will need the System.Web assembly available on your SQL Server to deploy it.

## Sending a message

	declare @sid varchar(34)
	exec SendSMSMessage 'api_version', 'account_id', 'auth_token', 'from_number', 'to_number', 'message_body', @sid output
	print(@sid)
	
## Make an outgoing call

	declare @sid varchar(34)
	exec MakeOutgoingCall 'api_version', 'account_id', 'auth_token', 'from_number', 'to_number', 'url', @sid output
	print(@sid)