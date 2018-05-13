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
