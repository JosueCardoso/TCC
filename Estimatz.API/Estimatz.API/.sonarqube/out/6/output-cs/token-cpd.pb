À
cG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Commands\AddStory\AddStoryCommand.cs
	namespace 	
Estimatz
 
. 
API 
. 
Commands 
.  
AddStory  (
{ 
public 

class 
AddStoryCommand  
:! "
IRequest# +
{ 
public 
Guid 
RoomId 
{ 
get  
;  !
set" %
;% &
}' (
public		 
Story		 
Story		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
}

 
} ‚
jG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Commands\AddStory\AddStoryCommandHandler.cs
	namespace 	
Estimatz
 
. 
API 
. 
Commands 
.  
AddStory  (
{ 
public		 

class		 "
AddStoryCommandHandler		 '
:		( )
IRequestHandler		* 9
<		9 :
AddStoryCommand		: I
>		I J
{

 
private 
readonly 
IStoryRepository )
_storyRepository* :
;: ;
private 
readonly 
INotificator % 
_notificationService& :
;: ;
private 
readonly 
ILogger  
<  !"
AddStoryCommandHandler! 7
>7 8
_logger9 @
;@ A
public "
AddStoryCommandHandler %
(% &
IStoryRepository& 6
storyRepository7 F
,F G
INotificatorH T
notificationServiceU h
,h i
ILoggerj q
<q r#
AddStoryCommandHandler	r à
>
à â
logger
ä ê
)
ê ë
{ 	
_storyRepository 
= 
storyRepository .
;. / 
_notificationService  
=! "
notificationService# 6
;6 7
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
Handle  
(  !
AddStoryCommand! 0
command1 8
,8 9
CancellationToken: K
cancellationTokenL ]
)] ^
{ 	
var 
response 
= 
await  
_storyRepository! 1
.1 2
AddStory2 :
(: ;
command; B
.B C
RoomIdC I
,I J
commandK R
.R S
StoryS X
)X Y
;Y Z
if 
( 
response 
. 

StatusCode "
==# %
HttpStatusCode& 4
.4 5
OK5 7
)7 8
{ 
_logger 
. 
LogInformation &
(& '
$"' )
$str) J
{J K
commandK R
.R S
RoomIdS Y
}Y Z
"Z [
)[ \
;\ ] 
_notificationService $
.$ %
Notify% +
(+ ,
new, /
(/ 0
success0 7
:7 8
true9 =
)= >
)> ?
;? @
return 
; 
} 
_logger!! 
.!! 
LogError!! 
(!! 
$"!! 
$str!! T
{!!T U
command!!U \
.!!\ ]
RoomId!!] c
}!!c d
"!!d e
)!!e f
;!!f g 
_notificationService""  
.""  !
Notify""! '
(""' (
new""( +
(""+ ,
success"", 3
:""3 4
false""5 :
)"": ;
)""; <
;""< =
return## 
;## 
}$$ 	
}%% 
}&& ”
gG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Commands\DeleteRoom\DeleteRoomCommand.cs
	namespace 	
Estimatz
 
. 
API 
. 
Commands 
.  

DeleteRoom  *
{ 
public 

class 
DeleteRoomCommand "
:# $
IRequest% -
{ 
public 
Guid 
RoomId 
{ 
get  
;  !
set" %
;% &
}' (
public 
Guid 
UserId 
{ 
get  
;  !
set" %
;% &
}' (
}		 
}

 ™#
nG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Commands\DeleteRoom\DeleteRoomCommandHandler.cs
	namespace 	
Estimatz
 
. 
API 
. 
Commands 
.  

