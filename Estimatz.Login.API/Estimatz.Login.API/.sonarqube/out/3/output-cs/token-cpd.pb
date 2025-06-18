á
zG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Infrastructure\Estimatz.Login.API.Services\EmailService\IEmailService.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Infra "
." #
Services# +
.+ ,
EmailService, 8
{ 
public 

	interface 
IEmailService "
{ 
void 
	SendEmail 
( 
EmailMessage #
emailContent$ 0
)0 1
;1 2
} 
}		 Â
€G:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Infrastructure\Estimatz.Login.API.Services\EmailService\MailKitEmailService.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Infra "
." #
Services# +
.+ ,
EmailService, 8
{ 
public 

class 
MailKitEmailService $
:% &
IEmailService' 4
{		 
public

 
void

 
	SendEmail

 
(

 
EmailMessage

 *
emailContent

+ 7
)

7 8
{ 	
var 

smtpClient 
= 
new  

SmtpClient! +
(+ ,
), -
;- .

smtpClient 
. 
Connect 
( 
$str /
,/ 0
$num1 4
,4 5
SecureSocketOptions6 I
.I J
StartTlsJ R
)R S
;S T

smtpClient 
. 
Authenticate #
(# $
$str$ A
,A B
$strC U
)U V
;V W
var 
message 
= 
new 
MimeMessage )
() *
)* +
;+ ,
message 
. 
From 
. 
Add 
( 
new  
MailboxAddress! /
(/ 0
$str0 C
,C D
$strE b
)b c
)c d
;d e
message 
. 
To 
. 
Add 
( 
emailContent '
.' (
	Recipient( 1
)1 2
;2 3
message 
. 
Subject 
= 
emailContent *
.* +
Subject+ 2
;2 3
message 
. 
Body 
= 
emailContent '
.' (
Body( ,
;, -

smtpClient 
. 
Send 
( 
message #
)# $
;$ %

smtpClient 
. 

Disconnect !
(! "
true" &
)& '
;' (
} 	
} 
} 