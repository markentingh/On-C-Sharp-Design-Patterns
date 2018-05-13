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
