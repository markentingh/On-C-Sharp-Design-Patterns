# On C#
This repository was created to help solidify my understanding of C# and all of its glory. Here, you will find extensive knowledge that I have compiled about many important subjects that will ultimately make you an enterprise-level engineer.

###  1. Design Patterns
My research began with design patterns in C#, since this is the most basic and useful information I had to study. There are 23 design patterns identified by four computer scientists who wrote a book called **Design Patterns: Elements of Reusable Object-Oriented Software**. I have described each pattern, provided working examples within a Visual Studio solution, and have included any helpful information that I have found about each pattern that can help clarify its use case, limitations, and requirements.

### 2. SOLID Architecture
These principles were created to provide developers with a way to manage most software design problems. The SOLID principles provides a way to create loosely coupled and encapsulated code for use in enterprise-level projects.

### 3. Architectural Patterns
There are many *architectural styles* used to design modern software, and in order to make the correct decisions when planning out the architecture of an application, one must fully understand each of the many available styles that can be implemented. In most cases, a combination of styles will be used to develop an application.

### 4. Generics
Generics are a very powerful feature in .NET and allows me to develop methods that can take any object type as arguments, without incurring the cost of runtime casts or boxing operations. In fact, I use generics every day with `List<T>` and `Dictionary<T key, T value>`

### 5. Lambda Expressions & Delegates
I've used LINQ for a long time without fully understanding how powerful lambda expressions were. Now, I am able to develop my own libraries that utilize functional programming techniques (and so can you!).

### 6. Multi-threading
This is an important topic since it allows me to write optimized code that can take full advantage of my CPU. I have discussed `System.Threading` along with `async` and `await`, and have given examples on how to properly use them in many different use cases.

### 7. Sorting & Filtering
There are many types of sorting & filtering algorithms that can save many resources and processing time (usually measured in milliseconds). The algorithms I talk about have been implemented in many other languages other than C#, but I have either found a working example in C#, or translated it myself.

### 8. GPU Utilization
Many mathmatical calculations that involve large amounts of data can be offloaded onto the GPU to relieve stress on the CPU as well as speed up the processing time for these calculations. I've provided some examples on how this can be done, along with a list of use cases for offloading math to the GPU.

### 9. Unit Testing
I've never taken the time to create unit tests before, but after many years of wondering how well my code will perform (if at all), I've finally figured out how to test my code with millions of hypothetical users / requests to see where limitations and security flaws exist. Finally, I can watch my code perform better after making optimizations and iterating through unit test results.

### 10. Performance Optimizations
This section is dedicated to my observations in making optimizations in my code. For example, it is better to append strings using a `StringBuilder` instead of just concatenating strings using `a += b`;

### 11. Entity Framework
I don't really like EF since I use SQL Projects in my Visual Studio Solution, but many people enjoy working with Object Relational Mappers (ORM). I do use *Dapper* to interface with SQL Server, though, which is a straight-forward ORM developed by StackOverflow.

### 12. Glossary
A collection of words & phrases that I have found to be important when speaking about C# development
