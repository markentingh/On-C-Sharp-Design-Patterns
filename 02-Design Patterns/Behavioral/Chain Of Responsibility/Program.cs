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
