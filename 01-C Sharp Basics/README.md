# C# Basics

### Fields
A *field* is a member of a class or struct. *Objects* and *values* within a class are known as *fields*. Members of a *method* are not fields.

```
class User
{
	//fields
	public int Id;
	private DateTime created;

	public void LogIn(string email, string password)
	{
		//non-fields
		var storedHash = GetPassword(email);
		var bcrypt = new Bcrypt(10);
		bool authenticated = bcrypt.Authenticate(email + password, storedHash);
	}
}
```

### Data Types
.NET has many predefined data types with specified ranges. An alias can be used to define a data type variable, or `var` along with an initial value could also define a data type variable at compile time.

```
int x; //Int32
var y = 5; //Int32
double a; //Double (recommended use)
var b = 0.5; //Double
System.Double c; //Double (not recommended use)
```

Data Type | Alias | .NET Type| Range
---|---|---|---
Byte|byte|System.Byte|0 to 255
SByte|sbyte|System.SByte|-128 to 127
Int32|int|System.Int32|-2,147,483,648 to 2,147,483,647
UInt32|uint|System.UInt32|0 to 4294967295
Int16|short|System.Int16|-32,768 to 32,767
UInt16|ushort|System.UInt16|0 to 65,535
Int64|long|System.Int64|-9,223,372,036,854,775,808 to 9,223,372,036,854,775,807
UInt64|ulong|System.UInt64|0 to 18,446,744,073,709,551,615
Single|float|System.Single|-3.402823e38 to 3.402823e38
Double|double|System.Double|-1.79769313486232e308 to 1.79769313486232e308
Char|char|System.Char|Unicode symbols used in text
Boolean|bool|System.Boolean|True or False
Object|object|System.Object|
String|string|System.String|
Decimal|decimal|System.Decimal|(+ or -) 1.0 x 10e-28 to 7.9 x 10e28
DateTime|DateTime|System.DateTime|0:00:00am 1/1/01 to 11:59:59pm 12/31/9999

### Value Types & Reference Types

##### Value Types
Predefined datatypes (int, double, byte), structures, and enums are all value types. 

When creating a Value Type, a single space in memory is allocated to store the value, and the variable directly holds the value. If you assign it to another variable, the value is copied to the second variable and there will be two independant copies of the value within memory. Value types can be created at compile time and can be stored in *stack* memory, but only if they are used as *temporary storages* or *local variables* that are not closed-over locals of an anonymous method or lambda expression. If a Value Type is created within an *Object* instance for example, it will exist within the *heap* along with the *Object* and will be managed by the *Garbage Collector*.

##### Reference Types
Classes, Objects, Arrays, Indexers, Interfaces, and pretty much anything that isn't a Value Type is a Reference Type.

Reference Types holds a reference, or address, in memory that points to the object, but is not the object itself. Because of this, assigning multiple variables to the same object simply creates references to the same object in memory. A Reference Type is stored within the *heap* and is handled by the *Garbage Collector*.

---

### Operators

Operators that take one operand, such as the *increment* operator (`++`) or `new` are **unary operators**.

Operators that take two operands, such as arithmetic operators (`+`, `*`, `/`), are **binary operators**.

The conditional operator (`?:`) is the only **ternary operator** in C#

##### Primary Operators

Expression|Description
---|---
`x.y`|Member access
`x?.y`|Conditional member access
`f(x)`|Method / delegate invocation
`a[x]`|Array and indexer access
`a?[x]`|Conditional array and indexer access
`x++`|Post-increment
`x--`|Post-decrement
`new T(...)`|Object / delegate creation
`new T(...){...}`|Object creation with initializer
`new T[...]`|Array creation
`typof(T)`|Obtain System.Type object for T
`checked(x)`|Evaluate expression in checked context
`unchecked(x)`|Evaluate expression in unchecked context
`default(T)`|Obtain default value of type T
`delegate{}`|Anonymous method


##### Unary Operators

Expression|Description
---|---
`+x`|Indentity
`-x`|Negation
`!x`|Logical negation
`~x`|Bitwise negation
`++x`|Pre-increment
`--x`|Pre-decrement
`(T)x`|Explicit convert x to type T

##### Multiplicative Operators

Expression|Description
---|---
`*`|Multiplication
`/`|Division
`%`|Remainder

##### Additive Operators

Expression|Description
---|---
`x + y`|Addition, string concatenation, delegate / event combination
`x - y`|Subtraction, delegate / event removal

##### Shift Operators

Expression|Description
---|---
`x << y`|Shift left
`x >> y`|Shift right

##### Relational & Type Operators

Expression|Description
---|---
`x < y`|Less than
`x > y`|Greater than
`x <= y`|Less than or equal
`x >= y`|Greater than or equal
`x is T`|Returns true if x is a T, false otherwise.
`x as T`|Return x typed as T, or null if x is not a T

##### Equality Operators

Expression|Description
---|---
`x == y`|Equal
`x != y`|Not equal

##### Logical, Conditional and Null Operators

