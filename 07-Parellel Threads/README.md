# Parellel Threads

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