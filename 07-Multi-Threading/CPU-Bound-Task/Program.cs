using System;
using System.Threading.Tasks;

namespace CPU_Bound_Task
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = CreateLargeDataAsync();
            DoCalc();
            var results = CalcLargeDataAsync(data.Result);
            DoOtherCalc();
            Console.WriteLine("7. large data calculation = " + results.Result);
            Console.ReadLine();
        }

        static async Task<double[]> CreateLargeDataAsync()
        {
            Console.WriteLine("1. Creating large data asynchronously...");
            var results = await Task.Run(() =>
            {
                var data = new double[1000];
                var rnd = new Random();
                for (var x = 0; x < data.Length; x++)
                {
                    data[x] = rnd.NextDouble() * 1000;
                }
                return data;
            });
            Console.WriteLine("2. Finished creating large data...");
            return results;
        }

        static void DoCalc()
        {
            Console.WriteLine("3. Calculating stuff synchronously");
        }

        static async Task<double> CalcLargeDataAsync(double[] data)
        {
            Console.WriteLine("4. Calculating large data asynchronously...");
            var result = await Task.Run(() =>
            {
                var results = 0.0;
                foreach (var d in data)
                {
                    results += d;
                }
                return results;
            });
            Console.WriteLine("5. Finished calculating large data!");
            return result;
        }

        static void DoOtherCalc()
        {
            Console.WriteLine("6. Calculating other stuff while calculating large data...");
        }
    }
}