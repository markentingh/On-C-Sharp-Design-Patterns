# Chain Of Responsibility
#### Part of Behavioral Design Patterns

### Use Case
When you need to process a list or chain of various types of requests and each of them may be handled by a different handler.

### Definitions
* **Handler**
  * An abstract class that contains a field that stores an instance of a successor handler with a method to set the successor. It also contains an abstract method that must be implemented by concrete classes to handle the request or pass it to the next object in the pipeline.
* **ConcreteHandler**
  * These classes are derived from the Handler class and includes a method that processes some requests and pass others to the next successor in the chain of requests.
* **Client**
  * A class that generates the request and passes it to the first handler within the chain of responsibility

### Real World Example
For example, a car repair shop would need a receptionist who answers phone calls and creates a schedule for cars that need repair. Mechanics would diagnose & repair those cars during their schedule, and managers would approve parts & labor with the client. The chain of responsibility would start with the receptionist scheduling a car for repairs, then the mechanic would diagnose the car and log which repairs will need to be made, then the manager would contact the client and approve labor and parts, the mechanic would make the repairs, the manager would approve the repaired car, and finally, the receptionist would collect fees from the client before giving them back their car.

**RepairHandler.cs**
```
using System;
using System.Collections.Generic;

//Information about repairs made to cars
public class Repairs
{
    public string clientName;
    public string carMake;
    public string carModel;
    public int carYear;
    public bool approved = false;
    public List<RepairPart> parts;
}

public class RepairPart
{
    public string partName;
    public string partId;
    public double partCost;
    public double laborCost;
}

public class RepairEventArgs : EventArgs
{
    internal Repairs Repairs { get; set; }
}

//Handler abstract class
public abstract class RepairHandler
{
    //Repairs delegate event
    public EventHandler<RepairEventArgs> Repairs;

    //Repairs event handler
    public abstract void RepairsHandler(object sender, RepairEventArgs e);

    //Constructor
    public RepairHandler()
    {
        Repairs += RepairsHandler;
    }

    //Chain Of Responsibility method
    public void ProcessRequest(Repairs repairs)
    {
        OnRequest(new RepairEventArgs { Repairs = repairs });
    }

    //Invoke Repairs event
    public virtual void OnRequest(RepairEventArgs e)
    {
        Repairs?.Invoke(this, e);
    }

    //Get/Set next successor(s)
    public RepairHandler SuccessorA { get; set; }
    public RepairHandler SuccessorB { get; set; }
}
```

**Receptionist.cs**
```
using System;

//ConcreteHandler class
public class Receptionist : RepairHandler
{
    public override void RepairsHandler(object sender, RepairEventArgs e)
    {
        if (e.Repairs.approved == false)
        {
            Console.WriteLine("Created new schedule for car repair");
            SuccessorA.RepairsHandler(this, e);
        }
        else
        {
            Console.WriteLine("Billed customer for work rendered");
        }
    }
}
```

**Mechanic.cs**
```
using System;

//ConcreteHandler class
public class Mechanic : RepairHandler
{
    public override void RepairsHandler(object sender, RepairEventArgs e)
    {
        if (e.Repairs.approved == false)
        {
            Console.WriteLine("Diagnosed car for repairs");
        }
        else
        {
            Console.WriteLine("Made repairs");
        }
        SuccessorA.RepairsHandler(this, e);
    }
}
```

**Manager.cs**
```
using System;

public class Manager : RepairHandler
{
    public override void RepairsHandler(object sender, RepairEventArgs e)
    {
        if (e.Repairs.approved == false)
        {
            e.Repairs.approved = true;
            Console.WriteLine("Approved parts & labor for repairs");
            SuccessorA.RepairsHandler(this, e);
        }
        else
        {
            Console.WriteLine("Approved repaired car");
            SuccessorB.RepairsHandler(this, e);
        }
    }
}
```

**Program.cs**
```
using System;
using System.Collections.Generic;

namespace Chain_Of_Responsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            //Generate request
            var receptionist = new Receptionist();
            var mechanic = new Mechanic();
            var manager = new Manager();

            //Set up chain of successors
            receptionist.SuccessorA = mechanic;
            mechanic.SuccessorA = manager;
            manager.SuccessorA = mechanic;
            manager.SuccessorB = receptionist;

            //generate car repairs
            var repairs = new Repairs()
            {
                clientName = "Robert Paulson",
                carMake = "Ford",
                carModel = "Mustang",
                carYear = 2002,
                parts = new List<RepairPart>()
                {
                    new RepairPart(){partName = "exhaust pipe", partId="EXH192383", laborCost = 75.0, partCost = 115.0 }
                }
            };

            //start chain of responsibility
            receptionist.ProcessRequest(repairs);

            //display console
            Console.ReadLine();
        }
    }
}

```

The Output should be as follows:

```
Created new schedule for car repair
Diagnosed car for repairs
Approved parts & labor for repairs
Made repairs
Approved repaired car
Billed customer for work rendered
```

In the example above, I used two Successors (A & B), since the manager had to hand-off approved costs to the mechanic and hand-off approved repairs to the receptionist. The receptionist & mechanic only used one successor, though. In a more elaborate real-world example, if the manager handed-off unapproved costs to the mechanic, the mechanic would have to find a better solution to lower costs before giving the estimated costs back to the manager. Also, if the mechanic couldn't work on a specific make & model vehicle, he would have to send the request for repairs back to the receptionist, so both successors would be utilized for the mechanic as well.

