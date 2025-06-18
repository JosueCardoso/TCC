’
}G:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Commands\ConfirmEmail\ConfirmEmailCommand.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Commands %
.% &
ConfirmEmail& 2
{ 
public 

class 
ConfirmEmailCommand $
:% &
IRequest' /
{ 
public 
string 
UserId 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Token 
{ 
get !
;! "
set# &
;& '
}( )
}		 
}

 ™4
„G:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Commands\ConfirmEmail\ConfirmEmailCommandHandler.cs
	namespace		 	
Estimatz		
 
.		 
Login		 
.		 
API		 
.		 
Commands		 %
.		% &
ConfirmEmail		& 2
{

 
public 

class &
ConfirmEmailCommandHandler +
:, -
IRequestHandler. =
<= >
ConfirmEmailCommand> Q
>Q R
{ 
private 
readonly 
UserManager $
<$ %
ApplicationUser% 4
>4 5
_userManager6 B
;B C
private 
readonly 
ITokenManager &
_tokenManager' 4
;4 5
private 
readonly 
INotificator % 
_notificationService& :
;: ;
private 
readonly 
ILogger  
<  !&
ConfirmEmailCommandHandler! ;
>; <
_logger= D
;D E
public &
ConfirmEmailCommandHandler )
() *
UserManager* 5
<5 6
ApplicationUser6 E
>E F
userManagerG R
,R S
ITokenManagerT a
tokenManagerb n
,n o
INotificatorp |
notificator	} ˆ
,
ˆ ‰
ILogger
Š ‘
<
‘ ’(
ConfirmEmailCommandHandler
’ ¬
>
¬ ­
logger
® ´
)
´ µ
{ 	
_userManager 
= 
userManager &
;& '
_tokenManager 
= 
tokenManager (
;( ) 
_notificationService  
=! "
notificator# .
;. /
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
Handle  
(  !
ConfirmEmailCommand! 4
request5 <
,< =
CancellationToken> O
cancellationTokenP a
)a b
{ 	
Notification 
notification %
;% &
IdentityResult 
? 
result "
=# $
null% )
;) *
if 
( 
! 
_tokenManager 
. 
IsValidToken +
(+ ,
request, 3
.3 4
Token4 9
)9 :
): ;
{    
_notificationService!! $
.!!$ %
Notify!!% +
(!!+ ,
new!!, /
(!!/ 0
success!!0 7
:!!7 8
false!!9 >
,!!> ?
new!!@ C
(!!C D
$str!!D T
)!!T U
)!!U V
)!!V W
;!!W X
_logger"" 
."" 
LogError""  
(""  !
$str""! S
)""S T
;""T U
return## 
;## 
}$$ 
var&& 
user&& 
=&& 
await&& 
_userManager&& )
.&&) *
FindByIdAsync&&* 7
(&&7 8
request&&8 ?
.&&? @
UserId&&@ F
)&&F G
;&&G H
if(( 
((( 
user(( 
is(( 
not(( 
null((  
)((  !
result)) 
=)) 
await)) 
_userManager)) +
.))+ ,
ConfirmEmailAsync)), =
())= >
user))> B
,))B C
request))D K
.))K L
Token))L Q
)))Q R
;))R S
if++ 
(++ 
result++ 
is++ 
not++ 
null++ "
&&++# %
result++& ,
.++, -
	Succeeded++- 6
)++6 7
{,, 
_tokenManager-- 
.-- 
InvalidToken-- *
(--* +
request--+ 2
.--2 3
Token--3 8
)--8 9
;--9 :
notification.. 
=.. 
new.. "
Notification..# /
(../ 0
success..0 7
:..7 8
true..9 =
,..= >
new..? B
(..B C
$str..C b
)..b c
)..c d
;..d e
_logger// 
.// 
LogInformation// &
(//& '
$"//' )
$str//) 0
{//0 1
user//1 5
?//5 6
.//6 7
Email//7 <
}//< =
$str//= U
"//U V
)//V W
;//W X
}00 
else11 
if11 
(11 
result11 
is11 
null11 #
)11# $
{22 
notification33 
=33 
new33 "
Notification33# /
(33/ 0
success330 7
:337 8
false339 >
,33> ?
new33@ C
(33C D
$str33D i
)33i j
)33j k
;33k l
_logger44 
.44 

LogWarning44 "
(44" #
$"44# %
$str44% f
"44f g
)44g h
;44h i
}55 
else66 
{77 
notification88 
=88 
new88 "
Notification88# /
(88/ 0
success880 7
:887 8
false889 >
)88> ?
;88? @
_logger99 
.99 

LogWarning99 "
(99" #
$"99# %
$str99% O
{99O P
user99P T
?99T U
.99U V
Email99V [
}99[ \
$str99\ ]
"99] ^
)99^ _
;99_ `
foreach;; 
(;; 
var;; 
error;; !
in;;" $
result;;% +
.;;+ ,
Errors;;, 2
);;2 3
{<< 
notification==  
.==  !

AddMessage==! +
(==+ ,
new==, /
Message==0 7
(==7 8
error==8 =
.=== >
Code==> B
,==B C
error==D I
.==I J
Description==J U
)==U V
)==V W
;==W X
_logger>> 
.>> 

LogWarning>> &
(>>& '
$">>' )
$str>>) /
{>>/ 0
error>>0 5
.>>5 6
Code>>6 :
}>>: ;
$str>>; K
{>>K L
error>>L Q
.>>Q R
Description>>R ]
}>>] ^
">>^ _
)>>_ `
;>>` a
}?? 
}@@  
_notificationServiceBB  
.BB  !
NotifyBB! '
(BB' (
notificationBB( 4
)BB4 5
;BB5 6
}CC 	
}DD 
}EE ú
‘G:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Commands\ConfirmRecoverPassword\ConfirmRecoverPasswordCommand.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Commands %
.% &"
ConfirmRecoverPassword& <
{ 
public 

class )
ConfirmRecoverPasswordCommand .
:/ 0
IRequest1 9
{ 
public 
string 
UserId 
{ 
get "
;" #
set$ '
;' (
}) *
public 
string 
Token 
{ 
get !
;! "
set# &
;& '
}( )
public		 
string		 
Password		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
public

 
string

 
ConfirmPassword

 %
{

& '
get

( +
;

+ ,
set

- 0
;

0 1
}

2 3
} 
} ñ6
˜G:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Commands\ConfirmRecoverPassword\ConfirmRecoverPasswordCommandHandler.cs
	namespace		 	
Estimatz		
 
.		 
Login		 
.		 
API		 
.		 
Commands		 %
.		% &"
ConfirmRecoverPassword		& <
{

 
public 

class 0
$ConfirmRecoverPasswordCommandHandler 5
:6 7
IRequestHandler8 G
<G H)
ConfirmRecoverPasswordCommandH e
>e f
{ 
private 
readonly 
UserManager $
<$ %
ApplicationUser% 4
>4 5
_userManager6 B
;B C
private 
readonly 
ITokenManager &
_tokenManager' 4
;4 5
private 
readonly 
INotificator % 
_notificationService& :
;: ;
private 
readonly 
ILogger  
<  !0
$ConfirmRecoverPasswordCommandHandler! E
>E F
_loggerG N
;N O
public 0
$ConfirmRecoverPasswordCommandHandler 3
(3 4
UserManager4 ?
<? @
ApplicationUser@ O
>O P
userManagerQ \
,\ ]
ITokenManager^ k
tokenManagerl x
,x y
INotificator	z †!
notificationService
‡ š
,
š ›
ILogger
œ £
<
£ ¤2
$ConfirmRecoverPasswordCommandHandler
¤ È
>
È É
logger
Ê Ð
)
Ð Ñ
{ 	
_userManager 
= 
userManager &
;& '
_tokenManager 
= 
tokenManager (
;( ) 
_notificationService  
=! "
notificationService# 6
;6 7
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
Handle  
(  !)
ConfirmRecoverPasswordCommand! >
request? F
,F G
CancellationTokenH Y
cancellationTokenZ k
)k l
{ 	
Notification 
notification %
;% &
if 
( 
! 
_tokenManager 
. 
IsValidToken +
(+ ,
request, 3
.3 4
Token4 9
)9 :
): ;
{  
_notificationService   $
.  $ %
Notify  % +
(  + ,
new  , /
(  / 0
success  0 7
:  7 8
false  9 >
,  > ?
new  @ C
(  C D
$str  D s
)  s t
)  t u
)  u v
;  v w
_logger!! 
.!! 
LogError!!  
(!!  !
$str!!! W
)!!W X
;!!X Y
return"" 
;"" 
}## 
if%% 
(%% 
request%% 
.%% 
Password%%  
!=%%! #
request%%$ +
.%%+ ,
ConfirmPassword%%, ;
)%%; <
{&&  
_notificationService'' $
.''$ %
Notify''% +
(''+ ,
new'', /
(''/ 0
success''0 7
:''7 8
false''9 >
,''> ?
new''@ C
(''C D
$str''D Y
)''Y Z
)''Z [
)''[ \
;''\ ]
_logger(( 
.(( 
LogError((  
(((  !
$"((! #
$str((# R
"((R S
)((S T
;((T U
return)) 
;)) 
}** 
var,, 
user,, 
=,, 
await,, 
_userManager,, )
.,,) *
FindByIdAsync,,* 7
(,,7 8
request,,8 ?
.,,? @
UserId,,@ F
),,F G
;,,G H
if.. 
(.. 
user.. 
==.. 
null.. 
).. 
{// 
notification00 
=00 
new00 "
Notification00# /
(00/ 0
success000 7
:007 8
false009 >
,00> ?
new00@ C
(00C D
$str00D \
)00\ ]
)00] ^
;00^ _
_logger11 
.11 
LogError11  
(11  !
$"11! #
$str11# D
"11D E
)11E F
;11F G
}22 
else33 
{44 
var55 
result55 
=55 
await55 "
_userManager55# /
.55/ 0
ResetPasswordAsync550 B
(55B C
user55C G
,55G H
request55I P
.55P Q
Token55Q V
,55V W
request55X _
.55_ `
Password55` h
)55h i
;55i j
if77 
(77 
result77 
.77 
	Succeeded77 $
)77$ %
{88 
_tokenManager99 !
.99! "
InvalidToken99" .
(99. /
request99/ 6
.996 7
Token997 <
)99< =
;99= >
notification::  
=::! "
new::# &
Notification::' 3
(::3 4
success::4 ;
:::; <
true::= A
)::A B
;::B C
_logger;; 
.;; 
LogInformation;; *
(;;* +
$str;;+ b
);;b c
;;;c d
}<< 
else== 
{>> 
notification??  
=??! "
new??# &
Notification??' 3
(??3 4
success??4 ;
:??; <
false??= B
)??B C
;??C D
_logger@@ 
.@@ 
LogError@@ $
(@@$ %
$"@@% '
$str@@' 8
{@@8 9
user@@9 =
.@@= >
Email@@> C
}@@C D
$str@@D X
"@@X Y
)@@Y Z
;@@Z [
foreachBB 
(BB 
varBB  
errorBB! &
inBB' )
resultBB* 0
.BB0 1
ErrorsBB1 7
)BB7 8
{CC 
notificationDD $
.DD$ %

AddMessageDD% /
(DD/ 0
newDD0 3
(DD3 4
errorDD4 9
.DD9 :
CodeDD: >
,DD> ?
errorDD@ E
.DDE F
DescriptionDDF Q
)DDQ R
)DDR S
;DDS T
_loggerEE 
.EE  
LogErrorEE  (
(EE( )
$"EE) +
$strEE+ 1
{EE1 2
errorEE2 7
.EE7 8
CodeEE8 <
}EE< =
$strEE= M
{EEM N
errorEEN S
.EES T
DescriptionEET _
}EE_ `
"EE` a
)EEa b
;EEb c
}FF 
}GG 
}HH  
_notificationServiceJJ  
.JJ  !
NotifyJJ! '
(JJ' (
notificationJJ( 4
)JJ4 5
;JJ5 6
}KK 	
}LL 
}MM à
qG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Commands\Logout\LogoutCommand.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Commands %
.% &
Logout& ,
{ 
public 

class 
LogoutCommand 
:  
IRequest! )
{ 
public 
string 
Token 
{ 
get !
;! "
set# &
;& '
}( )
} 
}		 ” 
xG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Commands\Logout\LogoutCommandHandler.cs
	namespace		 	
