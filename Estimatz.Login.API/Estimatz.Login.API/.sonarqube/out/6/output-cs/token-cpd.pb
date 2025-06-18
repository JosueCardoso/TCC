ó

ëG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Events\NewUserConfirmationEmail\NewUserConfirmationEmailEvent.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Events #
.# $$
NewUserConfirmationEmail$ <
{ 
public 

class )
NewUserConfirmationEmailEvent .
:/ 0
INotification1 >
{ 
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Username 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 
string		 
UserId		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public

 
string

 
Token

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
public 
string 
ApplicationURL $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} ﬂ$
òG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Events\NewUserConfirmationEmail\NewUserConfirmationEmailEventHandler.cs
	namespace		 	
Estimatz		
 
.		 
Login		 
.		 
API		 
.		 
Events		 #
.		# $$
NewUserConfirmationEmail		$ <
{

 
public 

class 0
$NewUserConfirmationEmailEventHandler 5
:6 7 
INotificationHandler8 L
<L M)
NewUserConfirmationEmailEventM j
>j k
{ 
private 
readonly 
IEmailService &
_emailService' 4
;4 5
private 
readonly 
ILogger  
<  !0
$NewUserConfirmationEmailEventHandler! E
>E F
_loggerG N
;N O
public 0
$NewUserConfirmationEmailEventHandler 3
(3 4
IEmailService4 A
emailServiceB N
,N O
ILoggerP W
<W X0
$NewUserConfirmationEmailEventHandlerX |
>| }
logger	~ Ñ
)
Ñ Ö
{ 	
_emailService 
= 
emailService (
;( )
_logger 
= 
logger 
; 
} 	
public 
Task 
Handle 
( )
NewUserConfirmationEmailEvent 8
notification9 E
,E F
CancellationTokenG X
cancellationTokenY j
)j k
{ 	
var 

urlDefault 
= 
$str R
;R S
var 
url 
= 
string 
. 
IsNullOrEmpty *
(* +
notification+ 7
.7 8
ApplicationURL8 F
)F G
?H I

urlDefaultJ T
:U V
notificationW c
.c d
ApplicationURLd r
;r s
_emailService 
. 
	SendEmail #
(# $
new$ '
EmailMessage( 4
{ 
Subject 
= 
$str 8
,8 9
	Recipient 
= 
new 
MailboxAddress  .
(. /
notification/ ;
.; <
Username< D
,D E
notificationF R
.R S
EmailS X
)X Y
,Y Z
Body 
= 
new 
TextPart #
(# $
$str$ *
)* +
{   
Text!! 
=!! 
CreateEmailBody!! *
(!!* +
notification!!+ 7
.!!7 8
Username!!8 @
,!!@ A!
CreateConfirmationUrl!!B W
(!!W X
notification!!X d
.!!d e
UserId!!e k
,!!k l
notification!!m y
.!!y z
Token!!z 
,	!! Ä
url
!!Å Ñ
)
!!Ñ Ö
)
!!Ö Ü
}"" 
}## 
)## 
;## 
_logger%% 
.%% 
LogInformation%% "
(%%" #
$"%%# %
$str%%% ]
{%%] ^
notification%%^ j
.%%j k
Email%%k p
}%%p q
"%%q r
)%%r s
;%%s t
return&& 
Task&& 
.&& 
CompletedTask&& %
;&&% &
}'' 	
private)) 
string)) !
CreateConfirmationUrl)) ,
()), -
string))- 3
userId))4 :
,)): ;
string))< B
token))C H
,))H I
string))J P
url))Q T
)))T U
{** 	
return++ 
$"++ 
{++ 
url++ 
}++ 
$str++ "
{++" #
userId++# )
}++) *
$str++* 1
{++1 2

WebUtility++2 <
.++< =
	UrlEncode++= F
(++F G
token++G L
)++L M
}++M N
"++N O
;++O P
},, 	
private.. 
string.. 
CreateEmailBody.. &
(..& '
string..' -
username... 6
,..6 7
string..8 >
link..? C
)..C D
{// 	
return00 
@$"00 
$str03 J
{33J K
username33K S
}33S T
$str35T A
{55A B
HtmlEncoder55B M
.55M N
Default55N U
.55U V
Encode55V \
(55\ ]
link55] a
)55a b
}55b c
$str58c 
"88 
;88 
}99 	
}:: 
};; á

