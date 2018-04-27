# Factory Method
#### Part of Creational Design Patterns
Creats an instance of several derived classes

### Use Case
When you need an interface for creating an object, but would like the subclasses to decide which class to instantiate.

### Definitions
* **Product** (e.g. Fish)
  * Defines the interface of objects the factory method creates
* **ConcreteProduct** (e.g. Salmon, Tuna)
  * Implements the *Product* interface
* **Creator** (e.g. Fisherman)
  * Declares the factory method, which returns an object of type *Product*.
  * Can also define a default implementation of the factory method that returns a default *ConcreteProduct* object
* **ConcreteCreator** (e.g. CatchSalmon, CatchTuna)
  * overrides the factory method to return an instance of a *ConcreteProduct*

### Real World Example
```
using System;

public class Program
{
    public static void Main()
    {
        //An array of creators
        Fisherman[] fishermen = new Fisherman[2];

        fishermen[0] = new CatchSalmon();
        fishermen[1] = new CatchTuna();

        //Iterate over creators to create products
        foreach (Fisherman fisherman in fishermen)
        {
            Fish fish = fisherman.Catch();
            Console.WriteLine("Caught {0}", fish.GetType().Name);
        }
    }
}


//Product
abstract class Fish { }

//ConcreteProduct
class Salmon : Fish { }
class Tuna : Fish { }

//Creator
abstract class Fisherman
{
    public abstract Fish Catch();
}

//ConcreteCreator
class CatchSalmon : Fisherman
{
    public override Fish Catch()
    {
        return new Salmon();
    }
}


class CatchTuna : Fisherman
{
    public override Fish Catch()
    {
        return new Tuna();
    }
}
```