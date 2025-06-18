•
iG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Queries\GetAllRooms\GetSimpleRoomsQuery.cs
	namespace 	
Estimatz
 
. 
API 
. 
Queries 
. 
GetAllRooms *
{ 
public 

class 
GetSimpleRoomsQuery $
:% &
IRequest' /
</ 0
List0 4
<4 5

SimpleRoom5 ?
>? @
>@ A
{ 
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
 å
pG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Queries\GetAllRooms\GetSimpleRoomsQueryHandler.cs
	namespace 	
Estimatz
 
. 
API 
. 
Queries 
. 
GetAllRooms *
{ 
public		 

class		 &
GetSimpleRoomsQueryHandler		 +
:		, -
IRequestHandler		. =
<		= >
GetSimpleRoomsQuery		> Q
,		Q R
List		S W
<		W X

SimpleRoom		X b
>		b c
>		c d
{

 
private 
readonly 
IRoomRepository (
_roomRepository) 8
;8 9
private 
readonly 
INotificator % 
_notificationService& :
;: ;
private 
readonly 
ILogger  
<  !&
GetSimpleRoomsQueryHandler! ;
>; <
_logger= D
;D E
public &
GetSimpleRoomsQueryHandler )
() *
IRoomRepository* 9
roomRepository: H
,H I
INotificatorJ V
notificatorW b
,b c
ILoggerd k
<k l'
GetSimpleRoomsQueryHandler	l Ü
>
Ü á
logger
à é
)
é è
{ 	
_roomRepository 
= 
roomRepository ,
;, - 
_notificationService  
=! "
notificator# .
;. /
_logger 
= 
logger 
; 
} 	
public 
Task 
< 
List 
< 

SimpleRoom #
># $
>$ %
Handle& ,
(, -
GetSimpleRoomsQuery- @
requestA H
,H I
CancellationTokenJ [
cancellationToken\ m
)m n
{ 	
var 
allRoomsByUserId  
=! "
new# &
List' +
<+ ,

SimpleRoom, 6
>6 7
(7 8
)8 9
;9 :
try 
{ 
allRoomsByUserId  
=! "
_roomRepository# 2
.2 3
GetAllRoomByUserId3 E
(E F
requestF M
.M N
UserIdN T
)T U
;U V 
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
;? @
_logger 
. 
LogInformation &
(& '
$"' )
$str) L
{L M
requestM T
.T U
UserIdU [
}[ \
$str\ r
"r s
)s t
;t u
} 
catch   
(   
	Exception   
ex   
)    
{!!  
_notificationService"" $
.""$ %
Notify""% +
(""+ ,
new"", /
(""/ 0
success""0 7
:""7 8
false""9 >
,""> ?
new""@ C
(""C D
ex""D F
.""F G
Message""G N
)""N O
)""O P
)""P Q
;""Q R
_logger## 
.## 
LogError##  
(##  !
$"##! #
$str### W
{##W X
request##X _
.##_ `
UserId##` f
}##f g
$str##g o
{##o p
ex##p r
.##r s
Message##s z
}##z {
"##{ |
)##| }
;##} ~
}$$ 
return&& 
Task&& 
.&& 

FromResult&& "
(&&" #
allRoomsByUserId&&# 3
)&&3 4
;&&4 5
}'' 	
}(( 
})) ¶
jG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Queries\GetIndicators\GetIndicatorsQuery.cs
	namespace 	
Estimatz
 
. 
API 
. 
Queries 
. 
GetIndicators ,
{ 
public 

class 
GetIndicatorsQuery #
:$ %
IRequest& .
<. /
List/ 3
<3 4
	Indicator4 =
>= >
>> ?
{ 
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
 í0
qG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Queries\GetIndicators\GetIndicatorsQueryHandler.cs
	namespace 	
Estimatz
 
. 
API 
. 
Queries 
. 
GetIndicators ,
{ 
public		 

class		 %
GetIndicatorsQueryHandler		 *
:		+ ,
IRequestHandler		- <
<		< =
GetIndicatorsQuery		= O
,		O P
List		Q U
<		U V
	Indicator		V _
>		_ `
>		` a
{

 
private 
readonly 
IRoomRepository (
_roomRepository) 8
;8 9
private 
readonly 
INotificator % 
_notificationService& :
;: ;
private 
readonly 
ILogger  
<  !%
GetIndicatorsQueryHandler! :
>: ;
_logger< C
;C D
public %
GetIndicatorsQueryHandler (
(( )
IRoomRepository) 8
roomRepository9 G
,G H
INotificatorI U
notificationServiceV i
,i j
ILoggerk r
<r s&
GetIndicatorsQueryHandler	s å
>
å ç
logger
é î
)
î ï
{ 	
_roomRepository 
= 
roomRepository ,
;, - 
_notificationService  
=! "
notificationService# 6
;6 7
_logger 
= 
logger 
; 
} 	
public 
Task 
< 
List 
< 
	Indicator "
>" #
># $
Handle% +
(+ ,
GetIndicatorsQuery, >
request? F
,F G
CancellationTokenH Y
cancellationTokenZ k
)k l
{ 	
var 

indicators 
= 
new  
List! %
<% &
	Indicator& /
>/ 0
(0 1
)1 2
;2 3
try 
{ 
var 
allRoomsByUserId $
=% &
_roomRepository' 6
.6 7
GetAllRoomByUserId7 I
(I J
requestJ Q
.Q R
UserIdR X
)X Y
;Y Z
if 
( 
allRoomsByUserId $
is% '
not( +
null, 0
&&1 3
allRoomsByUserId4 D
.D E
AnyE H
(H I
)I J
)J K
{ 
int   
amountOfStory   %
=  & '
$num  ( )
;  ) *
var!! 
amountOfRoom!! $
=!!% &
allRoomsByUserId!!' 7
.!!7 8
Count!!8 =
(!!= >
)!!> ?
;!!? @
allRoomsByUserId"" $
.""$ %
ForEach""% ,
("", -
x""- .
=>""/ 1
amountOfStory""2 ?
+=""@ B
x""C D
.""D E
TotalCountStories""E V
)""V W
;""W X
var## 
averageStoryByRoom## *
=##+ ,
amountOfStory##- :
/##; <
amountOfRoom##= I
;##I J

indicators%% 
.%% 
Add%% "
(%%" #
new%%# &
	Indicator%%' 0
{%%1 2
Description%%3 >
=%%? @
$str%%A V
,%%V W
Value%%X ]
=%%^ _
amountOfRoom%%` l
.%%l m
ToString%%m u
(%%u v
)%%v w
}%%x y
)%%y z
;%%z {

indicators&& 
.&& 
Add&& "
(&&" #
new&&# &
	Indicator&&' 0
{&&1 2
Description&&3 >
=&&? @
$str&&A Z
,&&Z [
Value&&\ a
=&&b c
amountOfStory&&d q
.&&q r
ToString&&r z
(&&z {
)&&{ |
}&&} ~
)&&~ 
;	&& Ä

indicators'' 
.'' 
Add'' "
(''" #
new''# &
	Indicator''' 0
{''1 2
Description''3 >
=''? @
$str''A ^
,''^ _
Value''` e
=''f g
averageStoryByRoom''h z
.''z {
ToString	''{ É
(
''É Ñ
)
''Ñ Ö
}
''Ü á
)
''á à
;
''à â
}((  
_notificationService** $
.**$ %
Notify**% +
(**+ ,
new**, /
(**/ 0
success**0 7
:**7 8
true**9 =
)**= >
)**> ?
;**? @
_logger++ 
.++ 
LogInformation++ &
(++& '
$"++' )
$str++) f
{++f g
request++g n
.++n o
UserId++o u
}++u v
"++v w
)++w x
;++x y
return,, 
Task,, 
.,, 

FromResult,, &
(,,& '

indicators,,' 1
),,1 2
;,,2 3
}-- 
catch.. 
(.. 
	Exception.. 
ex.. 
).. 
{//  
_notificationService00 $
.00$ %
Notify00% +
(00+ ,
new00, /
(00/ 0
success000 7
:007 8
false009 >
,00> ?
new00? B
(00B C
$"00C E
$str00E y
{00y z
request	00z Å
.
00Å Ç
UserId
00Ç à
}
00à â
$str
00â ë
{
00ë í
ex
00í î
.
00î ï
Message
00ï ú
}
00ú ù
"
00ù û
)
00û ü
)
00ü †
)
00† °
;
00° ¢
_logger11 
.11 
LogInformation11 &
(11& '
$"11' )
$str11) f
{11f g
request11g n
.11n o
UserId11o u
}11u v
"11v w
)11w x
;11x y
return33 
Task33 
.33 

FromResult33 &
(33& '

indicators33' 1
)331 2
;332 3
}44 
}55 	
}66 
}77 Ò
^G:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Queries\GetRoom\GetRoomQuery.cs
	namespace 	
Estimatz
 
. 
API 
. 
Queries 
. 
GetRoom &
{ 
public 

class 
GetRoomQuery 
: 
IRequest  (
<( )
Room) -
>- .
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
public		 
Guid		 
UserId		 
{		 
get		  
;		  !
set		" %
;		% &
}		' (
}

 
} ù
eG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Queries\GetRoom\GetRoomQueryHandler.cs
	namespace 	
Estimatz
 
. 
API 
. 
Queries 
. 
GetRoom &
{ 
public		 

class		 
GetRoomQueryHandler		 $
:		% &
IRequestHandler		' 6
<		6 7
GetRoomQuery		7 C
,		C D
Room		E I
>		I J
{

 
private 
readonly 
IRoomRepository (
_roomRepository) 8
;8 9
private 
readonly 
INotificator % 
_notificationService& :
;: ;
private 
readonly 
ILogger  
<  !
GetRoomQueryHandler! 4
>4 5
_logger6 =
;= >
public 
GetRoomQueryHandler "
(" #
IRoomRepository# 2
roomRepository3 A
,A B
INotificatorC O
notificationServiceP c
,c d
ILoggere l
<l m 
GetRoomQueryHandler	m Ä
>
Ä Å
logger
Ç à
)
à â
{ 	
_roomRepository 
= 
roomRepository ,
;, - 
_notificationService  
=! "
notificationService# 6
;6 7
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Room 
> 
Handle  &
(& '
GetRoomQuery' 3
request4 ;
,; <
CancellationToken= N
cancellationTokenO `
)` a
{ 	
var 
room 
= 
await 
_roomRepository ,
., -
FindRoom- 5
(5 6
request6 =
.= >
RoomId> D
)D E
;E F
if 
( 
room 
!= 
null 
) 
{  
_notificationService $
.$ %
Notify% +
(+ ,
new, /
(/ 0
success0 7
:7 8
true9 =
)= >
)> ?
;? @
_logger 
. 
LogInformation &
(& '
$"' )
$str) .
{. /
room/ 3
.3 4
Id4 6
}6 7
$str7 O
"O P
)P Q
;Q R
return 
room 
; 
}--  
_notificationService//  
.//  !
Notify//! '
(//' (
new//( +
(//+ ,
success//, 3
://3 4
false//5 :
,//: ;
new//< ?
(//? @
$str//@ c
)//c d
)//d e
)//e f
;//f g
_logger00 
.00 
LogError00 
(00 
$"00 
$str00 J
{00J K
request00K R
.00R S
UserId00S Y
}00Y Z
$str00Z c
{00c d
request00d k
.00k l
RoomId00l r
}00r s
$str00s t
"00t u
)00u v
;00v w
return11 
new11 
Room11 
(11 
)11 
;11 
}22 	
}33 
}44 ˜
`G:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Queries\GetStory\GetStoryQuery.cs
	namespace 	
Estimatz
 
. 
API 
. 
Queries 
. 
GetStory '
{ 
public 

class 
GetStoryQuery 
:  
IRequest! )
<) *
Story* /
>/ 0
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
public		 
Guid		 
StoryId		 
{		 
get		 !
;		! "
set		# &
;		& '
}		( )
}

 
} ÿ
gG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Queries\GetStory\GetStoryQueryHandler.cs
	namespace 	
Estimatz
 
. 
API 
. 
Queries 
. 
GetStory '
{ 
public		 

class		  
GetStoryQueryHandler		 %
:		& '
IRequestHandler		( 7
<		7 8
GetStoryQuery		8 E
,		E F
Story		G L
>		L M
{

 
private 
readonly 
IRoomRepository (
_roomRepository) 8
;8 9
private 
readonly 
INotificator % 
_notificationService& :
;: ;
private 
readonly 
ILogger  
<  ! 
GetStoryQueryHandler! 5
>5 6
_logger7 >
;> ?
public  
GetStoryQueryHandler #
(# $
IRoomRepository$ 3
roomRepository4 B
,B C
INotificatorD P
notificatorQ \
,\ ]
ILogger^ e
<e f 
GetStoryQueryHandlerf z
>z {
logger	| Ç
)
Ç É
{ 	
_roomRepository 
= 
roomRepository ,
;, - 
_notificationService  
=! "
notificator# .
;. /
_logger 
= 
logger 
; 
} 	
public 
async 
Task 
< 
Story 
>  
Handle! '
(' (
GetStoryQuery( 5
request6 =
,= >
CancellationToken? P
cancellationTokenQ b
)b c
{ 	
var 
room 
= 
await 
_roomRepository ,
., -
FindRoom- 5
(5 6
request6 =
.= >
RoomId> D
)D E
;E F
if 
( 
room 
!= 
null 
) 
{ 
var 
story 
= 
room  
.  !
UserStories! ,
., -
Find- 1
(1 2
x2 3
=>4 6
x7 8
.8 9
Id9 ;
==< >
request? F
.F G
StoryIdG N
)N O
;O P
if 
( 
story 
!= 
null  
)  !
{  
_notificationService   (
.  ( )
Notify  ) /
(  / 0
new  0 3
(  3 4
success  4 ;
:  ; <
true  = A
)  A B
)  B C
;  C D
_logger!! 
.!! 
LogInformation!! *
(!!* +
$"!!+ -
$str!!- 6
{!!6 7
story!!7 <
.!!< =
Id!!= ?
}!!? @
$str!!@ X
"!!X Y
)!!Y Z
;!!Z [
return"" 
story""  
;""  !
}## 
}$$  
_notificationService&&  
.&&  !
Notify&&! '
(&&' (
new&&( +
(&&+ ,
success&&, 3
:&&3 4
false&&5 :
,&&: ;
new&&< ?
(&&? @
$str&&@ c
)&&c d
)&&d e
)&&e f
;&&f g
_logger'' 
.'' 
LogError'' 
('' 
$"'' 
$str'' K
{''K L
request''L S
.''S T
RoomId''T Z
}''Z [
$str''[ g
{''g h
request''h o
.''o p
StoryId''p w
}''w x
"''x y
)''y z
;''z {
return(( 
new(( 
Story(( 
((( 
)(( 
;(( 
})) 	
}** 
}++ 