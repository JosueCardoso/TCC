…
kG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Domain\Estimatz.Login.API.Services\Token\ITokenManager.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Services %
.% &
Token& +
{ 
public 

	interface 
ITokenManager "
{ 
Task 
< 

SignInUser 
> 
GenerateToken &
(& '
string' -
email. 3
)3 4
;4 5
Task		 
<		 

SignInUser		 
>		 
RefreshToken		 %
(		% &$
RefreshTokenRequestModel		& >
refreshTokenModel		? P
)		P Q
;		Q R
bool

 
IsValidToken

 
(

 
string

  
token

! &
)

& '
;

' (
void 
InvalidToken 
( 
string  
token! &
)& '
;' (
Task 
< 
string 
> *
GenerateConfirmEmailTokenAsync 3
(3 4
ApplicationUser4 C
userD H
)H I
;I J
Task 
< 
string 
> (
GenerateRecoverPasswordToken 1
(1 2
ApplicationUser2 A
userB F
)F G
;G H
SimpleToken 
GetSimpleToken "
(" #
string# )
token* /
)/ 0
;0 1
} 
} ∂w
jG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Domain\Estimatz.Login.API.Services\Token\TokenManager.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Services %
.% &
Token& +
{ 
public 

class 
TokenManager 
: 
ITokenManager  -
{ 
private 
readonly 
TokenConfiguration +
_appSettings, 8
;8 9
private 
readonly 
UserManager $
<$ %
ApplicationUser% 4
>4 5
_userManager6 B
;B C
private 
readonly 
ITokenMemoryCache *
_tokenCache+ 6
;6 7
public 
TokenManager 
( 
IOptions $
<$ %
TokenConfiguration% 7
>7 8
appSettings9 D
,D E
UserManagerF Q
<Q R
ApplicationUserR a
>a b
userManagerc n
,n o
ITokenMemoryCache	p Å

tokenCache
Ç å
)
å ç
{ 	
_appSettings 
= 
appSettings &
.& '
Value' ,
;, -
_userManager 
= 
userManager &
;& '
_tokenCache 
= 

tokenCache $
;$ %
} 	
public 
async 
Task 
< 

SignInUser $
>$ %
GenerateToken& 3
(3 4
string4 :
email; @
)@ A
{ 	
var 
user 
= 
await 
_userManager )
.) *
FindByEmailAsync* :
(: ;
email; @
)@ A
;A B
var 
claims 
= 
await 
_userManager +
.+ ,
GetClaimsAsync, :
(: ;
user; ?
)? @
;@ A
var   
	userRoles   
=   
await   !
_userManager  " .
.  . /
GetRolesAsync  / <
(  < =
user  = A
)  A B
;  B C
claims"" 
."" 
Add"" 
("" 
new"" 
Claim""  
(""  !#
JwtRegisteredClaimNames""! 8
.""8 9
Sub""9 <
,""< =
user""> B
.""B C
Id""C E
)""E F
)""F G
;""G H
claims## 
.## 
Add## 
(## 
new## 
Claim##  
(##  !#
JwtRegisteredClaimNames##! 8
.##8 9
Email##9 >
,##> ?
user##@ D
.##D E
Email##E J
)##J K
)##K L
;##L M
claims$$ 
.$$ 
Add$$ 
($$ 
new$$ 
Claim$$  
($$  !#
JwtRegisteredClaimNames$$! 8
.$$8 9
Jti$$9 <
,$$< =
Guid$$> B
.$$B C
NewGuid$$C J
($$J K
)$$K L
.$$L M
ToString$$M U
($$U V
)$$V W
)$$W X
)$$X Y
;$$Y Z
claims%% 
.%% 
Add%% 
(%% 
new%% 
Claim%%  
(%%  !#
JwtRegisteredClaimNames%%! 8
.%%8 9
Nbf%%9 <
,%%< =
ToUnixEpochDate%%> M
(%%M N
DateTime%%N V
.%%V W
UtcNow%%W ]
)%%] ^
.%%^ _
ToString%%_ g
(%%g h
)%%h i
)%%i j
)%%j k
;%%k l
claims&& 
.&& 
Add&& 
(&& 
new&& 
Claim&&  
(&&  !#
JwtRegisteredClaimNames&&! 8
.&&8 9
Iat&&9 <
,&&< =
ToUnixEpochDate&&> M
(&&M N
DateTime&&N V
.&&V W
UtcNow&&W ]
)&&] ^
.&&^ _
ToString&&_ g
(&&g h
)&&h i
,&&i j
ClaimValueTypes&&k z
.&&z {
	Integer64	&&{ Ñ
)
&&Ñ Ö
)
&&Ö Ü
;
&&Ü á
claims'' 
.'' 
Add'' 
('' 
new'' 
Claim''  
(''  !

ClaimTypes''! +
.''+ ,
Authentication'', :
,'': ;
$str''< B
)''B C
)''C D
;''D E
foreach)) 
()) 
var)) 
userRole)) !
in))" $
	userRoles))% .
))). /
{** 
claims++ 
.++ 
Add++ 
(++ 
new++ 
Claim++ $
(++$ %
$str++% +
,+++ ,
userRole++- 5
)++5 6
)++6 7
;++7 8
},, 
var.. 
identityClaims.. 
=..  
new..! $
ClaimsIdentity..% 3
(..3 4
)..4 5
;..5 6
identityClaims// 
.// 
	AddClaims// $
