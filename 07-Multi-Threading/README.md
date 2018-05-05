# Multi-Threading Techniques

## Task-Based Asyncronous Pattern (TAP)
This pattern is used to represent arbitrary asynchronous operations. TAP is the recommended asynchronous design pattern for modern development.

Asynchronous methods in TAP should include the `Async` suffix after the operation name within a method name, such as `GetAsync` in a `Get` method. If a class already contains a method with the `Async` suffix, use the suffix `TaskAsync` instead. For example, `GetAsync` and `GetTaskAsync`.

A TAP method returns either `System.Threading.Tasks.Task` or `System.Threading.Tasks.Task<TResult>`, based on whether the corresponding synchronous method returns void or a type `TResult`.

The parameters or arguments of a TAP method should reflect the parameters of its synchronous counterpart, and they should be in the same order. Do not use `out` or `ref` modifiers within your arguments, though. Instead, return a `TResult` returned by `Task<TResult>` using a `Tuple` or a custom data structure if you need to return multiple outputs.

For every asynchronous method, the .NET CLR will generate a **state machine** during compile-time.

You should write asynchronous code for two purposes:

* **I/O Bound Code**. When you need to do an input / output operation, such as downloading large files from the network, reading large files, or accessing a database resource. In this case, you would use the `await` keyword on an async method that returns a `Task` or `Task<TResult>`.
* **CPU Bound Code**. When you need to run a very large calculation, such as calculating the most efficient route on a map. In this case, you would use the `await` keyword on an `async` method that will run on a background thread using `Task.Run()`

### Async Method
When converting a method into an asynchronous method, you should make the following changes:
* The method definition should include the `async` modifier. This will allow you to use the `await` modifier within the method body
* Change the return type to either `void`, `Task`, or `Task<T>`, where `T` is the return data type. For example: `public async Task<string> UpdateProgressAsync() {}`
	Also, It is recommended to use `Task` instead of `void`. 
* Use the `await` modifier when executing methods within the body of your asynchronous method, or else the execution of those methods will be considered synchronous

### I/O Bound Task
In .NET framework 4.5, there are many built-in libraries that support async / await, such as `HttpClient`, `StreamReader`, & `StreamWriter`. In the following example, we will download a file from the internet asynchronously.

```
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IO_Bound_Task
{
    class Program
    {
        private const string URL = "https://github.com/Datasilk/Saber/";

        static void Main(string[] args)
        {
            DoCalc();
            var download = FileDownloader.DownloadFileAsync(URL);
            DoOtherCalc();
            Console.ReadLine();
        }

        public static void DoCalc()
        {
            Console.WriteLine("1. Calculating synchronously");
        }

        static void DoOtherCalc()
        {
            Console.WriteLine("6. Calculating stuff while downloading...");
        }
    }

    public static class FileDownloader
    {
        public static async Task DownloadFileAsync(string url)
        {
            using (var httpClient = new HttpClient())
            {
                Console.WriteLine("2. Begin downloading file...");
                string result = await httpClient.GetStringAsync(url);
                Console.WriteLine("3. Finished downloading file!");
                Console.WriteLine($"4. Reading length of http Get for {url}");
                Console.WriteLine($"5. {result.Length} character(s)");
            }
        }
    }
}
```

The Output should be as follows:

```
1. Calculating synchronously
2. Begin downloading file...
6. Calculating stuff while downloading...
3. Finished downloading file!
4. Reading length of http Get for https://github.com/Datasilk/Saber/
5. 74719 character(s)
```

> NOTE: You can open the above example project solution **IO-Bound-Task.sln**

### CPU Bound Task
In .NET framework 4.5, there are many built-in libraries that support async / await, such as `HttpClient`, `StreamReader`, & `StreamWriter`. In the following example, we will download a file from the internet asynchronously.

```
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
```

The Output should be as follows:

```
1. Creating large data asynchronously...
3. Calculating stuff synchronously
2. Finished creating large data...
4. Calculating large data asynchronously...
6. Calculating other stuff while calculating large data...
5. Finished calculating large data!
6. large data calculation = 503045.039670563
```

Notice that `data.Result` is passed into the asynchronous method `CalcLargeDataAsync`, and `results.Result` is used when writing to the console at the end of the `Main` method. The `Task.Result` property waits until the asynchronous method is complete before returning a value, similar to `Task.Wait()`.


### Task.Yield()
Use `Task.Yield()` to immediately return the result of a given Task, while scheduling any code below the `Yield` to the CPU asynchronously. This creates the chance for other tasks to be scheduled to the CPU first. 

```
static void DoNotYeild()
{
    DoCalcsWithResult();
    DoMoreCalcs();
    // Returns after DoMoreCalcs() finishes
}

static async Task YeildAsync()
{
    DoCalcsWithResult();
    await Task.Yield(); // Returns immediately
    DoMoreCalcs();
}
```

## Parellel Threads

### PLINQ
Using parallel threads in LINQ may speed up your calculations in some situations, but in others, it may slow them down. Let's take a look at an example of parallel LINQ.

```
using System;
using System.Linq;

class Program
{
	public void Main(string[] args)
	{
		var nums = new[]{ 50, 25, 42, 101, 290, 777 };
		var query = from x in nums.AsParallel() select Calc(x);
	}

	private int Calc(var z)
	{
		int count = z;
		for(var y = 1; y < 1000; y++){
			count += y;
		}
		return count;
	}
}
```