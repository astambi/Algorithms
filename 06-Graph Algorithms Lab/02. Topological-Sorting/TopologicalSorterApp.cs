using System;
using System.Collections.Generic;

public class TopologicalSorterApp
{
    public static void Main()
    {
        //var graph = new Dictionary<string, List<string>>()
        //{
        //    { "5", new List<string>() { "11" } },
        //    { "7", new List<string>() { "8", "11" } },
        //    { "8", new List<string>() { "9" } },
        //    { "11", new List<string>() { "2", "9", "10" } },
        //    { "9", new List<string>() { } } ,
        //    { "3", new List<string>() { "8", "10" } } ,
        //    { "2", new List<string>() { } } ,
        //    { "10", new List<string>() { } } ,
        //};

        //var graph = new Dictionary<string, List<string>>()
        //{
        //    { "IDEs", new List<string>() { "variables", "loops" } },
        //    { "variables", new List<string>() { "conditionals", "loops", "bits" } },
        //    { "loops", new List<string>() { "bits" } },
        //    { "conditionals", new List<string>() { "loops" } },
        //    { "bits", new List<string>() { } } // corrected
        //};

        var graph = new Dictionary<string, List<string>>() {
            { "A", new List<string>() { "B", "C" } },
            { "B", new List<string>() { "D", "E" } },
            { "C", new List<string>() { "F" } },
            { "D", new List<string>() { "C", "F" } },
            { "E", new List<string>() { "D" } },
            { "F", new List<string>() { } },
        };

        var topSorter = new TopologicalSorter(graph);
        var sortedNodes = topSorter.TopSort();

        Console.WriteLine("Topological sorting: {0}", string.Join(", ", sortedNodes));

        // Topological sorting: A, B, E, D, C, F
    }
}