(//$ %
claims//% +
)//+ ,
;//, -
var11 
expireAt11 
=11 
DateTime11 #
.11# $
UtcNow11$ *
.11* +

AddMinutes11+ 5
(115 6
_appSettings116 B
.11B C

Expiration11C M
)11M N
;11N O
var22 
accessToken22 
=22 
CreateToken22 )
(22) *
identityClaims22* 8
,228 9
expireAt22: B
)22B C
;22C D
var33 
simpleToken33 
=33 
new33 !
SimpleToken33" -
{44 
ExpireAt55 
=55 
expireAt55 #
,55# $
UserId66 
=66 
user66 
.66 
Id66  
,66  !
TokenString77 
=77 
accessToken77 )
}88 
;88 
_tokenCache:: 
.:: 
Add:: 
(:: 
accessToken:: '
,::' (
simpleToken::) 4
)::4 5
;::5 6
var<< 
response<< 
=<< 
new<< 

SignInUser<< )
{== 
AccessToken>> 
=>> 
accessToken>> )
,>>) *
	ExpiresIn?? 
=?? 
TimeSpan?? $
.??$ %
FromMinutes??% 0
(??0 1
_appSettings??1 =
.??= >

Expiration??> H
)??H I
.??I J
TotalSeconds??J V
,??V W
	UserToken@@ 
