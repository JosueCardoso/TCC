¤3
eG:\Estimatz\Estimatz.API\Estimatz.API\Infrastructure\Estimatz.API.CosmosDB\CosmosDB\CosmosDBClient.cs
	namespace 	
Estimatz
 
. 
API 
. 
CosmosDB 
.  
CosmosDB  (
{ 
public 

class 
CosmosDBClient 
:  !
ICosmosDBClient" 1
{ 
private		 
readonly		 
string		 
_endpointUri		  ,
;		, -
private

 
readonly

 
string

 
_primaryKey

  +
;

+ ,
private 
CosmosClient 
_cosmosClient *
;* +
private 
Database 
	_dataBase "
;" #
private 
	Container 

_container $
;$ %
public 
CosmosDBClient 
( 
IOptions &
<& '
CosmosConfig' 3
>3 4
configuration5 B
)B C
{ 	
_endpointUri 
= 
configuration (
.( )
Value) .
.. /
Url/ 2
;2 3
_primaryKey 
= 
configuration '
.' (
Value( -
.- .

PrimaryKey. 8
;8 9
if 
( 
_cosmosClient 
==  
null! %
)% &
{ 
var 
cosmosClientOptions '
=( )
new* -
CosmosClientOptions. A
{ 
ConnectionMode "
=# $
ConnectionMode% 3
.3 4
Gateway4 ;
,; <
AllowBulkExecution &
=' (
true) -
} 
; 
_cosmosClient 
= 
new  #
CosmosClient$ 0
(0 1
_endpointUri1 =
,= >
_primaryKey? J
,J K
cosmosClientOptionsL _
)_ `
;` a
if 
( 
_cosmosClient  
is! #
not$ '
null( ,
), -
{ 
	_dataBase   
=   
_cosmosClient    -
.  - .
GetDatabase  . 9
(  9 :
configuration  : G
.  G H
Value  H M
.  M N
Database  N V
)  V W
;  W X
if"" 
("" 
	_dataBase""  
is""! #
not""$ '
null""( ,
)"", -
{## 

_container$$ "
=$$# $
	_dataBase$$% .
.$$. /
GetContainer$$/ ;
($$; <
configuration$$< I
.$$I J
Value$$J O
.$$O P
	Container$$P Y
)$$Y Z
;$$Z [
}%% 
}&& 
}'' 
}(( 	
public** 
async** 
Task** 
<** 
ItemResponse** &
<**& '
T**' (
>**( )
>**) *
CreateItemAsync**+ :
<**: ;
T**; <
>**< =
(**= >
T**> ?
item**@ D
)**D E
{++ 	
return,, 
await,, 

_container,, #
.,,# $
CreateItemAsync,,$ 3
(,,3 4
item,,4 8
),,8 9
;,,9 :
}-- 	
public// 
IOrderedQueryable//  
<//  !
T//! "
>//" #
GetItemQueryable//$ 4
<//4 5
T//5 6
>//6 7
(//7 8
)//8 9
{00 	
return11 

_container11 
.11  
GetItemLinqQueryable11 2
<112 3
T113 4
>114 5
(115 6*
allowSynchronousQueryExecution116 T
:11T U
true11V Z
)11Z [
;11[ \
}22 	
public44 
async44 
Task44 
<44 
ItemResponse44 &
<44& '
T44' (
>44( )
>44) *

DeleteItem44+ 5
<445 6
T446 7
>447 8
(448 9
string449 ?
id44@ B
,44B C
string44D J
partitionKeyString44K ]
)44] ^
{55 	
PartitionKey66 
partitionKey66 %
=66& '
new66( +
PartitionKey66, 8
(668 9
partitionKeyString669 K
)66K L
;66L M
return77 
await77 

_container77 #
.77# $
DeleteItemAsync77$ 3
<773 4
T774 5
>775 6
(776 7
id777 9
,779 :
partitionKey77; G
)77G H
;77H I
}88 	
public:: 
async:: 
Task:: 
<:: 
ItemResponse:: &
<::& '
T::' (
>::( )
>::) *
GetItem::+ 2
<::2 3
T::3 4
>::4 5
(::5 6
string::6 <
id::= ?
,::? @
string::A G
partitionKeyString::H Z
)::Z [
{;; 	
PartitionKey<< 
partitionKey<< %
=<<& '
new<<( +
PartitionKey<<, 8
(<<8 9
partitionKeyString<<9 K
)<<K L
;<<L M
return== 
await== 

_container== #
.==# $
ReadItemAsync==$ 1
<==1 2
T==2 3
>==3 4
(==4 5
id==5 7
,==7 8
partitionKey==9 E
)==E F
;==F G
}>> 	
public@@ 
async@@ 
Task@@ 
<@@ 
ItemResponse@@ &
<@@& '
T@@' (
>@@( )
>@@) *
PatchUpdate@@+ 6
<@@6 7
T@@7 8
>@@8 9
(@@9 :
string@@: @
id@@A C
,@@C D
string@@E K
partitionKeyString@@L ^
,@@^ _
List@@` d
<@@d e
PatchOperation@@e s
>@@s t
patchOperations	@@u „
)
@@„ …
{AA 	
PartitionKeyBB 
partitionKeyBB %
=BB& '
newBB( +
PartitionKeyBB, 8
(BB8 9
partitionKeyStringBB9 K
)BBK L
;BBL M
returnCC 
awaitCC 

_containerCC #
.CC# $
PatchItemAsyncCC$ 2
<CC2 3
TCC3 4
>CC4 5
(CC5 6
idCC6 8
,CC8 9
partitionKeyCC: F
,CCF G
patchOperationsCCH W
)CCW X
;CCX Y
}DD 	
}EE 
}FF ¶
fG:\Estimatz\Estimatz.API\Estimatz.API\Infrastructure\Estimatz.API.CosmosDB\CosmosDB\ICosmosDBClient.cs
	namespace 	
Estimatz
 
. 
API 
. 
CosmosDB 
.  
CosmosDB  (
{ 
public 

	interface 
ICosmosDBClient $
{ 
Task 
< 
ItemResponse 
< 
T 
> 
> 
CreateItemAsync -
<- .
T. /
>/ 0
(0 1
T1 2
item3 7
)7 8
;8 9
IOrderedQueryable 
< 
T 
> 
GetItemQueryable -
<- .
T. /
>/ 0
(0 1
)1 2
;2 3
Task		 
<		 
ItemResponse		 
<		 
T		 
>		 
>		 

DeleteItem		 (
<		( )
T		) *
>		* +
(		+ ,
string		, 2
id		3 5
,		5 6
string		7 =
partitionKeyString		> P
)		P Q
;		Q R
Task

 
<

 
ItemResponse

 
<

 
T

 
>

 
>

 
GetItem

 %
<

% &
T

& '
>

' (
(

( )
string

) /
id

0 2
,

2 3
string

4 :
partitionKeyString

; M
)

M N
;

N O
Task 
< 
ItemResponse 
< 
T 
> 
> 
PatchUpdate )
<) *
T* +
>+ ,
(, -
string- 3
id4 6
,6 7
string8 >
partitionKeyString? Q
,Q R
ListS W
<W X
PatchOperationX f
>f g
patchOperationsh w
)w x
;x y
} 
} 