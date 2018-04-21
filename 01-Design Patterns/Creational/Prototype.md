# Prototype
#### Part of Creational Design Patterns
A fully initialized instance used to be cloned

### Use Case
When you need a set of objects that are derived from a prototypical instance of another object.

### Definitions
* **Prototype** (e.g. Sheep)
  * Declares an interface for cloning itself
* **ConcretePrototype** (e.g. BlackSheep)
  * Implements an operation for cloning itself

### Real World Example
```
using System;

public class Program
{
    public static void Main()
    {
        //Create a concrete prototype instance to clone
        BlackSheep sheep = new Sheep("Barry");

        //clone concrete prototype
        BlackSheep sheep2 = (BlackSheep)sheep.Clone();

        Console.WriteLine("Cloned sheep likes being called {0}", sheep2.Name);
    }
}

//Prototype
abstract class Sheep
{
    private string _name;

    public Sheep(string name)
    {
        this._name = name;
    }

    public string Name
    {
        get { return _name; }
    }
    public abstract Sheep Clone();
}

//ConcretePrototype
class BlackSheep : Sheep
{
    public BlackSheep(string name) : base(name) { }

    public override Sheep Clone()
    {
        return (Sheep)this.MemberwiseClone();
    }
}
```