=@@ 
new@@ 
	UserToken@@  )
{AA 
IdBB 
=BB 
userBB 
.BB 
IdBB  
,BB  !
EmailCC 
=CC 
userCC  
.CC  !
EmailCC! &
,CC& '
ClaimsDD 
=DD 
claimsDD #
.DD# $
SelectDD$ *
(DD* +
xDD+ ,
=>DD- /
newDD0 3
	ClaimUserDD4 =
{EE 
TypeFF 
=FF 
xFF  
.FF  !
TypeFF! %
,FF% &
ValueGG 
=GG 
xGG  !
.GG! "
ValueGG" '
,GG' (
}HH 
)HH 
,HH 
}II 
}JJ 
;JJ 
returnLL 
responseLL 
;LL 
}MM 	
publicOO 
boolOO 
IsValidTokenOO  
(OO  !
stringOO! '
tokenOO( -
)OO- .
{PP 	
ifQQ 
(QQ 
stringQQ 
.QQ 
IsNullOrEmptyQQ $
(QQ$ %
tokenQQ% *
)QQ* +
)QQ+ ,
returnRR 
falseRR 
;RR 
varTT 
tokenByCacheTT 
=TT 
_tokenCacheTT *
.TT* +
GetTT+ .
(TT. /
tokenTT/ 4
)TT4 5
;TT5 6
ifUU 
(UU 
tokenByCacheUU 
isUU 
nullUU  $
||UU% '
tokenByCacheUU( 4
.UU4 5
ExpireAtUU5 =
<UU> ?
DateTimeUU@ H
.UUH I
UtcNowUUI O
)UUO P
returnVV 
falseVV 
;VV 
returnXX 
trueXX 
;XX 
}YY 	
public[[ 
async[[ 
Task[[ 
<[[ 

SignInUser[[ $
>[[$ %
RefreshToken[[& 2
([[2 3$
RefreshTokenRequestModel[[3 K
refreshTokenModel[[L ]
)[[] ^
{\\ 	
var]] 
newToken]] 
=]] 
await]]  
GenerateToken]]! .
(]]. /
refreshTokenModel]]/ @
.]]@ A
Email]]A F
)]]F G
;]]G H
_tokenCache^^ 
.^^ 
Remove^^ 
(^^ 
refreshTokenModel^^ 0
.^^0 1
TokenString^^1 <
)^^< =
;^^= >
return`` 
newToken`` 
;`` 
}aa 	
publiccc 
voidcc 
InvalidTokencc  
(cc  !
stringcc! '
tokencc( -
)cc- .
{dd 	
_tokenCacheee 
.ee 
Removeee 
(ee 
tokenee $
)ee$ %
;ee% &
}ff 	
publichh 
asynchh 
Taskhh 
<hh 
stringhh  
>hh  !*
GenerateConfirmEmailTokenAsynchh" @
(hh@ A
ApplicationUserhhA P
userhhQ U
)hhU V
{ii 	
varjj "
emailConfirmationTokenjj &
=jj' (
awaitjj) .
_userManagerjj/ ;
.jj; </
#GenerateEmailConfirmationTokenAsyncjj< _
(jj_ `
userjj` d
)jjd e
;jje f
varkk 
expireAtkk 
=kk 
DateTimekk #
.kk# $
UtcNowkk$ *
.kk* +

AddMinuteskk+ 5
(kk5 6
$numkk6 8
)kk8 9
;kk9 :
varll 
simpleTokenll 
=ll 
newll !
SimpleTokenll" -
{mm 
ExpireAtnn 
=nn 
expireAtnn #
,nn# $
UserIdoo 
=oo 
useroo 
.oo 
Idoo  
,oo  !
TokenStringpp 
=pp "
emailConfirmationTokenpp 4
}qq 
;qq 
_tokenCachess 
.ss 
Addss 
(ss "
emailConfirmationTokenss 2
,ss2 3
simpleTokenss4 ?
)ss? @
;ss@ A
returnuu "
emailConfirmationTokenuu )
;uu) *
}vv 	
publicxx 
asyncxx 
Taskxx 
<xx 
stringxx  
>xx  !(
GenerateRecoverPasswordTokenxx" >
(xx> ?
ApplicationUserxx? N
userxxO S
)xxS T
{yy 	
varzz 
passwordResetTokenzz "
=zz# $
awaitzz% *
_userManagerzz+ 7
.zz7 8+
GeneratePasswordResetTokenAsynczz8 W
(zzW X
userzzX \
)zz\ ]
;zz] ^
var{{ 
expireAt{{ 
={{ 
DateTime{{ #
.{{# $
UtcNow{{$ *
.{{* +

AddMinutes{{+ 5
({{5 6
$num{{6 8
){{8 9
;{{9 :
var|| 
simpleToken|| 
=|| 
new|| !
SimpleToken||" -
{}} 
ExpireAt~~ 
=~~ 
expireAt~~ #
,~~# $
UserId 
= 
user 
. 
Id  
,  !
TokenString
ÄÄ 
=
ÄÄ  
passwordResetToken
ÄÄ 0
}
ÅÅ 
;
ÅÅ 
_tokenCache
ÉÉ 
.
ÉÉ 
Add
ÉÉ 
(
ÉÉ  
passwordResetToken
ÉÉ .
,
ÉÉ. /
simpleToken
ÉÉ0 ;
)
ÉÉ; <
;
ÉÉ< =
return
ÖÖ  
passwordResetToken
ÖÖ %
;
ÖÖ% &
}
ÜÜ 	
public
àà 
SimpleToken
àà 
GetSimpleToken
àà )
(
àà) *
string
àà* 0
token
àà1 6
)
àà6 7
=>
àà8 :
_tokenCache
àà; F
.
ààF G
Get
ààG J
(
ààJ K
token
ààK P
)
ààP Q
;
ààQ R
private
ää 
string
ää 
CreateToken
ää "
(
ää" #
ClaimsIdentity
ää# 1
claims
ää2 8
,
ää8 9
DateTime
ää: B
expireAt
ääC K
)
ääK L
{
ãã 	
var
åå 
tokenHandler
åå 
=
åå 
new
åå "%
JwtSecurityTokenHandler
åå# :
(
åå: ;
)
åå; <
;
åå< =
var
çç 
key
çç 
=
çç 
Encoding
çç 
.
çç 
ASCII
çç $
.
çç$ %
GetBytes
çç% -
(
çç- .
_appSettings
çç. :
.
çç: ;
Secret
çç; A
)
ççA B
;
ççB C
var
èè 
token
èè 
=
èè 
tokenHandler
èè $
.
èè$ %
CreateToken
èè% 0
(
èè0 1
new
èè1 4%
SecurityTokenDescriptor
èè5 L
{
êê 
Issuer
ëë 
=
ëë 
_appSettings
ëë %
.
ëë% &
Emissor
ëë& -
,
ëë- .
Audience
íí 
=
íí 
_appSettings
íí '
.
íí' (
Validate
íí( 0
,
íí0 1
Subject
ìì 
=
ìì 
claims
ìì  
,
ìì  !
Expires
îî 
=
îî 
expireAt
îî "
,
îî" # 
SigningCredentials
ïï "
=
ïï# $
new
ïï% ( 
SigningCredentials
ïï) ;
(
ïï; <
new
ïï< ?"
SymmetricSecurityKey
ïï@ T
(
ïïT U
key
ïïU X
)
ïïX Y
,
ïïY Z 
SecurityAlgorithms
ïï[ m
.
ïïm n"
HmacSha256Signatureïïn Å
)ïïÅ Ç
}
ññ 
)
ññ 
;
ññ 
return
òò 
tokenHandler
òò 
.
òò  

WriteToken
òò  *
(
òò* +
token
òò+ 0
)
òò0 1
;
òò1 2
}
ôô 	
private
õõ 
static
õõ 
long
õõ 
ToUnixEpochDate
õõ +
(
õõ+ ,
DateTime
õõ, 4
date
õõ5 9
)
õõ9 :
=>
úú 
(
úú 
long
úú 
)
úú 
Math
úú 
.
úú 
Round
úú 
(
úú 
(
úú  
date
úú  $
.
úú$ %
ToUniversalTime
úú% 4
(
úú4 5
)
úú5 6
-
úú7 8
new
úú9 <
DateTimeOffset
úú= K
(
úúK L
$num
úúL P
,
úúP Q
$num
úúR S
,
úúS T
$num
úúU V
,
úúV W
$num
úúX Y
,
úúY Z
$num
úú[ \
,
úú\ ]
$num
úú^ _
,
úú_ `
TimeSpan
úúa i
.
úúi j
Zero
úúj n
)
úún o
)
úúo p
.
úúp q
TotalSeconds
úúq }
)
úú} ~
;
úú~ 
}
ùù 
}ûû 