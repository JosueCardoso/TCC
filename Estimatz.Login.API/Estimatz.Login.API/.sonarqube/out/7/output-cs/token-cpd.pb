‘
nG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Queries\SignIn\SignInQuery.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Queries $
.$ %
SignIn% +
{ 
public 

class 
SignInQuery 
: 
IRequest '
<' (

SignInUser( 2
>2 3
{ 
[		 	
Required			 
(		 
ErrorMessage		 
=		  
$str		! <
)		< =
]		= >
[

 	
EmailAddress

	 
(

 
ErrorMessage

 "
=

# $
$str

% K
)

K L
]

L M
public 
string 
Email 
{ 
get !
;! "
set# &
;& '
}( )
[ 	
Required	 
( 
ErrorMessage 
=  
$str! <
)< =
]= >
[ 	
StringLength	 
( 
$num 
, 
ErrorMessage '
=( )
$str* ^
,^ _
MinimumLength` m
=n o
$nump q
)q r
]r s
public 
string 
Password 
{  
get! $
;$ %
set& )
;) *
}+ ,
} 
} Æ7
uG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API.Queries\SignIn\SignInQueryHandler.cs
	namespace		 	
Estimatz		
 
.		 
Login		 
.		 
API		 
.		 
Queries		 $
.		$ %
SignIn		% +
{

 
public 

class 
SignInQueryHandler #
:$ %
IRequestHandler& 5
<5 6
SignInQuery6 A
,A B

SignInUserC M
>M N
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
SignInManager &
<& '
ApplicationUser' 6
>6 7
_signInManager8 F
;F G
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
<  !
SignInQueryHandler! 3
>3 4
_logger5 <
;< =
public 
SignInQueryHandler !
(! "
UserManager" -
<- .
ApplicationUser. =
>= >
userManager? J
,J K
SignInManagerL Y
<Y Z
ApplicationUserZ i
>i j
signInManagerk x
,x y
ITokenManager	z á
tokenManager
à î
,
î ï
INotificator
ñ ¢!
notificationService
£ ∂
,
∂ ∑
ILogger
∏ ø
<
ø ¿ 
SignInQueryHandler
¿ “
>
“ ”
logger
‘ ⁄
)
⁄ €
{ 	
_userManager 
= 
userManager &
;& '
_signInManager 
= 
signInManager *
;* +
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
; 
} 	
public 
async 
Task 
< 

SignInUser $
>$ %
Handle& ,
(, -
SignInQuery- 8
request9 @
,@ A
CancellationTokenB S
cancellationTokenT e
)e f
{ 	
var 
user 
= 
await 
_userManager )
.) *
FindByEmailAsync* :
(: ;
request; B
.B C
EmailC H
)H I
;I J
if   
(   
user   
==   
null   
)   
return!! 
InvalidSignIn!! $
(!!$ %
$str!!% B
,!!B C
$"!!D F
$str!!F d
{!!d e
request!!e l
.!!l m
Email!!m r
}!!r s
$str	!!s ê
"
!!ê ë
,
!!ë í
request
!!ì ö
.
!!ö õ
Email
!!õ †
)
!!† °
;
!!° ¢
var## 
emailsIsConfirmed## !
=##" #
await##$ )
_userManager##* 6
.##6 7!
IsEmailConfirmedAsync##7 L
(##L M
user##M Q
)##Q R
;##R S
if%% 
(%% 
!%% 
emailsIsConfirmed%% "
)%%" #
return&& 
InvalidSignIn&& $
(&&$ %
$str&&% r
,&&r s
$"&&t v
$str&&v }
{&&} ~
request	&&~ Ö
.
&&Ö Ü
Email
&&Ü ã
}
&&ã å
$str
&&å õ
"
&&õ ú
,
&&ú ù
request
&&û •
.
&&• ¶
Email
&&¶ ´
)
&&´ ¨
;
&&¨ ≠
var(( 
result(( 
=(( 
await(( 
_signInManager(( -
.((- .
PasswordSignInAsync((. A
(((A B
request((B I
.((I J
Email((J O
,((O P
request((Q X
.((X Y
Password((Y a
,((a b
false((c h
,((h i
true((j n
)((n o
;((o p
if** 
(** 
result** 
.** 
IsLockedOut** "
)**" #
return++ 
InvalidSignIn++ $
(++$ %
$str++% h
,++h i
$"++j l
$str++l s
{++s t
request++t {
.++{ |
Email	++| Å
}
++Å Ç
$str
++Ç Ω
"
++Ω æ
,
++æ ø
request
++¿ «
.
++« »
Email
++» Õ
)
++Õ Œ
;
++Œ œ
if-- 
(-- 
!-- 
result-- 
.-- 
	Succeeded-- !
)--! "
return.. 
InvalidSignIn.. $
(..$ %
$str..% B
,..B C
$"..D F
$str..F d
{..d e
request..e l
...l m
Email..m r
}..r s
$str	..s ê
"
..ê ë
,
..ë í
request
..ì ö
.
..ö õ
Email
..õ †
)
..† °
;
..° ¢
_logger00 
.00 
LogInformation00 "
(00" #
$"00# %
$str00% -
{00- .
request00. 5
.005 6
Email006 ;
}00; <
$str00< P
"00P Q
)00Q R
;00R S 
_notificationService11  
.11  !
Notify11! '
(11' (
new11( +
Notification11, 8
(118 9
success119 @
:11@ A
true11B F
)11F G
)11G H
;11H I
return33 
await33 
_tokenManager33 &
.33& '
GenerateToken33' 4
(334 5
request335 <
.33< =
Email33= B
)33B C
;33C D
}44 	
private66 

SignInUser66 
InvalidSignIn66 (
(66( )
string66) /
messageNotifiaction660 C
,66C D
string66E K

messageLog66L V
,66V W
string66X ^
email66_ d
)66d e
{77 	
var88 
notification88 
=88 
new88 "
Notification88# /
(88/ 0
success880 7
:887 8
false889 >
,88> ?
new88@ C
Message88D K
(88K L
messageNotifiaction88L _
)88_ `
)88` a
;88a b 
_notificationService99  
.99  !
Notify99! '
(99' (
notification99( 4
)994 5
;995 6
_logger:: 
.:: 
LogInformation:: "
(::" #

messageLog::# -
)::- .
;::. /
return<< 
new<< 

SignInUser<< !
(<<! "
)<<" #
;<<# $
}== 	
}>> 
}?? 