âG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Events\RecoverPasswordEmail\RecoverPasswordEmailEvent.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Events #
.# $ 
RecoverPasswordEmail$ 8
{ 
public 

class %
RecoverPasswordEmailEvent *
:+ ,
INotification- :
{ 
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
public 
string 
Username 
{  
get! $
;$ %
set& )
;) *
}+ ,
public		 
string		 
UserId		 
{		 
get		 "
;		" #
set		$ '
;		' (
}		) *
public

 
string

 
Token

 
{

 
get

 !
;

! "
set

# &
;

& '
}

( )
public 
string 
ApplicationURL $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} û%
êG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Events\RecoverPasswordEmail\RecoverPasswordEmailEventHandler.cs
	namespace		 	
Estimatz		
 
.		 
Login		 
.		 
API		 
.		 
Events		 #
.		# $ 
RecoverPasswordEmail		$ 8
{

 
public 

class ,
 RecoverPasswordEmailEventHandler 1
:2 3 
INotificationHandler4 H
<H I%
RecoverPasswordEmailEventI b
>b c
{ 
private 
readonly 
IEmailService &
_emailService' 4
;4 5
private 
readonly 
ILogger  
<  !,
 RecoverPasswordEmailEventHandler! A
>A B
_loggerC J
;J K
public ,
 RecoverPasswordEmailEventHandler /
(/ 0
IEmailService0 =
emailService> J
,J K
ILoggerL S
<S T,
 RecoverPasswordEmailEventHandlerT t
>t u
loggerv |
)| }
{ 	
_emailService 
= 
emailService (
;( )
_logger 
= 
logger 
; 
} 	
public 
Task 
Handle 
( %
RecoverPasswordEmailEvent 4
notification5 A
,A B
CancellationTokenC T
cancellationTokenU f
)f g
{ 	
var 

urlDefault 
= 
$str ]
;] ^
var 
url 
= 
string 
. 
IsNullOrEmpty *
(* +
notification+ 7
.7 8
ApplicationURL8 F
)F G
?H I

urlDefaultJ T
:U V
notificationW c
.c d
ApplicationURLd r
;r s
_emailService 
. 
	SendEmail #
(# $
new$ '
EmailMessage( 4
{ 
Subject 
= 
$str ;
,; <
	Recipient 
= 
new 
MailboxAddress  .
(. /
notification/ ;
.; <
Username< D
,D E
notificationF R
.R S
EmailS X
)X Y
,Y Z
Body 
= 
new 
TextPart #
(# $
$str$ *
)* +
{   
Text!! 
=!! 
CreateEmailBody!! *
(!!* +
notification!!+ 7
.!!7 8
Username!!8 @
,!!@ A!
CreateConfirmationUrl!!B W
(!!W X
notification!!X d
.!!d e
UserId!!e k
,!!k l
notification!!m y
.!!y z
Token!!z 
,	!! Ä
url
!!Å Ñ
)
!!Ñ Ö
)
!!Ö Ü
}"" 
}## 
)## 
;## 
_logger%% 
.%% 
LogInformation%% "
(%%" #
$"%%# %
$str%%% \
{%%\ ]
notification%%] i
.%%i j
Email%%j o
}%%o p
"%%p q
)%%q r
;%%r s
return&& 
Task&& 
.&& 
CompletedTask&& %
;&&% &
}'' 	
private)) 
string)) !
CreateConfirmationUrl)) ,
()), -
string))- 3
userId))4 :
,)): ;
string))< B
token))C H
,))H I
string))J P
url))Q T
)))T U
{** 	
return++ 
$"++ 
{++ 
url++ 
}++ 
$str++ "
{++" #
userId++# )
}++) *
$str++* 1
{++1 2

WebUtility++2 <
.++< =
	UrlEncode++= F
(++F G
token++G L
)++L M
}++M N
"++N O
;++O P
},, 	
private.. 
string.. 
CreateEmailBody.. &
(..& '
string..' -
username... 6
,..6 7
string..8 >
link..? C
)..C D
{// 	
return00 
@$"00 
$str03 <
{33< =
username33= E
}33E F
$str34F F
{44F G
DateTime44H P
.44P Q
Now44Q T
}44U V
$str46V A
{66A B
HtmlEncoder66B M
.66M N
Default66N U
.66U V
Encode66V \
(66\ ]
link66] a
)66a b
}66b c
$str69c 
"99 
;99 
}:: 	
};; 
}<< 