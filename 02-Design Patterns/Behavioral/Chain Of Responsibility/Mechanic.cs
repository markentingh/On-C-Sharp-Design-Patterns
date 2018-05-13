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
