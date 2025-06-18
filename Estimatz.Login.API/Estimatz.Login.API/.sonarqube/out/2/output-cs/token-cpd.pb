Î
ÇG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Infrastructure\Estimatz.Login.API.Infrastructure\TokenCache\ITokenMemoryCache.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Data !
.! "

TokenCache" ,
{ 
public 

	interface 
ITokenMemoryCache &
{ 
void 
Add 
( 
string 
key 
, 
SimpleToken (
value) .
). /
;/ 0
SimpleToken 
Get 
( 
string 
key "
)" #
;# $
void		 
Remove		 
(		 
string		 
key		 
)		 
;		  
void

 
RemoveExpiredTokens

  
(

  !
DateTime

! )
dateNow

* 1
)

1 2
;

2 3
} 
} ‘
ÇG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Infrastructure\Estimatz.Login.API.Infrastructure\TokenCache\TokenCacheCleaner.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Data !
.! "

TokenCache" ,
{ 
public 

class 
TokenCacheCleaner "
:# $
IHostedService% 3
,3 4
IDisposable5 @
{ 
private 
Timer 
_timer 
; 
private 
readonly 
ITokenMemoryCache *
_tokenCache+ 6
;6 7
public

 
TokenCacheCleaner

  
(

  !
ITokenMemoryCache

! 2

tokenCache

3 =
)

= >
{ 	
_tokenCache 
= 

tokenCache $
;$ %
} 	
public 
Task 

StartAsync 
( 
CancellationToken 0
stoppingToken1 >
)> ?
{ 	
_timer 
= 
new 
Timer 
( 
DoWork %
,% &
null' +
,+ ,
TimeSpan- 5
.5 6
Zero6 :
,: ;
TimeSpan< D
.D E
FromMinutesE P
(P Q
$numQ R
)R S
)S T
;T U
return 
Task 
. 
CompletedTask %
;% &
} 	
private 
void 
DoWork 
( 
object "
state# (
)( )
{ 	
_tokenCache 
. 
RemoveExpiredTokens +
(+ ,
DateTime, 4
.4 5
UtcNow5 ;
); <
;< =
} 	
public 
Task 
	StopAsync 
( 
CancellationToken /
stoppingToken0 =
)= >
{ 	
_timer 
? 
. 
Change 
( 
Timeout "
." #
Infinite# +
,+ ,
$num- .
). /
;/ 0
return 
Task 
. 
CompletedTask %
;% &
} 	
public!! 
void!! 
Dispose!! 
(!! 
)!! 
{"" 	
_timer## 
?## 
.## 
Dispose## 
(## 
)## 
;## 
}$$ 	
}%% 
}&& º
ÅG:\Estimatz\Estimatz.Login.API\Estimatz.Login.API\Infrastructure\Estimatz.Login.API.Infrastructure\TokenCache\TokenMemoryCache.cs
	namespace 	
Estimatz
 
. 
Login 
. 
API 
. 
Data !
.! "

TokenCache" ,
{ 
public 

class 
TokenMemoryCache !
:" #
ITokenMemoryCache$ 5
{ 
private  
ConcurrentDictionary $
<$ %
string% +
,+ ,
SimpleToken- 8
>8 9
_usersTokens: F
;F G
public		 
TokenMemoryCache		 
(		  
)		  !
{

 	
_usersTokens 
= 
new  
ConcurrentDictionary 3
<3 4
string4 :
,: ;
SimpleToken< G
>G H
(H I
)I J
;J K
} 	
public 
void 
Add 
( 
string 
key "
," #
SimpleToken$ /
value0 5
)5 6
{ 	
_usersTokens 
. 
TryAdd 
(  
key  #
,# $
value% *
)* +
;+ ,
} 	
public 
SimpleToken 
Get 
( 
string %
key& )
)) *
{ 	
_usersTokens 
. 
TryGetValue $
($ %
key% (
,( )
out* -
SimpleToken. 9
value: ?
)? @
;@ A
return 
value 
; 
} 	
public 
void 
Remove 
( 
string !
key" %
)% &
{ 	
_usersTokens 
. 
	TryRemove "
(" #
key# &
,& '
out( +
_, -
)- .
;. /
} 	
public 
List 
< 
SimpleToken 
>  
GetAllTokens! -
(- .
). /
=>0 2
_usersTokens3 ?
.? @
Values@ F
.F G
ToListG M
(M N
)N O
;O P
public   
void   
RemoveExpiredTokens   '
(  ' (
DateTime  ( 0
dateNow  1 8
)  8 9
{!! 	
var"" 
expiredTokens"" 
="" 
_usersTokens""  ,
."", -
Where""- 2
(""2 3
x""3 4
=>""5 7
x""8 9
.""9 :
Value"": ?
.""? @
ExpireAt""@ H
<""I J
dateNow""K R
)""R S
.""S T
ToList""T Z
(""Z [
)""[ \
;""\ ]
expiredTokens## 
.## 
ForEach## !
(##! "
x##" #
=>##$ &
Remove##' -
(##- .
x##. /
.##/ 0
Value##0 5
.##5 6
TokenString##6 A
)##A B
)##B C
;##C D
}$$ 	
}%% 
}&& 