Category|Expression|Description
---|---|---
Logical AND|`x & y`|Integer bitwise AND, boolean logical AND
Logical XOR|`x ^ y`|Integer bitwise XOR, boolean logical XOR
Logical OR|`x|y`|Integer bitwise OR, boolean logical OR
Conditional AND|`x&&y`|Evaluates y only if x is true
Conditional OR|`x||y`|Evaluates y only if x is false
Null coalescing|`x ?? y`|Evaluates to y if x is null, to x otherwise
Conditional|`x ? y : z`|Evaluates to y if x is true, z if x is false

##### Assignment and Anonymous Operators

Expression|Description
---|---
`=`|Assignment
`x op= y`|Compound assignment. Supports these operators: `+=`, `-=`, `*=`, `/=`, `%=`, `&=`, `|=`, `!=`, `<<=`, `>>=`
`(T x) => y`|Anonymous function (lambda expression)

---

### Modifiers
Modifiers are used to change the declaration of any type and its members.

#### Access Modifiers
There are four different access modifiers that can determine the scope of access for a particular type or type member. These access modifiers control whether they can be used from other code in your assembly or other assemblies.

##### internal
The type or member can only be accessed by code in the assembly in which it was declared, or from within a derived class in another assembly. If no access modifier is defined, your variable will be considered *internal*.

##### public
The type or member can be accessed by any other code in the same assembly or within another assembly that references it.

##### protected
The type or member can be accessed only by code in the same class, or in a classs that is derived from that class

##### private
The type or member can only be accessed by come in the same class or struct.

---

#### Const Modifier
Use the `const` keyword to declare a constant field or local. Constant fields & locals are not variables and cannot be modified. A constant can be numbers, Boolean values, strings, or a null reference.

```
const int x = 42;
```

#### Abstract Modifier
The abstract modifier indicates that a class or method doesn't have complete implementations. An *Abstract class* contains all abstract methods. All implementations for an abstract class is done by a derived class, which inherits the abstract class.

```
public abstract class Calculator
{
	public abstract double TrackXYPos(int x, int y);
	public abstract double HitDetect (Rectangle rect1, Rectangle rect2);
}
```

An abstract method is a method without a body, and is implimented by a derived class.

```
public abstract int Calculate();
```

#### Static Modifier
Declares a member that belongs to the type itelf instead of belonging to a specific object. Static methods in one class can be called from another class without having to create an instance of the class. If a class is declared static, then all methods within the class must also be declared static.

#### Virtual & Override Modifiers
A virtual modifier allows derived classes to override members & methods of a class. In order to extend any *virtual* or *abstract* type, an override modifier is needed.

```
class Vehicle
{
	public virtual void TurnOn(string key) {}
}

class Car : Vehicle
{
	public override void TurnOn(string key) {}
}
```

#### Sealed Modifier
A sealed class cannot be inherited by any other class.

A sealed method can be used to prevent derived classes from overriding the method.

```
class Animal
{
	public virtual void MatingCall(){ }
}

class Dog : Animal
{
	public sealed override void MatingCall() { //bark }
}

class Wolf: Dog
{
	public override void MatingCall() { //howl }
}
```

In the example above, the `Dog` class seals the inherited `MatingCall` method, but the `Wolf` class tries overriding the same method, resulting in an exception. The above example therefore cannot be compiled.

#### Partial Modifier
The partial modifier allows for the definition of a class, struct, or interface to be split into multiple files. This could be useful when working with large projects.

##### Partial Class

example: `User.cs`
```
namespace MyApp
{
	partial class User
	{
		public int Id;
	}
}
```

example: `Vendor/User.cs`
```
namespace MyAppo
{
	partial class User
	{
		public int VendorId;
	}
}
```

In the example above, the application has a class to handle users, and a 3rd party vendor class extends the partial *User* class to include new functionality to the application.

There are special rules that apply to a partial type:
* All parts must use the `partial` keyword
* All the parts must have the same accessibility modifiers
* If any part is declared `abstract`, then the whole type is considered abstract
* If any part is declared `sealed`, then the whole type is considered sealed
* If any part declares a base type, then the whole type inherits that class
* Parts can specify different base interfaces, and the final type implements all the interfaces listed by all the partial declarations
* Any class, struct, or interface members declared within a partial definition are available to all the other parts

##### Partial Method
A partial method works similar to an abstract method, where the signature is defined within one part of the partial type, and the implementation is defined in another part of the type. Partial methods allow class designers to provide method hooks, similar to event handlers, that other developers my use. If the method is not used, however, the compiler removes the signature at compile time.

There are limitations to partial methods, unfortunately.

* Signatures for the partial method within both parts of the partial type must match
* The method must return *void*
* No access modifiers are allowed and therefore must be implicitly private

example: `User.cs`
```
namespace MyApp
{
	partial class User
	{
		partial void LogInSuccess(int userId);
	}
}
```

example: `Vendor/User.cs`
```
namespace MyAppo
{
	partial class User
	{
		partial void LogInSuccess(int userId)
		{
			//do stuff
		}
	}
}
```

#### ReadOnly Modifier
Declares a field that can only be assigned values as part of the declaration or within the constructor of the same class.

```
class Company
{
	private readonly int Id;

	public Company (int companyId)
	{
		Id = companyId;
	}
}

class Employee
{
	public readonly string columnName { get; } = "employee";
}