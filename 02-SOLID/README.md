# SOLID Architecture Principles
The SOLID architecture principles help developers build multi-layered applications in C# with readable and easily maintanable code. These principles provide a path to move away from tightly coupled code to the more loosely coupled & encapsulated needs of a business.

## What Does SOLID Mean?

| | 
| --- | ---
**S** | Single Responsibility Principle (SRP)
**O** | Open Closed Principle (OSP)
**L** | Liskov Substitution Principle (LSP)
**I** | Interface Segregation Principle (ISP)
**D** | Dependency Inversion Principle (DIP)

### S: Single Responsibility Principle (SRP)
> "There should never be more than one reason for a class to change"

A class should concentrate on doing one thing, or have one responsibility. This doesn't mean it should only have one method, but instead, all the methods should relate to a single purpose, and should be cohesive.

For example, a **Shopping Cart** class should be able to keep track of a list of products the user selected to purchase, calculate the total amount for their invoice, and allow the user to remove items from their cart. It should not, however be able to know about how to access products from the database, or print an invoice to PDF, or calculate how much inventory a product has.

### O: Open Closed Principle (OSP)
> "Software entities (classes, modules, methods) should be open for extension, but closed for modification"

Design your class in such a way that all current requirements are met and new functionality can be added via inheritance when new requirements are generated.

For example, instead of changing the contents of a method by adding new `if` statements to a block of code, use inherited classes. When new requirements are generated, simply create another inherited class.

#### Don't
```
public enum BookType
{
	Fiction, NonFiction
}


public class Book
{
	public int GetCategory(BookType type)
	{
		if(type == BookType.Fiction)
		{
			return 100;
		}
		else if(type == BookType.NonFiction)	
		{
			return 200;
		}
	}
}
```

#### Do!
```
public enum BookType
{
	Fiction, NonFiction
}


public class Book
{
	public virtual int GetCategory(BookType type)
	{
		return 0;
	}
}

public class BookFiction : Book
{
	public override int GetCategory(BookType type)
	{
		return 100;
	}
}

public class BookNonFiction : Book
{
	public override int GetCategory(BookType type)
	{
		return 200;
	}
}
```

The `Book` class is now closed for any new modification, but it's open for extensions when new Book types are added to the project.

### L: Liskov Substitution Principle (LSP)
> "Objects in a program should be replaceable with instances of their sub types without altering the correctness of that program"

Derived classes should extend their base classes without changing their behavior, thus, a derived class should be perfectly substitutable for their base class in any use case.

#### Don't
```
class Program
{
	public void Main(string[] args)
	{
		Car car = new Truck();
		Console.WriteLine(car.GetWheels());
	}
}

public class Car
{
	public virtual int GetWheels()
	{
		return 4;
	}
}

public class Truck : Car
{
	public override int GetWheels()
	{
		return 6;
	}
}
```

#### Do!
```
class Program
{
	public void Main(string[] args)
	{
		Vehicle vehicle = new Truck();
		Console.WriteLine(vehicle.GetWheels());
	}
}

public abstract class Vehicle
{
	public virtual int GetWheels();
}

public class Car : Vehicle
{
	public override int GetWheels()
	{
		return 4;
	}
}

public class Truck : Vehicle
{
	public override int GetWheels()
	{
		return 6;
	}
}
```

### I: Interface Segregation Principle (ISP)
> "Clients should not be forced to implement interfaces they don't use. Instead of one fat interface, many small interfaces are preferred based on groups of methods, each one serving one sub module."

When creating classes that use an interface, each class should have a use for the methods defined within the interface. If a class does not have a need for one or more of the methods within the interface, the architecture is wrong. Instead, break up the interface into smaller interfaces that can be used when there is a need.

#### Don't
```
public interface IEmployee
{
	void ClockIn();
	void ClockOut();
	void CreateMeeting();
}

public class Manager: IEmployee { }
public class Salesman: IEmployee { }
```

In the above example, managers and salesmen share the same interface, but only managers can create meetings, so there is a problem with the architecture.

#### Do!
```
public interface IEmployee
{
	void ClockIn();
	void ClockOut();
}

public interface IManager
{
	void CreateMeeting();
}

public class Manager: IEmployee, IManager { }
public class Salesman: IEmployee { }
```

To fix this issue, I've moved the `CreateMeeting` method into a separate interface specifically made for managers.

### D: Dependency Inversion Principle (DIP)
> "High-level modules should not depend on low-level modules. Both should depend on abstractions. Abstractions should not depend on details. Details should depend on abstrations."

Keep your classes as loosely coupled as possible!

#### Don't
```
class Program
{
	public void Main(string[] args)
	{
		try
		{
			//do stuff
		}
		catch(Exception ex
		{
			Logger.LogWriteToConsole(ex.Message);
		}
	}
}

public class Logger
{
	public static void LogStoreInDb { //save log into a database }
	public static void LogStoreInFile { //save log to file }
	public static void LogWriteToConsole { //write log to console window }
}
```

In the above example, there is no abstraction layer, so we must select how to log a message by selecting a method within the `Logger` class by name.

#### Do!
```
class Program
{
	public void Main(string[] args)
	{
		try
		{
			//do stuff
		}
		catch(Exception ex
		{
			var logger = new Logger(new LogWriteToConsole());
			logger.Log(ex.Message);
		}
	}
}

public interface ILogger
{
	void Log(string message);
}

public class LogStoreInDb : ILogger {
	public void Log(string message){ //save log into a database }
}

public class LogStoreInFile : ILogger {
	public void Log(string message){ //save log to file}
}

public class LogWriteToConsole : ILogger {
	public void Log(string message){ //write log to console window}
}

public class Logger
{
	ILogger _logger;_
	public Logger(ILogger logger){
		_logger = logger;
	}
	
	public void Log(string message){
		_logger.Log(message);
	}
}
```

The example above separates the logging class by using an interface that will allow our sub classes to log a message based on the needs of the client. The client's `Main` method decides which sub class to instantiate for the `Logger` class, and the Logger class handles the process of informing the sub class to log a message. 

This is a very simple example, but in the real-world, the `Logger` class would be capable of much more than just Logging a message. It may categorize the message before sending it to the sub class, or depending on the type of message, it may automatically email an admin with details about the message, for instance. 