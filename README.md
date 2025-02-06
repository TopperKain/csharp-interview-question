# csharp-interview-question

## Full Outer Join LINQ Function

This project provides a LINQ compatible function that performs a full outer join between two enumerables.

### How to Use the Full Outer Join Function

1. Add the `FullOuterJoin` class library to your project.
2. Use the `FullOuterJoin` function as an extension method on your enumerables.

Example:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using FullOuterJoin;

class Program
{
    static void Main()
    {
        var left = new List<(int Id, string Name)>
        {
            (1, "Left1"),
            (2, "Left2"),
            (3, "Left3")
        };

        var right = new List<(int Id, string Name)>
        {
            (2, "Right2"),
            (3, "Right3"),
            (4, "Right4")
        };

        var result = left.FullOuterJoin(
            right,
            l => l.Id,
            r => r.Id,
            (l, r, id) => new { Id = id, LeftName = l.Name, RightName = r.Name }
        ).ToList();

        foreach (var item in result)
        {
            Console.WriteLine($"Id: {item.Id}, LeftName: {item.LeftName}, RightName: {item.RightName}");
        }
    }
}
```

### How to Run the Console Application

1. Open the solution file `csharp-interview-question.sln` in Visual Studio.
2. Set `FullOuterJoin.ConsoleApp` as the startup project.
3. Run the project.

The console application will demonstrate the full outer join function by joining two sample lists and printing the result to the console.
