# Builder
#### Part of Creational Design Patterns
Separates object construction from its representation

### Use Case
When you need to separate the construction of a complex object from its representation, which then allows you to use the same construction process to create defferent representations.

### Definitions

* **Builder** (e.g. VehicleBuilder)
  * Specifies an abstract interface for creating parts of a Product object
* **ConcreateBuilder** (e.g. CarBuilder, TruckBuilder)
  * constructs and assembles parts of the product by implementing the Builder interface
  * Defines and keeps track of the representation it creates
  * Provides an interface for retrieving the product
*  **Director** (e.g. Factory)
   *  Constructs an object using the *Builder* interface
* **Product** (e.g. Vehicle)
   * Represents the complex object under construction. *ConcreteBuild* builds to product's internal representation and defines the process by which it's assembled 
   * Includes classes that define the constituent parts, including interfaces for assembling the parts into the final result

### Real World Example
```
using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        //Create director and builders
        Factory factory = new Factory();
        VehicleBuilder carBuilder = new CarBuilder();
        VehicleBuilder truckBuilder = new TruckBuilder();

        //Construct two products
        factory.Construct(carBuilder);
        Vehicle car = carBuilder.ViewResult();
        car.Drive();

        factory.Construct(truckBuilder);
        Vehicle truck = truckBuilder.ViewResult();
        truck.Drive();
    }
}

//Director
class Factory
{
    public void Construct(VehicleBuilder builder)
    {
        builder.BuildFrame();
        builder.BuildInterior();
    }
}

//Builder
abstract class VehicleBuilder
{
    public abstract void BuildFrame();
    public abstract void BuildInterior();
    public abstract Vehicle ViewResult();
}

//ConcreteBuilder
class CarBuilder: VehicleBuilder
{
    private Vehicle _product = new Vehicle();

    public override void BuildFrame()
    {
        _product.Add("Small frame");
    }

    public override void BuildInterior()
    {
        _product.Add("Leather interior");
    }
}

class TruckBuilder : VehicleBuilder
{
    private Vehicle _product = new Vehicle();

    public override void BuildFrame()
    {
        _product.Add("Heavy-duty frame");
    }

    public override void BuildInterior()
    {
        _product.Add("Fabric interior");
    }
}

//Prdouct
class Vehicle
{
    private List<string> _parts = new List<string>();

    public void Add(string part)
    {
        _parts.Add(part);
    }

    public void Drive()
    {
        Console.WriteLine("\nVehicle Parts:");
        foreach (string part in _parts)
        {
            Console.WriteLine(part);
        }
    }
}
```