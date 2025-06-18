¬
\G:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Notifications\INotificator.cs
	namespace 	
Estimatz
 
. 
API 
. 
Notifications $
{ 
public 

	interface 
INotificator !
{ 
bool 
HasNotification 
{ 
get "
;" #
}$ %
bool 
HasMessages 
{ 
get 
; 
}  !
bool		 
IsSucess		 
{		 
get		 
;		 
}		 
List

 
<

 
Message

 
>

 
Messages

 
{

  
get

! $
;

$ %
}

& '
void 
Notify 
( 
Notification  
notification! -
)- .
;. /
} 
} Ø
dG:\Estimatz\Estimatz.API\Estimatz.API\Application\Estimatz.API.Notifications\NotificationsService.cs
	namespace 	
Estimatz
 
. 
API 
. 
Notifications $
{ 
public 

class  
NotificationsService %
:& '
INotificator( 4
{ 
private 
Notification 
_notification *
;* +
public		 
List		 
<		 
Message		 
>		 
Messages		 %
=>		& (
HasNotification		) 8
?		9 :
_notification		; H
.		H I
Messages		I Q
:		R S
new		T W
List		X \
<		\ ]
Message		] d
>		d e
(		e f
)		f g
;		g h
public

 
bool

 
HasNotification

 #
=>

$ &
_notification

' 4
is

5 7
not

8 ;
null

< @
;

@ A
public 
bool 
HasMessages 
=>  "
HasNotification# 2
&&3 5
_notification6 C
.C D
MessagesD L
.L M
AnyM P
(P Q
)Q R
;R S
public 
bool 
IsSucess 
=> 
HasNotification  /
&&0 2
_notification3 @
.@ A
SuccessA H
;H I
public 
void 
Notify 
( 
Notification '
notification( 4
)4 5
{ 	
_notification 
= 
notification (
;( )
} 	
} 
} 