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
