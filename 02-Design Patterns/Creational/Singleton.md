# Singleton
#### Part of Creational Design Patterns
A class of which only a single instance can exist.

### Use Case
A singleton should be used when you have the need for a class that can only be instantiated once globally from any point in your project.

### Non-thread-safe Example
```
using System;
public class Singleton
{
    private static Singleton instance;
    private Singleton() {}

    public static Singleton Instance
    {
        get
        {
            if( instance == null)
            {
                instance = new Singleton();
            }
            return instance;
        }
    }
}
```

* Because the instance is created inside the `Instance` property method, you can execute additional functionality before initializing the instance
* The instance is not created until an object requests an instance. In other words, this is referred to as *lazy loading* or *lazy instantiation*.

##### Cons
* Not safe for multithreaded environments. If multiple threads executes the Instance property method at the same time, more than one instance may be created if both threads detect that `instance == null`

### Static Thread-safe Example
```
using System;
public sealed class Singleton
{
    private static readonly Singleton instance = new Singleton();
    private Singleton() {}

    public static Singleton Instance
    {
        get
        {
            return instance;
        }
    }
}
```

* The class is marked **sealed** to prevent derivation, which could add instances
* The private instance is marked **readonly** and can only be assigned during static initialization or within a class constructor
* The instance is only created after the class is referenced by a call to the `Instance` property, therefore making this class *lazy*
* The CLR handles when and how the static `instance` variable is initialized, making this approach possible
* This is the most preferred approach for implementing a `Singleton` in .NET

##### Cons
* You have less control over the mechanics of instantiation
* You cannot perform any tasks in the `Instance` property before the `instance` variable is instantiated

### Multithreaded Example
```
using System;
public sealed class Singleton
{
    private static volatile Singleton instance;
    private static object syncRoot = new Object();
    private Singleton() {}

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if(instance == null){
                        instance = new Singleton();
                    }
                }
            }
            return instance;
        }
    }
}
```

* Uses *Double-Check Locking* to ensure that only one thread can create a new instance of the Singleton
* instance variable is marked **volatile** to ensure that assignment completes before the variable can be accessed
* **syncRoot** variable is a dummy object used to lock instead of locking onto the type itself, to avoid deadlocks 
* The CLR handles *Double-Check Locking* correctly, unlike C++

### Acknowledgements
* https://msdn.microsoft.com/en-us/library/ff650316.aspx