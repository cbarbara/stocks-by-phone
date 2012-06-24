stocks-by-phone
===============

a sample app combining the Xignite and twilio API to delivery stock prices by SMS or through a phone call.

To get started, open up Web.Config and replace the "xigniteToken" value with your own xignite authentication token.
Build and deploy to your server.

Create a new twilio app and configure the following:
 * the Voice URL should make a GET call to the PhoneCallMenu.xml file.
 * the SMS URL should make a POST call to the ReplyToSms.aspx file.
