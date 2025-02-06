using System;
using System.Collections.Generic;
using System.Linq;
using FullOuterJoin;

namespace FullOuterJoin.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
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
}