Estimatz		
 
.		 
Login		 
.		 
API		 
.		 
Commands		 %
.		% &
Logout		& ,
{

 
public 

class  
LogoutCommandHandler %
:& '
IRequestHandler( 7
<7 8
LogoutCommand8 E
>E F
{ 
private 
readonly 
UserManager $
<$ %
ApplicationUser% 4
>4 5
_userManager6 B
;B C
private 
readonly 
ITokenManager &
_tokenManager' 4
;4 5
private 
readonly 
ILogger  
<  ! 
LogoutCommandHandler! 5
>5 6
_logger7 >
;> ?
private 
readonly 
INotificator % 
_notificationService& :
;: ;
public  
LogoutCommandHandler #
(# $
UserManager$ /
</ 0
ApplicationUser0 ?
>? @
userManagerA L
,L M
ITokenManagerN [
tokenManager\ h
,h i
INotificatorj v 
notificationService	w Š
,
Š ‹
ILogger
Œ “
<
“ ”"
LogoutCommandHandler
” ¨
>
¨ ©
logger
ª °
)
° ±
{ 	
_userManager 
= 
userManager &
;& '
_tokenManager 
= 
tokenManager (
;( )
_logger 
= 
logger 
;  
_notificationService  
=! "
notificationService# 6
;6 7
} 	
public 
async 
Task 
Handle  
(  !
LogoutCommand! .
request/ 6
,6 7
CancellationToken8 I
cancellationTokenJ [
)[ \
{ 	
Notification 
notification %
;% &
if 
( 
! 
_tokenManager 
. 
IsValidToken +
(+ ,
request, 3
.3 4
Token4 9
)9 :
): ;
{ 
notification   
=   
new   "
(  " #
success  # *
:  * +
false  , 1
,  1 2
new  3 6
(  6 7
$str  7 b
)  b c
)  c d
;  d e
_logger!! 
.!! 

LogWarning!! "
(!!" #
$str!!# O
)!!O P
;!!P Q
}"" 
else## 
{$$ 
var%% 
simpleToken%% 
=%%  !
_tokenManager%%" /
.%%/ 0
GetSimpleToken%%0 >
(%%> ?
request%%? F
.%%F G
Token%%G L
)%%L M
;%%M N
var&& 
user&& 
=&& 
await&&  
_userManager&&! -
.&&- .
FindByIdAsync&&. ;
(&&; <
simpleToken&&< G
.&&G H
UserId&&H N
)&&N O
;&&O P
notification(( 
=(( 
new(( "
(((" #
success((# *
:((* +
true((, 0
)((0 1
;((1 2
_tokenManager)) 
.)) 
InvalidToken)) *
())* +
request))+ 2
.))2 3
Token))3 8
)))8 9
;))9 :
_logger** 
.** 
LogInformation** &
(**& '
$"**' )
$str**) <
{**< =
user**= A
?**A B
.**B C
Email**C H
}**H I
$str**I Z
"**Z [
)**[ \
;**\ ]
}++  
_notificationService--  
.--  !
Notify--! '
(--' (
notification--( 4
)--4 5
;--5 6
}.. 	
}// 
}00 ¶
ƒG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Commands\RecoverPassword\RecoverPasswordCommand.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Commands %
.% &
RecoverPassword& 5
{ 
public 

class "
RecoverPasswordCommand '
:( )
IRequest* 2
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
string 
? 
ApplicationURL %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
}		 
}

 ì$
ŠG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Commands\RecoverPassword\RecoverPasswordCommandhandler.cs
	namespace

 	
Estimatz


 
.

 
Login

 
.

 
API

 
.

 
Commands

 %
.

% &
RecoverPassword

& 5
{ 
public 

class )
RecoverPasswordCommandHandler .
:/ 0
IRequestHandler1 @
<@ A"
RecoverPasswordCommandA W
>W X
{ 
private 
readonly 
UserManager $
<$ %
ApplicationUser% 4
>4 5
_userManager6 B
;B C
private 
readonly 
ITokenManager &
_tokenManager' 4
;4 5
private 
readonly 
INotificator % 
_notificationService& :
;: ;
private 
readonly 
ILogger  
<  !)
RecoverPasswordCommandHandler! >
>> ?
_logger@ G
;G H
private 
readonly 
	IMediator "
	_mediator# ,
;, -
public )
RecoverPasswordCommandHandler ,
(, -
UserManager- 8
<8 9
ApplicationUser9 H
>H I
userManagerJ U
,U V
ITokenManagerW d
tokenManagere q
,q r
INotificators !
notificationService
€ “
,
“ ”
ILogger
• œ
<
œ +
RecoverPasswordCommandHandler
 º
>
º »
logger
¼ Â
,
Â Ã
	IMediator
Ä Í
mediator
Î Ö
)
Ö ×
{ 	
_userManager 
= 
userManager &
;& '
_tokenManager 
= 
tokenManager (
;( ) 
_notificationService  
=! "
notificationService# 6
;6 7
_logger 
= 
logger 
; 
	_mediator 
= 
mediator  
;  !
} 	
public 
async 
Task 
Handle  
(  !"
RecoverPasswordCommand! 7
request8 ?
,? @
CancellationTokenA R
cancellationTokenS d
)d e
{ 	
var 
user 
= 
await 
_userManager )
.) *
FindByEmailAsync* :
(: ;
request; B
.B C
EmailC H
)H I
;I J
if!! 
(!! 
user!! 
is!! 
null!! 
)!! 
{"" 
var## 
notifiation## 
=##  !
new##" %
Notification##& 2
(##2 3
success##3 :
:##: ;
false##< A
,##A B
new##C F
(##F G
$str##G _
)##_ `
)##` a
;##a b 
_notificationService$$ $
.$$$ %
Notify$$% +
($$+ ,
notifiation$$, 7
)$$7 8
;$$8 9
_logger&& 
.&& 
LogError&&  
(&&  !
$"&&! #
$str&&# ;
{&&; <
request&&< C
.&&C D
Email&&D I
}&&I J
$str&&J Z
"&&Z [
)&&[ \
;&&\ ]
return'' 
;'' 
}(( 
var** 
token** 
=** 
await** 
_tokenManager** +
.**+ ,(
GenerateRecoverPasswordToken**, H
(**H I
user**I M
)**M N
;**N O
await++ 
	_mediator++ 
.++ 
Publish++ #
(++# $
new++$ '%
RecoverPasswordEmailEvent++( A
{,, 
ApplicationURL-- 
=--  
request--! (
.--( )
ApplicationURL--) 7
,--7 8
Email.. 
=.. 
request.. 
...  
Email..  %
,..% &
Token// 
=// 
token// 
,// 
UserId00 
=00 
user00 
.00 
Id00  
,00  !
Username11 
=11 
user11 
.11  
Name11  $
}22 
)22 
;22  
_notificationService44  
.44  !
Notify44! '
(44' (
new44( +
(44+ ,
success44, 3
:443 4
true445 9
)449 :
)44: ;
;44; <
}55 	
}66 
}77 Í
}G:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Commands\RefreshToken\RefreshTokenCommand.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Commands %
.% &
RefreshToken& 2
{ 
public 

class 
RefreshTokenCommand $
:% &
IRequest' /
</ 0

SignInUser0 :
>: ;
{ 
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
public		 
string		 
TokenString		 !
{		" #
get		$ '
;		' (
set		) ,
;		, -
}		. /
}

 
} Ê
„G:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Commands\RefreshToken\RefreshTokenCommandHandler.cs
	namespace		 	
Estimatz		
 
.		 
Login		 
.		 
API		 
.		 
Commands		 %
.		% &
RefreshToken		& 2
{

 
public 

class &
RefreshTokenCommandHandler +
:, -
IRequestHandler. =
<= >
RefreshTokenCommand> Q
,Q R

SignInUserS ]
>] ^
{ 
private 
readonly 
ITokenManager &
_tokenManager' 4
;4 5
private 
readonly 
INotificator % 
_notificationService& :
;: ;
private 
readonly 
ILogger  
<  !&
RefreshTokenCommandHandler! ;
>; <
_logger= D
;D E
public &
RefreshTokenCommandHandler )
() *
ITokenManager* 7
tokenManager8 D
,D E
INotificatorF R
notificatorS ^
,^ _
ILogger` g
<g h'
RefreshTokenCommandHandler	h ‚
>
‚ ƒ
logger
„ Š
)
Š ‹
{ 	
_tokenManager 
= 
tokenManager (
;( ) 
_notificationService  
=! "
notificator# .
;. /
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 

SignInUser $
>$ %
Handle& ,
(, -
RefreshTokenCommand- @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 	
if 
( 
! 
_tokenManager 
. 
IsValidToken +
(+ ,
request, 3
.3 4
TokenString4 ?
)? @
)@ A
{ 
var 
notification  
=! "
new# &
Notification' 3
(3 4
success4 ;
:; <
false= B
,B C
newD G
MessageH O
(O P
$str	P ’
)
’ “
)
“ ”
;
” • 
_notificationService $
.$ %
Notify% +
(+ ,
notification, 8
)8 9
;9 :
_logger 
. 
LogError  
(  !
$"! #
$str# g
{g h
requesth o
.o p
Emailp u
}u v
$str	v …
"
… †
)
† ‡
;
‡ ˆ
return   
new   

SignInUser   %
(  % &
)  & '
;  ' (
}!! 
var## 
newToken## 
=## 
await##  
_tokenManager##! .
.##. /
RefreshToken##/ ;
(##; <
new##< ?$
RefreshTokenRequestModel##@ X
{$$ 
Email%% 
=%% 
request%% 
.%%  
Email%%  %
,%%% &
TokenString&& 
=&& 
request&& %
.&&% &
TokenString&&& 1
}'' 
)'' 
;''  
_notificationService))  
.))  !
Notify))! '
())' (
new))( +
Notification)), 8
())8 9
success))9 @
:))@ A
true))B F
)))F G
)))G H
;))H I
_logger** 
.** 
LogInformation** "
(**" #
$"**# %
$str**% D
{**D E
request**E L
.**L M
Email**M R
}**R S
$str**S j
"**j k
)**k l
;**l m
return,, 
newToken,, 
;,, 
}-- 	
}.. 
}// ä
|G:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Commands\Register\RegisterNewUserCommand.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Commands %
.% &
Register& .
{ 
public 

class "
RegisterNewUserCommand '
:( )
IRequest* 2
{ 
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
public		 
string		 
Username		 
{		  
get		! $
;		$ %
set		& )
;		) *
}		+ ,
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
EmailAddress	 
( 
ErrorMessage "
=# $
$str% K
)K L
]L M
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage '
=( )
$str* ^
,^ _
MinimumLength` m
=n o
$nump q
)q r
]r s
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
[ 	
Compare	 
( 
$str 
, 
ErrorMessage )
=* +
$str, E
)E F
]F G
public 
string 
ConfirmPassword %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
public 
string 
? 
ApplicationUrl %
{& '
get( +
;+ ,
set- 0
;0 1
}2 3
} 
} ô8
ƒG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Commands\Register\RegisterNewUserCommandHandler.cs
	namespace

 	
