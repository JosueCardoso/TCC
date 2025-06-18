Ÿ
nG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API\Controllers\BaseController.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Controllers (
{ 
[ 
ApiController 
] 
public 

class 
MainController 
:  !
ControllerBase" 0
{		 
private

 
readonly

 
INotificator

 %
_notificator

& 2
;

2 3
public 
MainController 
( 
INotificator *
notificator+ 6
)6 7
{ 	
_notificator 
= 
notificator &
;& '
} 	
	protected 
ActionResult 
CustomResponse -
(- .
object. 4
result5 ;
=< =
null> B
)B C
{ 	
if 
( 
_notificator 
. 
IsSucess %
&&& (
_notificator) 5
.5 6
HasMessages6 A
)A B
return 
Ok 
( 
new 
ActionResponse ,
(, -
success- 4
:4 5
true6 :
,: ;
messages< D
:D E
_notificatorF R
.R S
MessagesS [
.[ \
Select\ b
(b c
xc d
=>e g
xh i
)i j
)j k
)k l
;l m
else 
if 
( 
_notificator  
.  !
IsSucess! )
)) *
return 
Ok 
( 
new 
ActionResponse ,
(, -
success- 4
:4 5
true6 :
,: ;
data< @
:@ A
resultB H
,H I
messagesJ R
:R S
nullT X
)X Y
)Y Z
;Z [
return 

BadRequest 
( 
new !
ActionResponse" 0
(0 1
success1 8
:8 9
false: ?
,? @
messagesA I
:I J
_notificatorK W
.W X
MessagesX `
.` a
Selecta g
(g h
xh i
=>j l
xm n
)n o
)o p
)p q
;q r
} 	
} 
} Ò4
tG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API\Controllers\v1\AccountController.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Controllers (
.( )
v1) +
{ 
[ 

ApiVersion 
( 
$str 
) 
] 
[ 
Route 

(
 
$str 3
)3 4
]4 5
[ 
ApiController 
] 
public 

class 
AccountController "
:# $
MainController% 3
{ 
private 
readonly 
	IMediator "
	_mediator# ,
;, -
public 
AccountController  
(  !
	IMediator! *
mediator+ 3
,3 4
INotificator5 A
notificatorB M
)M N
:O P
baseQ U
(U V
notificatorV a
)a b
{ 	
	_mediator 
= 
mediator  
;  !
} 	
[ 	
HttpPost	 
( 
$str 
) 
] 
public 
async 
Task 
< 
ActionResult &
>& '
Register( 0
(0 1"
RegisterNewUserCommand1 G
requestH O
)O P
{ 	
await   
	_mediator   
.   
Send    
(    !
request  ! (
)  ( )
;  ) *
return!! 
CustomResponse!! !
(!!! "
)!!" #
;!!# $
}"" 	
[$$ 	
HttpGet$$	 
($$ 
$str$$  
)$$  !
]$$! "
public%% 
async%% 
Task%% 
<%% 
ActionResult%% &
>%%& '
ConfirmEmail%%( 4
(%%4 5
[%%5 6
	FromQuery%%6 ?
]%%? @
[%%@ A
Required%%A I
]%%I J
string%%J P
userId%%Q W
,%%W X
[%%Y Z
	FromQuery%%Z c
]%%c d
[%%d e
Required%%e m
]%%m n
string%%o u
token%%v {
)%%{ |
{&& 	
await'' 
	_mediator'' 
.'' 
Send''  
(''  !
new''! $
ConfirmEmailCommand''% 8
{''9 :
Token''; @
=''A B
token''C H
,''H I
UserId''J P
=''Q R
userId''S Y
}''Z [
)''[ \
;''\ ]
return(( 
CustomResponse(( !
(((! "
)((" #
;((# $
})) 	
[++ 	
HttpPost++	 
(++ 
$str++ 
)++ 
]++ 
public,, 
async,, 
Task,, 
<,, 
ActionResult,, &
>,,& '
Login,,( -
(,,- .
SignInQuery,,. 9
request,,: A
),,A B
{-- 	
return.. 
CustomResponse.. !
(..! "
await.." '
	_mediator..( 1
...1 2
Send..2 6
(..6 7
request..7 >
)..> ?
)..? @
;..@ A
}// 	
[11 	
HttpPost11	 
(11 
$str11 
)11 
]11 
[22 	
	Authorize22	 
]22 
public33 
async33 
Task33 
<33 
ActionResult33 &
>33& '
Logout33( .
(33. /
)33/ 0
{44 	
var55 
accessToken55 
=55 
await55 #
HttpContext55$ /
.55/ 0
GetTokenAsync550 =
(55= >
$str55> F
,55F G
$str55H V
)55V W
;55W X
await66 
	_mediator66 
.66 
Send66  
(66  !
new66! $
LogoutCommand66% 2
{663 4
Token665 :
=66; <
accessToken66= H
}66I J
)66J K
;66K L
return88 
CustomResponse88 !
(88! "
)88" #
;88# $
}99 	
[;; 	
HttpPost;;	 
(;; 
$str;; $
);;$ %
];;% &
public<< 
async<< 
Task<< 
<<< 
ActionResult<< &
><<& '
RecoverPassword<<( 7
(<<7 8"
RecoverPasswordCommand<<8 N
request<<O V
)<<V W
{== 	
await>> 
	_mediator>> 
.>> 
Send>>  
(>>  !
request>>! (
)>>( )
;>>) *
return?? 
CustomResponse?? !
(??! "
)??" #
;??# $
}@@ 	
[BB 	
HttpPostBB	 
(BB 
$strBB ,
)BB, -
]BB- .
publicCC 
asyncCC 
TaskCC 
<CC 
ActionResultCC &
>CC& '"
ConfirmRecoverPasswordCC( >
(CC> ?)
ConfirmRecoverPasswordCommandCC? \
requestCC] d
)CCd e
{DD 	
awaitEE 
	_mediatorEE 
.EE 
SendEE  
(EE  !
requestEE! (
)EE( )
;EE) *
returnFF 
CustomResponseFF !
(FF! "
)FF" #
;FF# $
}GG 	
[II 	
HttpGetII	 
(II 
$strII  
)II  !
]II! "
[JJ 	
	AuthorizeJJ	 
]JJ 
publicKK 
asyncKK 
TaskKK 
<KK 
ActionResultKK &
>KK& '
RefreshTokenKK( 4
(KK4 5
[KK5 6
	FromQueryKK6 ?
]KK? @
[KK@ A
RequiredKKA I
]KKI J
stringKKK Q
emailKKR W
)KKW X
{LL 	
varMM 
accessTokenMM 
=MM 
awaitMM #
HttpContextMM$ /
.MM/ 0
GetTokenAsyncMM0 =
(MM= >
$strMM> F
,MMF G
$strMMH V
)MMV W
;MMW X
varNN 
requestNN 
=NN 
newNN 
RefreshTokenCommandNN 1
{NN2 3
TokenStringNN4 ?
=NN@ A
accessTokenNNB M
,NNM N
EmailNNO T
=NNU V
emailNNW \
}NN] ^
;NN^ _
returnPP 
CustomResponsePP !
(PP! "
awaitPP" '
	_mediatorPP( 1
.PP1 2
SendPP2 6
(PP6 7
requestPP7 >
)PP> ?
)PP? @
;PP@ A
}QQ 	
}RR 
}SS ª

rG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API\DbContext\ApplicationDbContext.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
	DbContext &
{ 
public 

class  
ApplicationDbContext %
:& '
IdentityDbContext( 9
<9 :
ApplicationUser: I
,I J
IdentityRoleK W
,W X
stringY _
>_ `
{		 
public

  
ApplicationDbContext

 #
(

# $
DbContextOptions

$ 4
<

4 5 
ApplicationDbContext

5 I
>

I J
options

K R
)

R S
:

T U
base

V Z
(

Z [
options

[ b
)

b c
{ 	
Database 
. 
EnsureCreated "
(" #
)# $
;$ %
} 	
	protected 
override 
void 
OnModelCreating  /
(/ 0
ModelBuilder0 <
builder= D
)D E
{ 	
base 
. 
OnModelCreating  
(  !
builder! (
)( )
;) *
} 	
} 
} î
}G:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API\Migrations\20230503030755_InitialIdentity.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 

Migrations '
{ 
public		 

partial		 
class		 
InitialIdentity		 (
:		) *
	Migration		+ 4
{

 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
}
ÅÅ 	
	protected
ÈÈ 
override
ÈÈ 
void
ÈÈ 
Down
ÈÈ  $
(
ÈÈ$ %
MigrationBuilder
ÈÈ% 5
migrationBuilder
ÈÈ6 F
)
ÈÈF G
{
ÉÉ 	
migrationBuilder
ÊÊ 
.
ÊÊ 
	DropTable
ÊÊ &
(
ÊÊ& '
name
ËË 
:
ËË 
$str
ËË (
)
ËË( )
;
ËË) *
migrationBuilder
ÍÍ 
.
ÍÍ 
	DropTable
ÍÍ &
(
ÍÍ& '
name
ÎÎ 
:
ÎÎ 
$str
ÎÎ (
)
ÎÎ( )
;
ÎÎ) *
migrationBuilder
ÐÐ 
.
ÐÐ 
	DropTable
ÐÐ &
(
ÐÐ& '
name
ÑÑ 
:
ÑÑ 
$str
ÑÑ (
)
ÑÑ( )
;
ÑÑ) *
migrationBuilder
ÓÓ 
.
ÓÓ 
	DropTable
ÓÓ &
(
ÓÓ& '
name
ÔÔ 
:
ÔÔ 
$str
ÔÔ '
)
ÔÔ' (
;
ÔÔ( )
migrationBuilder
ÖÖ 
.
ÖÖ 
	DropTable
ÖÖ &
(
ÖÖ& '
name
×× 
:
×× 
$str
×× (
)
××( )
;
××) *
migrationBuilder
ÙÙ 
.
ÙÙ 
	DropTable
ÙÙ &
(
ÙÙ& '
name
ÚÚ 
:
ÚÚ 
$str
ÚÚ #
)
ÚÚ# $
;
ÚÚ$ %
migrationBuilder
ÜÜ 
.
ÜÜ 
	DropTable
ÜÜ &
(
ÜÜ& '
name
ÝÝ 
:
ÝÝ 
$str
ÝÝ #
)
ÝÝ# $
;
ÝÝ$ %
}
ÞÞ 	
}
ßß 
}àà µ
€G:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API\Migrations\20230509031050_AdicionarNovoCampo.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 

Migrations '
{ 
public 

partial 
class 
AdicionarNovoCampo +
:, -
	Migration. 7
{		 
	protected 
override 
void 
Up  "
(" #
MigrationBuilder# 3
migrationBuilder4 D
)D E
{ 	
migrationBuilder 
. 
	AddColumn &
<& '
string' -
>- .
(. /
name 
: 
$str 
, 
table 
: 
$str $
,$ %
type 
: 
$str %
,% &
nullable 
: 
false 
,  
defaultValue 
: 
$str  
)  !
;! "
} 	
	protected 
override 
void 
Down  $
($ %
MigrationBuilder% 5
migrationBuilder6 F
)F G
{ 	
migrationBuilder 
. 

DropColumn '
(' (
name 
: 
$str 
, 
table 
: 
$str $
)$ %
;% &
} 	
} 
} ”
iG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API\Models\ActionResponse.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Models #
{ 
public 

class 
ActionResponse 
{ 
public 
ActionResponse 
( 
bool "
success# *
,* +
object, 2
data3 7
,7 8
object9 ?
messages@ H
)H I
{ 	
Success 
= 
success 
; 
Data 
= 
data 
; 
Messages		 
=		 
messages		 
;		  
}

 	
public 
ActionResponse 
( 
bool "
success# *
,* +
object, 2
messages3 ;
); <
{ 	
Success 
= 
success 
; 
Messages 
= 
messages 
;  
} 	
public 
bool 
Success 
{ 
get !
;! "
set# &
;& '
}( )
public 
object 
? 
Data 
{ 
get !
;! "
set# &
;& '
}( )
public 
object 
? 
Messages 
{  !
get" %
;% &
set' *
;* +
}, -
} 
} ÚV
[G:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Application\Estimatz.Login.API\Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
builder 
. 
Services 
. 
AddControllers 
(  
)  !
;! "
builder 
. 
Services 
. #
AddEndpointsApiExplorer (
(( )
)) *
;* +
builder 
. 
Services 
. 

AddMediatR 
( 
cfg 
=>  "
cfg# &
.& '(
RegisterServicesFromAssembly' C
(C D
typeofD J
(J K)
RegisterNewUserCommandHandlerK h
)h i
.i j
Assemblyj r
)r s
)s t
;t u
builder 
. 
Services 
. 

AddMediatR 
( 
cfg 
=>  "
cfg# &
.& '(
RegisterServicesFromAssembly' C
(C D
typeofD J
(J K0
$NewUserConfirmationEmailEventHandlerK o
)o p
.p q
Assemblyq y
)y z
)z {
;{ |
builder 
. 
Services 
. 

AddMediatR 
( 
cfg 
=>  "
cfg# &
.& '(
RegisterServicesFromAssembly' C
(C D
typeofD J
(J K
SignInQueryHandlerK ]
)] ^
.^ _
Assembly_ g
)g h
)h i
;i j
builder 
. 
Services 
. 
	AddScoped 
< 
ITokenManager (
,( )
TokenManager* 6
>6 7
(7 8
)8 9
;9 :
builder 
. 
Services 
. 
	AddScoped 
< 
IEmailService (
,( )
MailKitEmailService* =
>= >
(> ?
)? @
;@ A
builder   
.   
Services   
.   
AddSingleton   
<   
ITokenMemoryCache   /
,  / 0
TokenMemoryCache  1 A
>  A B
(  B C
)  C D
;  D E
builder!! 
.!! 
Services!! 
.!! 
	AddScoped!! 
<!! 
INotificator!! '
,!!' ( 
NotificationsService!!) =
>!!= >
(!!> ?
)!!? @
;!!@ A
builder"" 
."" 
Services"" 
."" 
AddHostedService"" !
<""! "
TokenCacheCleaner""" 3
>""3 4
(""4 5
)""5 6
;""6 7
builder$$ 
.$$ 
Services$$ 
.$$ 
AddIdentity$$ 
<$$ 
ApplicationUser$$ ,
,$$, -
IdentityRole$$. :
>$$: ;
($$; <
)$$< =
.%% $
AddEntityFrameworkStores%% 
<%%  
ApplicationDbContext%% .
>%%. /
(%%/ 0
)%%0 1
.&& $
AddDefaultTokenProviders&& 
(&& 
)&& 
;&& 
var(( 
config(( 

=(( 
new((  
ConfigurationBuilder(( %
(((% &
)((& '
.)) 
SetBasePath)) 
()) 
	Directory)) 
.)) 
GetCurrentDirectory)) .
()). /
)))/ 0
)))0 1
.** 
AddJsonFile** 
(** 
$str** #
)**# $
.++ 
Build++ 

(++
 
)++ 
;++ 
var-- 
connectionString-- 
=-- 
config-- 
.-- 
GetConnectionString-- 1
(--1 2
$str--2 F
)--F G
;--G H
builder// 
.// 
Services// 
.// 
AddDbContext// 
<//  
ApplicationDbContext// 2
>//2 3
(//3 4
options//4 ;
=>//< >
options00 
.00 
UseSqlServer00 
(00 
connectionString00 )
)00) *
)00* +
;00+ ,
var22 
appSettingsSections22 
=22 
config22  
.22  !

GetSection22! +
(22+ ,
$str22, 3
)223 4
;224 5
builder33 
.33 
Services33 
.33 
	Configure33 
<33 
TokenConfiguration33 -
>33- .
(33. /
appSettingsSections33/ B
)33B C
;33C D
var55 
tokenSettings55 
=55 
appSettingsSections55 '
.55' (
Get55( +
<55+ ,
TokenConfiguration55, >
>55> ?
(55? @
)55@ A
;55A B
var66 
key66 
=66 	
Encoding66
 
.66 
ASCII66 
.66 
GetBytes66 !
(66! "
tokenSettings66" /
.66/ 0
Secret660 6
)666 7
;667 8
builder88 
.88 
Services88 
.88 
AddAuthentication88 "
(88" #
x88# $
=>88% '
{99 
x:: 
.:: %
DefaultAuthenticateScheme:: 
=::  !
JwtBearerDefaults::" 3
.::3 4 
AuthenticationScheme::4 H
;::H I
x;; 
.;; "
DefaultChallengeScheme;; 
=;; 
JwtBearerDefaults;; 0
.;;0 1 
AuthenticationScheme;;1 E
;;;E F
}<< 
)<< 
.<< 
AddJwtBearer<< 
(<< 
x<< 
=><< 
{== 
x>> 
.>>  
RequireHttpsMetadata>> 
=>> 
false>> "
;>>" #
x?? 
.?? 
	SaveToken?? 
=?? 
true?? 
;?? 
x@@ 
.@@ %
TokenValidationParameters@@ 
=@@  !
new@@" %%
TokenValidationParameters@@& ?
{AA $
ValidateIssuerSigningKeyBB  
=BB! "
trueBB# '
,BB' (
IssuerSigningKeyCC 
=CC 
newCC  
SymmetricSecurityKeyCC 3
(CC3 4
keyCC4 7
)CC7 8
,CC8 9
ValidateIssuerDD 
=DD 
trueDD 
,DD 
ValidateAudienceEE 
=EE 
trueEE 
,EE  
ValidAudienceFF 
=FF 
tokenSettingsFF %
.FF% &
ValidateFF& .
,FF. /
ValidIssuerGG 
=GG 
tokenSettingsGG #
.GG# $
EmissorGG$ +
}HH 
;HH 
}II 
)II 
;II 
builderKK 
.KK 
ServicesKK 
.KK 
AddSwaggerGenKK 
(KK 
cKK  
=>KK! #
{LL 
cMM 
.MM 

SwaggerDocMM 
(MM 
$strMM 
,MM 
newMM 
OpenApiInfoMM &
{MM' (
TitleMM) .
=MM/ 0
$strMM1 ?
,MM? @
VersionMMA H
=MMI J
$strMMK O
}MMP Q
)MMQ R
;MMR S
cOO 
.OO !
AddSecurityDefinitionOO 
(OO 
$strOO $
,OO$ %
newOO& )!
OpenApiSecuritySchemeOO* ?
{PP 
DescriptionQQ 
=QQ 
$strQQ L
,QQL M
NameRR 
=RR 
$strRR 
,RR 
SchemeSS 
=SS 
$strSS 
,SS 
BearerFormatTT 
=TT 
$strTT 
,TT 
InUU 

=UU 
ParameterLocationUU 
.UU 
HeaderUU %
,UU% &
TypeVV 
=VV 
SecuritySchemeTypeVV !
.VV! "
ApiKeyVV" (
}WW 
)WW 
;WW 
cYY 
.YY "
AddSecurityRequirementYY 
(YY 
newYY  &
OpenApiSecurityRequirementYY! ;
{ZZ 
{[[ 	
new\\ !
OpenApiSecurityScheme\\ %
{]] 
	Reference^^ 
=^^ 
new^^ 
OpenApiReference^^  0
{__ 
Type`` 
=`` 
ReferenceType`` (
.``( )
SecurityScheme``) 7
,``7 8
Idaa 
=aa 
$straa !
}bb 
}cc 
,cc 
newdd 
stringdd 
[dd 
]dd 
{dd 
}dd 
}ee 	
}ff 
)ff 
;ff 
}gg 
)gg 
;gg 
builderii 
.ii 
Servicesii 
.ii 
AddApiVersioningii !
(ii! "
optionsii" )
=>ii* ,
{jj 
optionskk 
.kk 
DefaultApiVersionkk 
=kk 
newkk  #

ApiVersionkk$ .
(kk. /
$numkk/ 0
,kk0 1
$numkk2 3
)kk3 4
;kk4 5
optionsll 
.ll /
#AssumeDefaultVersionWhenUnspecifiedll /
=ll0 1
truell2 6
;ll6 7
optionsmm 
.mm 
ReportApiVersionsmm 
=mm 
truemm  $
;mm$ %
}nn 
)nn 
;nn 
varpp 
apppp 
=pp 	
builderpp
 
.pp 
Buildpp 
(pp 
)pp 
;pp 
ifss 
(ss 
appss 
.ss 
Environmentss 
.ss 
IsDevelopmentss !
(ss! "
)ss" #
)ss# $
{tt 
appuu 
.uu 

UseSwaggeruu 
(uu 
)uu 
;uu 
appvv 
.vv 
UseSwaggerUIvv 
(vv 
cvv 
=>vv 
{ww 
cxx 	
.xx	 

SwaggerEndpointxx
 
(xx 
$strxx 4
,xx4 5
$strxx6 D
)xxD E
;xxE F
}yy 
)yy 
;yy 
}zz 
app|| 
.|| 
UseHttpsRedirection|| 
(|| 
)|| 
;|| 
app}} 
.}} 
UseAuthentication}} 
(}} 
)}} 
;}} 
app~~ 
.~~ 
UseAuthorization~~ 
(~~ 
)~~ 
;~~ 
app 
. 
UseApiVersioning 
( 
) 
; 
app€€ 
.
€€ 
MapControllers
€€ 
(
€€ 
)
€€ 
;
€€ 
app‚‚ 
.
‚‚ 
Run
‚‚ 
(
‚‚ 
)
‚‚ 	
;
‚‚	 