DeleteRoom  *
{ 
public 

class $
DeleteRoomCommandHandler )
:* +
IRequestHandler, ;
<; <
DeleteRoomCommand< M
>M N
{		 
private

 
readonly

 
IRoomRepository

 (
_roomRepository

) 8
;

8 9
private 
readonly 
INotificator % 
_notificationService& :
;: ;
private 
readonly 
ILogger  
<  !$
DeleteRoomCommandHandler! 9
>9 :
_logger; B
;B C
public $
DeleteRoomCommandHandler '
(' (
IRoomRepository( 7
roomRepository8 F
,F G
INotificatorH T
notificationServiceU h
,h i
ILoggerj q
<q r%
DeleteRoomCommandHandler	r ä
>
ä ã
logger
å í
)
í ì
{ 	
_roomRepository 
= 
roomRepository ,
;, - 
_notificationService  
=! "
notificationService# 6
;6 7
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
Handle  
(  !
DeleteRoomCommand! 2
request3 :
,: ;
CancellationToken< M
cancellationTokenN _
)_ `
{ 	
var 
room 
= 
await 
_roomRepository ,
., -
FindRoom- 5
(5 6
request6 =
.= >
RoomId> D
)D E
;E F
if 
( 
room 
. 
UserId 
== 
request %
.% &
UserId& ,
), -
{ 
var 
response 
= 
await $
_roomRepository% 4
.4 5

DeleteRoom5 ?
(? @
request@ G
.G H
RoomIdH N
)N O
;O P
if 
( 
response 
. 

StatusCode '
==( *
System+ 1
.1 2
Net2 5
.5 6
HttpStatusCode6 D
.D E
	NoContentE N
)N O
{ 
_logger 
. 
LogInformation *
(* +
$"+ -
$str- 2
{2 3
room3 7
.7 8
Id8 :
}: ;
$str; ^
{^ _
request_ f
.f g
UserIdg m
}m n
"n o
)o p
;p q 
_notificationService (
.( )
Notify) /
(/ 0
new0 3
(3 4
success4 ;
:; <
true= A
)A B
)B C
;C D
return 
; 
}   
else!! 
{"" 
_logger## 
.## 
LogError## $
(##$ %
$"##% '
$str##' H
"##H I
)##I J
;##J K 
_notificationService$$ (
.$$( )
Notify$$) /
($$/ 0
new$$0 3
($$3 4
success$$4 ;
:$$; <
false$$= B
,$$B C
new$$D G
($$G H
$str$$H i
)$$i j
)$$j k
)$$k l
;$$l m
return%% 
;%% 
}&& 
}'' 
_logger)) 
.)) 
LogError)) 
()) 
$")) 
$str)) V
{))V W
request))W ^
.))^ _
UserId))_ e
}))e f
$str))f n
{))n o
room))o s
.))s t
Id))t v
}))v w
"))w x
)))x y
;))y z 
_notificationService**  
.**  !
Notify**! '
(**' (
new**( +
(**+ ,
success**, 3
:**3 4
false**5 :
,**: ;
new**< ?
(**? @
$str**@ a
)**a b
)**b c
)**c d
;**d e
}++ 	
},, 
}-- ô
^G:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Commands\Mapping\RoomMapping.cs
	namespace 	
Estimatz
 
. 
API 
. 
Commands 
.  
Mapping  '
{ 
public 

class 
RoomMapping 
: 
Profile &
{ 
public		 
RoomMapping		 
(		 
)		 
{

 	
	CreateMap 
< 
SaveRoomCommand %
,% &
Room' +
>+ ,
(, -
)- .
;. /
} 	
} 
} ÿ
iG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Commands\RemoveStory\RemoveStoryCommand.cs
	namespace 	
Estimatz
 
. 
API 
. 
Commands 
.  
RemoveStory  +
{ 
public 

class 
RemoveStoryCommand #
:$ %
IRequest& .
{ 
public 
Guid 
StoryId 
{ 
get !
;! "
set# &
;& '
}( )
public 
Guid 
RoomId 
{ 
get  
;  !
set" %
;% &
}' (
}		 
}

 ç$
pG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Commands\RemoveStory\RemoveStoryCommandHandler.cs
	namespace 	
Estimatz
 
. 
API 
. 
Commands 
.  
RemoveStory  +
{		 
public

 

class

 %
RemoveStoryCommandHandler

 *
:

+ ,
IRequestHandler

- <
<

< =
RemoveStoryCommand

= O
>

O P
{ 
private 
readonly 
IStoryRepository )
_storyRepository* :
;: ;
private 
readonly 
IRoomRepository (
_roomRepository) 8
;8 9
private 
readonly 
INotificator % 
_notificationService& :
;: ;
private 
readonly 
ILogger  
<  !%
RemoveStoryCommandHandler! :
>: ;
_logger< C
;C D
public %
RemoveStoryCommandHandler (
(( )
IStoryRepository) 9
storyRepository: I
,I J
IRoomRepositoryK Z
roomRepository[ i
,i j
INotificatork w 
notificationService	x ã
,
ã å
ILogger
ç î
<
î ï'
RemoveStoryCommandHandler
ï Æ
>
Æ Ø
logger
∞ ∂
)
∂ ∑
{ 	
_storyRepository 
= 
storyRepository .
;. /
_roomRepository 
= 
roomRepository ,
;, - 
_notificationService  
=! "
notificationService# 6
;6 7
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
Handle  
(  !
RemoveStoryCommand! 3
request4 ;
,; <
CancellationToken= N
cancellationTokenO `
)` a
{ 	
var 
room 
= 
await 
_roomRepository ,
., -
FindRoom- 5
(5 6
request6 =
.= >
RoomId> D
)D E
;E F
if 
( 
room 
!= 
null 
) 
{ 
var 

indexArray 
=  
room! %
.% &
UserStories& 1
.1 2
	FindIndex2 ;
(; <
x< =
=>> @
xA B
.B C
IdC E
==F H
requestI P
.P Q
StoryIdQ X
)X Y
;Y Z
if!! 
(!! 

indexArray!! 
!=!!  
-!!! "
$num!!" #
)!!# $
{"" 
var## 
response##  
=##! "
await### (
_storyRepository##) 9
.##9 :
RemoveStory##: E
(##E F

indexArray##F P
,##P Q
request##R Y
.##Y Z
RoomId##Z `
)##` a
;##a b
if%% 
(%% 
response%% 
.%%  

StatusCode%%  *
==%%+ -
HttpStatusCode%%. <
.%%< =
OK%%= ?
)%%? @
{&& 
_logger'' 
.''  
LogInformation''  .
(''. /
$"''/ 1
$str''1 :
{'': ;
request''; B
.''B C
StoryId''C J
}''J K
$str''K ]
{''] ^
request''^ e
.''e f
RoomId''f l
}''l m
"''m n
)''n o
;''o p 
_notificationService(( ,
.((, -
Notify((- 3
(((3 4
new((4 7
(((7 8
success((8 ?
:((? @
true((A E
)((E F
)((F G
;((G H
return)) 
;)) 
}** 
}++ 
},, 
_logger.. 
... 
LogInformation.. "
(.." #
$"..# %
$str..% I
{..I J
request..J Q
...Q R
StoryId..R Y
}..Y Z
$str..Z c
{..c d
request..d k
...k l
RoomId..l r
}..r s
"..s t
)..t u
;..u v 
_notificationService//  
.//  !
Notify//! '
(//' (
new//( +
(//+ ,
success//, 3
://3 4
false//5 :
)//: ;
)//; <
;//< =
}00 	
}11 
}22 †
cG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Commands\SaveRoom\SaveRoomCommand.cs
	namespace 	
Estimatz
 
. 
API 
. 
Commands 
.  
SaveRoom  (
{ 
public 

class 
SaveRoomCommand  
:! "
IRequest$ ,
<, -
Guid- 1
>1 2
{		 
[

 	
JsonProperty

	 
(

 
$str

 
)

 
]

  
public 

RoomStatus 
Status  
{! "
get# &
;& '
set( +
;+ ,
}- .
[ 	
JsonProperty	 
( 
$str #
)# $
]$ %
public 
List 
< 
Story 
> 
? 
UserStories '
{( )
get* -
;- .
set/ 2
;2 3
}4 5
[ 	
JsonProperty	 
( 
$str 
) 
]  
public 
Guid 
UserId 
{ 
get  
;  !
set" %
;% &
}' (
[ 	
JsonProperty	 
( 
$str "
)" #
]# $
public 

RoomConfig 

RoomConfig $
{% &
get' *
;* +
set, /
;/ 0
}1 2
} 
} ˚-
jG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Commands\SaveRoom\SaveRoomCommandHandler.cs
	namespace 	
Estimatz
 
. 
API 
. 
Commands 
.  
SaveRoom  (
{ 
public 

class "
SaveRoomCommandHandler '
:( )
IRequestHandler* 9
<9 :
SaveRoomCommand: I
,I J
GuidK O
>O P
{ 
private 
readonly 
IRoomRepository (
_roomRepository) 8
;8 9
private 
readonly 
IMapper  
_mapper! (
;( )
private 
readonly 
INotificator % 
_notificationService& :
;: ;
private 
readonly 
ILogger  
<  !"
SaveRoomCommandHandler! 7
>7 8
_logger9 @
;@ A
private 
readonly 
	IMediator "
_mediatr# +
;+ ,
public "
SaveRoomCommandHandler %
(% &
IRoomRepository& 5
roomRepository6 D
,D E
IMapperF M
mapperN T
,T U
INotificatorV b
notificationServicec v
,v w
ILoggerx 
<	 Ä$
SaveRoomCommandHandler
Ä ñ
>
ñ ó
logger
ò û
,
û ü
	IMediator
† ©
mediatr
™ ±
)
± ≤
{ 	
_roomRepository 
= 
roomRepository ,
;, -
_mapper 
= 
mapper 
;  
_notificationService  
=! "
notificationService# 6
;6 7
_logger 
= 
logger 
; 
_mediatr 
= 
mediatr 
; 
} 	
public 
async 
Task 
< 
Guid 
> 
Handle  &
(& '
SaveRoomCommand' 6
request7 >
,> ?
CancellationToken@ Q
cancellationTokenR c
)c d
{ 	
var   
room   
=   
_mapper   
.   
Map   "
<  " #
Room  # '
>  ' (
(  ( )
request  ) 0
)  0 1
;  1 2
room!! 
.!! 
Id!! 
=!! 
Guid!! 
.!! 
NewGuid!! "
(!!" #
)!!# $
;!!$ %
var## 
response## 
=## 
await##  
_roomRepository##! 0
.##0 1

CreateRoom##1 ;
(##; <
room##< @
)##@ A
;##A B
if%% 
(%% 
response%% 
?%% 
.%% 

StatusCode%% $
==%%% '
System%%( .
.%%. /
Net%%/ 2
.%%2 3
HttpStatusCode%%3 A
.%%A B
Created%%B I
)%%I J
{&& 
_logger'' 
.'' 
LogInformation'' &
(''& '
$"''' )
$str'') :
{'': ;
room''; ?
.''? @
Id''@ B
}''B C
$str''C V
"''V W
)''W X
;''X Y
if)) 
()) 
room)) 
.)) 

RoomConfig)) #
.))# $

VotingType))$ .
==))/ 1

VotingType))2 <
.))< =

FreeVoting))= G
)))G H
CreateStory** 
(**  
room**  $
.**$ %
Id**% '
)**' (
;**( ) 
_notificationService,, $
.,,$ %
Notify,,% +
(,,+ ,
new,,, /
Notification,,0 <
(,,< =
success,,= D
:,,D E
true,,F J
),,J K
),,K L
;,,L M
return.. 
room.. 
... 
Id.. 
;.. 
}//  
_notificationService11  
.11  !
Notify11! '
(11' (
new11( +
Notification11, 8
(118 9
success119 @
:11@ A
false11B G
,11G H
new11I L
(11L M
$str11M q
)11q r
)11r s
)11s t
;11t u
_logger22 
.22 
LogError22 
(22 
$"22 
$str22 A
"22A B
)22B C
;22C D
return44 
Guid44 
.44 
Empty44 
;44 
}55 	
private77 
async77 
void77 
CreateStory77 &
(77& '
Guid77' +
roomId77, 2
)772 3
{88 	
var99 
story99 
=99 
new99 
Story99 !
{:: 
Id;; 
=;; 
Guid;; 
.;; 
NewGuid;; !
(;;! "
);;" #
,;;# $
Name<< 
=<< 
$str<< 
,<< 
Status== 
=== 
StoryStatus== $
.==$ %

Unfinished==% /
}>> 
;>> 
await@@ 
_mediatr@@ 
.@@ 
Send@@ 
(@@  
new@@  #
AddStoryCommand@@$ 3
{AA 
RoomIdBB 
=BB 
roomIdBB 
,BB  
StoryCC 
=CC 
storyCC 
}DD 
)DD 
;DD 
_loggerFF 
.FF 
LogInformationFF "
(FF" #
$"FF# %
$strFF% :
{FF: ;
storyFF; @
.FF@ A
IdFFA C
}FFC D
$strFFD W
"FFW X
)FFX Y
;FFY Z
}GG 	
}HH 
}II ó
uG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Commands\UpdateStatusStory\UpdateStatusStoryCommand.cs
	namespace 	