Estimatz


 
.

 
Login

 
.

 
API

 
.

 
Commands

 %
.

% &
Register

& .
{ 
public 

class )
RegisterNewUserCommandHandler .
:/ 0
IRequestHandler1 @
<@ A"
RegisterNewUserCommandA W
>W X
{ 
private 
readonly 
UserManager $
<$ %
ApplicationUser% 4
>4 5
_userManager6 B
;B C
private 
readonly 
ITokenManager &
_tokenManager' 4
;4 5
private 
readonly 
	IMediator "
	_mediator# ,
;, -
private 
readonly 
INotificator % 
_notificationService& :
;: ;
private 
readonly 
ILogger  
<  !)
RegisterNewUserCommandHandler! >
>> ?
_logger@ G
;G H
public )
RegisterNewUserCommandHandler ,
(, -
UserManager- 8
<8 9
ApplicationUser9 H
>H I
userManagerJ U
,U V
ITokenManagerW d
tokenManagere q
,q r
	IMediators |
mediator	} …
,
… †
INotificator
‡ “!
notificationService
” §
,
§ ¨
ILogger
© °
<
° ±+
RegisterNewUserCommandHandler
± Î
>
Î Ï
logger
Ð Ö
)
Ö ×
{ 	
_userManager 
= 
userManager &
;& '
_tokenManager 
= 
tokenManager (
;( )
	_mediator 
= 
mediator  
;  ! 
_notificationService  
=! "
notificationService# 6
;6 7
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
Handle  
(  !"
RegisterNewUserCommand! 7
request8 ?
,? @
CancellationTokenA R
cancellationTokenS d
)d e
{ 	
Notification 
notification %
;% &
var!! 
user!! 
=!! 
new!! 
ApplicationUser!! *
{"" 
UserName## 
=## 
request## "
.##" #
Email### (
,##( )
Email$$ 
=$$ 
request$$ 
.$$  
Email$$  %
,$$% &
EmailConfirmed%% 
=%%  
false%%! &
,%%& '
Name&& 
=&& 
request&& 
.&& 
Username&& '
}'' 
;'' 
if)) 
()) 
request)) 
.)) 
Password)) 
!=))  "
request))# *
.))* +
ConfirmPassword))+ :
))): ;
{**  
_notificationService++ $
.++$ %
Notify++% +
(+++ ,
new++, /
Notification++0 <
(++< =
success++= D
:++D E
false++F K
,++K L
new++M P
Message++Q X
(++X Y
$str++Y n
)++n o
)++o p
)++p q
;++q r
_logger,, 
.,, 
LogInformation,, &
(,,& '
$",,' )
$str,,) 1
{,,1 2
user,,2 6
.,,6 7
Email,,7 <
},,< =
$str,,= ^
",,^ _
),,_ `
;,,` a
return-- 
;-- 
}.. 
var00 
result00 
=00 
await00 
_userManager00 +
.00+ ,
CreateAsync00, 7
(007 8
user008 <
,00< =
request00> E
.00E F
Password00F N
)00N O
;00O P
if22 
(22 
result22 
.22 
	Succeeded22  
)22  !
{33 
var44 
confirmationToken44 %
=44& '
await44( -
_tokenManager44. ;
.44; <*
GenerateConfirmEmailTokenAsync44< Z
(44Z [
user44[ _
)44_ `
;44` a
await55 
	_mediator55 
.55  
Publish55  '
(55' (
new55( +)
NewUserConfirmationEmailEvent55, I
{66 
UserId77 
=77 
user77 !
.77! "
Id77" $
,77$ %
Email88 
=88 
user88  
.88  !
Email88! &
,88& '
Username99 
=99 
user99 #
.99# $
Name99$ (
,99( )
Token:: 
=:: 
confirmationToken:: -
,::- .
ApplicationURL;; "
=;;# $
request;;% ,
.;;, -
ApplicationUrl;;- ;
}<< 
)<< 
;<< 
notification>> 
=>> 
new>> "
Notification>># /
(>>/ 0
success>>0 7
:>>7 8
true>>9 =
,>>= >
new>>? B
Message>>C J
(>>J K
$str>>K k
)>>k l
)>>l m
;>>m n
_logger?? 
.?? 
LogInformation?? &
(??& '
$"??' )
$str??) 1
{??1 2
user??2 6
.??6 7
Email??7 <
}??< =
$str??= U
"??U V
)??V W
;??W X
}@@ 
elseAA 
{BB 
notificationCC 
=CC 
newCC "
NotificationCC# /
(CC/ 0
successCC0 7
:CC7 8
falseCC9 >
)CC> ?
;CC? @
_loggerDD 
.DD 

LogWarningDD "
(DD" #
$"DD# %
$strDD% -
{DD- .
userDD. 2
.DD2 3
EmailDD3 8
}DD8 9
$strDD9 M
"DDM N
)DDN O
;DDO P
foreachFF 
(FF 
varFF 
errorFF "
inFF# %
resultFF& ,
.FF, -
ErrorsFF- 3
)FF3 4
{GG 
notificationHH  
.HH  !

AddMessageHH! +
(HH+ ,
newHH, /
MessageHH0 7
(HH7 8
errorHH8 =
.HH= >
CodeHH> B
,HHB C
errorHHD I
.HHI J
DescriptionHHJ U
)HHU V
)HHV W
;HHW X
_loggerII 
.II 

LogWarningII &
(II& '
$"II' )
$strII) /
{II/ 0
errorII0 5
.II5 6
CodeII6 :
}II: ;
$strII; K
{IIK L
errorIIL Q
.IIQ R
DescriptionIIR ]
}II] ^
"II^ _
)II_ `
;II` a
}JJ 
}KK  
_notificationServiceMM  
.MM  !
NotifyMM! '
(MM' (
notificationMM( 4
)MM4 5
;MM5 6
}NN 	
}OO 
}PP 