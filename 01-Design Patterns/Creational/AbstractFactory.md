# Abstract Factory
#### Part of Creational Design Patterns
Creates an instance of several families of classes.

### Use Case
When there is a need to create families of related objects without specifying their concrete classes.

For example, when structuring the communication performed between different departments & types of employees within a company, this would be a good solution to do so.

### Definitions

* **AbstractFactory** (e.g. DepartmentFactory)
  * Declares an interface for operations that create abstract products
* **ConcereteFactory** (e.g. SalesFactory, MarketingFactory)
  * Implements the operations to create concrete product objects
* **AbstractProduct** (e.g. Employee, Manager)
  * Declares an interface for a type of product object
* **Product** (e.g. Salesman, SalesManager, MarketingAnalyst, MarketingManager)
  * Defines a product object to be created by the corresponding concrete factory
  * Implements the *AbstractProduct* interface
* **Client** (e.g. Department)
  * Uses interfaces declared by *AbstractFactory* and *AbstractProduct* classes

### Real World Example
```
using System;

public class Program
{
    public static void Main()
    {
        //Abstract Factory for Sales Department
        DepartmentFactory SalesFactory = new SalesFactory();
        Department SalesTeam = new Department(SalesFactory);

        //Abstract Factory for Marketing Department
        DepartmentFactory MarketingFactory = new MarketingFactory();
        Department MarketingTeam = new Department(MarketingFactory);
    }
}

//AbstractFactory 
abstract class DepartmentFactory
{
    public abstract Employee CreateEmployee();
    public abstract Manager CreateManager();
}

//ConcreteFactory
class SalesFactory: DepartmentFactory
{
    public override Employee CreateEmployee()
    {
        return new Salesman();
    }

    public override Manager CreateManager()
    {
        return new SalesManager();
    }
}

class MarketingFactory : DepartmentFactory
{
    public override Employee CreateEmployee()
    {
        return new MarketingAnalyst();
    }

    public override Manager CreateManager()
    {
        return new MarketingManager();
    }
}

//AbstractProduct
abstract class Employee { }

abstract class Manager
{
    public abstract void Interact(Employee employee);
}

//Product
class Salesman : Employee { }

class SalesManager : Manager
{
    public override void Interact(Employee employee)
    {
        Console.WriteLine(this.GetType().Name + " interacts with " + employee.GetType().Name);
    }
}


class MarketingAnalyst : Employee { }

class MarketingManager : Manager
{
    public override void Interact(Employee employee)
    {
        Console.WriteLine(this.GetType().Name + " interacts with " + employee.GetType().Name);
    }
}

//Client
class Department
{
    private Employee _employee;
    private Manager _manager;

    public Department(DepartmentFactory factory)
    {
        _employee = factory.CreateEmployee();
        _manager = factory.CreateManager();
    }

    public void Run()
    {
        _manager.Interact(_employee);
    }
}


```