Estimatz
 
. 
API 
. 
Commands 
.  
UpdateStatusStory  1
{ 
public 

class $
UpdateStatusStoryCommand )
:* +
IRequest, 4
{ 
public 
StoryStatus 
NewStoryStatus )
{* +
get, /
;/ 0
set1 4
;4 5
}6 7
public		 
Guid		 
RoomId		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
public

 
Guid

 
StoryId

 
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
( )
} 
} ∑A
|G:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Commands\UpdateStatusStory\UpdateStatusStoryCommandHandler.cs
	namespace

 	
Estimatz


 
.

 
API

 
.

 
Commands

 
.

  
UpdateStatusStory

  1
{ 
public 

class +
UpdateStatusStoryCommandHandler 0
:1 2
IRequestHandler3 B
<B C$
UpdateStatusStoryCommandC [
>[ \
{ 
private 
readonly 
IStoryRepository )
_storyRepository* :
;: ;
private 
readonly 
IRoomRepository (
_roomRepository) 8
;8 9
private 
readonly 
ILogger  
<  !+
UpdateStatusStoryCommandHandler! @
>@ A
_loggerB I
;I J
private 
readonly 
INotificator % 
_notificationService& :
;: ;
public +
UpdateStatusStoryCommandHandler .
(. /
IStoryRepository/ ?
storyRepository@ O
,O P
IRoomRepositoryQ `
	roomStorya j
,j k
ILoggerl s
<s t,
UpdateStatusStoryCommandHandler	t ì
>
ì î
logger
ï õ
,
õ ú
INotificator
ù ©!
notificationService
™ Ω
)
Ω æ
{ 	
_storyRepository 
= 
storyRepository .
;. /
_roomRepository 
= 
	roomStory '
;' (
_logger 
= 
logger 
;  
_notificationService  
=! "
notificationService# 6
;6 7
} 	
public 
async 
Task 
Handle  
(  !$
UpdateStatusStoryCommand! 9
request: A
,A B
CancellationTokenC T
cancellationTokenU f
)f g
{ 	
var 
room 
= 
await 
_roomRepository ,
., -
FindRoom- 5
(5 6
request6 =
.= >
RoomId> D
)D E
;E F
if 
( 
room 
is 
not 
null 
)  
{   
var!! 

indexArray!! 
=!!  
room!!! %
.!!% &
UserStories!!& 1
.!!1 2
	FindIndex!!2 ;
(!!; <
x!!< =
=>!!> @
x!!A B
.!!B C
Id!!C E
==!!F H
request!!I P
.!!P Q
StoryId!!Q X
)!!X Y
;!!Y Z
if## 
(## 

indexArray## 
!=## !
-##" #
$num### $
)##$ %
{$$ 

RoomStatus%% 
newRoomStatus%% ,
=%%- .

RoomStatus%%/ 9
.%%9 :

NotStarted%%: D
;%%D E
bool&& 
updateRoomStatus&& )
=&&* +
false&&, 1
;&&1 2
if(( 
((( 
request(( 
.(( 
NewStoryStatus(( -
==((. 0
StoryStatus((1 <
.((< =

InProgress((= G
&&((H J
room((K O
.((O P
Status((P V
==((W Y

RoomStatus((Z d
.((d e

NotStarted((e o
)((o p
{)) 
newRoomStatus** %
=**& '

RoomStatus**( 2
.**2 3

Unfinished**3 =
;**= >
updateRoomStatus++ (
=++) *
true+++ /
;++/ 0
},, 
if.. 
(.. 
request.. 
... 
NewStoryStatus.. -
==... 0
StoryStatus..1 <
...< =
Finished..= E
&&..F H
room..I M
...M N
Status..N T
==..U W

RoomStatus..X b
...b c

Unfinished..c m
&&..n p
CanFinishRoom..q ~
(..~ 
room	.. É
,
..É Ñ
request
..Ö å
.
..å ç
StoryId
..ç î
)
..î ï
)
..ï ñ
{// 
newRoomStatus00 %
=00& '

RoomStatus00( 2
.002 3
Finished003 ;
;00; <
updateRoomStatus11 (
=11) *
true11+ /
;11/ 0
}22 
var44 
responseStory44 %
=44& '
await44( -
_storyRepository44. >
.44> ?
UpdateStatusStory44? P
(44P Q

indexArray44Q [
,44[ \
request44] d
.44d e
NewStoryStatus44e s
,44s t
request44u |
.44| }
RoomId	44} É
)
44É Ñ
;
44Ñ Ö
if66 
(66 
updateRoomStatus66 (
)66( )
{77 
var88 
responseRoom88 (
=88) *
await88+ 0
_roomRepository881 @
.88@ A
UpdateStatusRoom88A Q
(88Q R
room88R V
.88V W
Id88W Y
,88Y Z
newRoomStatus88[ h
)88h i
;88i j
if:: 
(:: 
responseRoom:: '
.::' (

StatusCode::( 2
==::3 5
HttpStatusCode::6 D
.::D E
OK::E G
)::G H
_logger;; #
.;;# $
LogInformation;;$ 2
(;;2 3
$";;3 5
$str;;5 Q
{;;Q R
request;;R Y
.;;Y Z
RoomId;;Z `
};;` a
";;a b
);;b c
;;;c d
else<< 
_logger== #
.==# $
LogError==$ ,
(==, -
$"==- /
$str==/ Z
{==Z [
request==[ b
.==b c
RoomId==c i
}==i j
"==j k
)==k l
;==l m
}>> 
if@@ 
(@@ 
responseStory@@ %
.@@% &

StatusCode@@& 0
==@@1 3
HttpStatusCode@@4 B
.@@B C
OK@@C E
)@@E F
{AA 
_loggerBB 
.BB  
LogInformationBB  .
(BB. /
$"BB/ 1
$strBB1 Q
{BBQ R
requestBBR Y
.BBY Z
StoryIdBBZ a
}BBa b
$strBBb k
{BBk l
requestBBl s
.BBs t
RoomIdBBt z
}BBz {
"BB{ |
)BB| }
;BB} ~ 
_notificationServiceCC ,
.CC, -
NotifyCC- 3
(CC3 4
newCC4 7
(CC7 8
successCC8 ?
:CC? @
trueCCA E
)CCE F
)CCF G
;CCG H
returnDD 
;DD 
}EE 
}FF 
}GG 
_loggerII 
.II 
LogErrorII 
(II 
$"II 
$strII O
{IIO P
requestIIP W
.IIW X
StoryIdIIX _
}II_ `
$strII` i
{IIi j
requestIIj q
.IIq r
RoomIdIIr x
}IIx y
"IIy z
)IIz {
;II{ | 
_notificationServiceJJ  
.JJ  !
NotifyJJ! '
(JJ' (
newJJ( +
(JJ+ ,
successJJ, 3
:JJ3 4
falseJJ5 :
)JJ: ;
)JJ; <
;JJ< =
}KK 	
privateMM 
boolMM 
CanFinishRoomMM "
(MM" #
RoomMM# '
roomMM( ,
,MM, -
GuidMM. 2
storyIdMM3 :
)MM: ;
{NN 	
boolOO 

finishRoomOO 
=OO 
trueOO "
;OO" #
foreachQQ 
(QQ 
varQQ 
storyQQ 
inQQ  
roomQQ! %
.QQ% &
UserStoriesQQ& 1
)QQ1 2
{RR 
ifSS 
(SS 
storySS 
.SS 
IdSS 
==SS 
storyIdSS  '
)SS' (
continueTT 
;TT 
ifVV 
(VV 
storyVV 
.VV 
StatusVV 
!=VV  "
StoryStatusVV# .
.VV. /
FinishedVV/ 7
)VV7 8

finishRoomWW 
=WW  
falseWW! &
;WW& '
}XX 
returnZZ 

finishRoomZZ 
;ZZ 
}[[ 	
}\\ 
}]] é
qG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Commands\UpdateStoryVote\UpdateStoryVoteCommand.cs
	namespace 	
Estimatz
 
. 
API 
. 
Commands 
.  
UpdateStoryVote  /
{ 
public 

class "
UpdateStoryVoteCommand '
:( )
IRequest* 2
{ 
public 
VotingResult 
VotingResult (
{) *
get+ .
;. /
set0 3
;3 4
}5 6
public		 
Guid		 
RoomId		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
public

 
Guid

 
StoryId

 
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
( )
} 
} â%
xG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Commands\UpdateStoryVote\UpdateStoryVoteCommandHandler.cs
	namespace 	
Estimatz
 
. 
API 
. 
Commands 
.  
UpdateStoryVote  /
{		 
public

 

class

 )
UpdateStoryVoteCommandHandler

 .
:

/ 0
IRequestHandler

1 @
<

@ A"
UpdateStoryVoteCommand

A W
>

W X
{ 
private 
readonly 
IRoomRepository (
_roomRepository) 8
;8 9
private 
readonly 
IStoryRepository )
_storyRepository* :
;: ;
private 
readonly 
ILogger  
<  !)
UpdateStoryVoteCommandHandler! >
>> ?
_logger@ G
;G H
private 
readonly 
INotificator % 
_notificationService& :
;: ;
public )
UpdateStoryVoteCommandHandler ,
(, -
IRoomRepository- <
roomRepository= K
,K L
IStoryRepositoryM ]
storyRepository^ m
,m n
INotificatoro { 
notificationService	| è
,
è ê
ILogger
ë ò
<
ò ô+
UpdateStoryVoteCommandHandler
ô ∂
>
∂ ∑
logger
∏ æ
)
æ ø
{ 	
_roomRepository 
= 
roomRepository ,
;, -
_storyRepository 
= 
storyRepository .
;. /
_logger 
= 
logger 
;  
_notificationService  
=! "
notificationService# 6
;6 7
} 	
public 
async 
Task 
Handle  
(  !"
UpdateStoryVoteCommand! 7
request8 ?
,? @
CancellationTokenA R
cancellationTokenS d
)d e
{ 	
var 
room 
= 
await 
_roomRepository ,
., -
FindRoom- 5
(5 6
request6 =
.= >
RoomId> D
)D E
;E F
if 
( 
room 
is 
not 
null 
)  
{ 
var 

indexArray 
=  
room! %
.% &
UserStories& 1
.1 2
	FindIndex2 ;
(; <
x< =
=>> @
xA B
.B C
IdC E
==F H
requestI P
.P Q
StoryIdQ X
)X Y
;Y Z
if!! 
(!! 

indexArray!! 
!=!! !
-!!" #
$num!!# $
)!!$ %
{"" 
var## 
result## 
=##  
await##! &
_storyRepository##' 7
.##7 8
UpdateStoryVote##8 G
(##G H
request##H O
.##O P
RoomId##P V
,##V W

indexArray##X b
,##b c
request##d k
.##k l
VotingResult##l x
)##x y
;##y z
if%% 
(%% 
result%% 
.%% 

StatusCode%% (
==%%) +
HttpStatusCode%%, :
.%%: ;
OK%%; =
)%%= >
{&& 
_logger'' 
.''  
LogInformation''  .
(''. /
$"''/ 1
$str''1 R
{''R S
request''S Z
.''Z [
StoryId''[ b
}''b c
$str''c l
{''l m
request''m t
.''t u
RoomId''u {
}''{ |
"''| }
)''} ~
;''~  
_notificationService(( ,
.((, -
Notify((- 3
(((3 4
new((4 7
(((7 8
success((8 ?
:((? @
true((A E
)((E F
)((F G
;((G H
return)) 
;)) 
}** 
}++ 
},, 
_logger.. 
... 
LogError.. 
(.. 
$".. 
$str.. P
{..P Q
request..Q X
...X Y
StoryId..Y `
}..` a
$str..a j
{..j k
request..k r
...r s
RoomId..s y
}..y z
"..z {
)..{ |
;..| } 
_notificationService//  
.//  !
Notify//! '
(//' (
new//( +
(//+ ,
success//, 3
://3 4
false//5 :
)//: ;
)//; <
;//< =
}00 	
}11 
}22 