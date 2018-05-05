# Delegates
From what I understand, a delegate is a anonymous reference pointer to one or more objects. A delegate doesn't care about the object it references, as long as the argument types and return type matches the delegate's.

There are three stages for using a delegate: Declaration, Instantiation, and Invocation.

```
public class Log
{
	//Delcaration
	public delegate void Handler(string message);
}

public class Logger
{
	public void LogToFile(string message)
	{
		File.WriteAllText("C:\logs\log.txt", message);
	}
}


public class Program
{
	
    public static void Main()
    {
        //Instantiation
		Log.Handler handler = new Log.Handler(Logger.LogToFile);
		
		//Invocation
		handler("Running program...");
    }
}
```

In the example above, the `Log.Handler` delegate is used to create a reference pointer to `Logger.LogToFile` with the variable `handler`. Then, the `handler` object is executed like a method with a string argument, which then executes the `Logger.LogToFile` method and writes a log to file.

### Multicasting
Delegates can be used as a reference pointer to multiple objects. Simply use the `+=` operator to add an object reference to a delegate instance. When the delegate is invoked, it will execute all functions within the list. Also, an existing reference pointer can be removed from the delegate by using the `-=` operator.

### Invoking On Separate Threads
 I've learned through my experience using delegates in WinForms that they can be used to `Invoke` methods that exist on a separate thread. Instead of executing a method that exists within a WinForm Control directly from a thread outside of the UI thread, use `myControl.Invoke(myDelegateInstance)` to execute a method within a control on the UI thread.
