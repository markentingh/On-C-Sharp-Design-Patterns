# C# Basics

### Value Types & Reference Types
*Reference Type* variables are stored in memory within the heap while *Value Type* variables are stored in memory on the stack. 

##### Value Types
Predefined datatypes, structures, and enums are all value types. 

When creating a Value Type, a single space in memory is allocated to store the value, and the variable directly holds the value. If you assign it to another variable, the value is copied to the second variable and there will be two independant copies of the value within memory. Value types can be created at compile time and stored in stack memory.

##### Reference Types
Classes, Objects, Arrays, Indexers, Interfaces, and pretty much anything that isn't a Value Type is a Reference Type.

Reference Types holds a reference, or address, in memory that points to the object, but is not the object itself. Because of this, assigning multiple variables to the same object simply creates references to the same object in memory.