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

### Value Types & Reference Types

##### Value Types
Predefined datatypes (int, double, byte), structures, and enums are all value types. 

When creating a Value Type, a single space in memory is allocated to store the value, and the variable directly holds the value. If you assign it to another variable, the value is copied to the second variable and there will be two independant copies of the value within memory. Value types can be created at compile time and can be stored in *stack* memory, but only if they are used as *temporary storages* or *local variables* that are not closed-over locals of an anonymous method or lambda expression. If a Value Type is created within an *Object* instance for example, it will exist within the *heap* along with the *Object* and will be managed by the *Garbage Collector*.

##### Reference Types
Classes, Objects, Arrays, Indexers, Interfaces, and pretty much anything that isn't a Value Type is a Reference Type.

Reference Types holds a reference, or address, in memory that points to the object, but is not the object itself. Because of this, assigning multiple variables to the same object simply creates references to the same object in memory. A Reference Type is stored within the *heap* and is handled by the *Garbage Collector*.

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