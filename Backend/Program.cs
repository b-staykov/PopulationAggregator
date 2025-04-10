using Backend;
using System;
using Backend.Services;
using Microsoft.Extensions.DependencyInjection;

using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;


class Result
{

    /*
     * Complete the 'fizzBuzz' function below.
     *
     * The function accepts INTEGER n as parameter.
     */

    public static void fizzBuzz(int n)
    {
        bool mod3 = false;
        bool mod5 = false;
        
        for (int i = 1; i <= n; i++)
        {
            mod3 = (i % 3) == 0;
            mod5 = (i % 5) == 0;

            var result = mod3 && mod5 ? "FizzBuzz" :
                mod3 ? "Fizz" :
                    mod5 ? "Buzz" : i.ToString();

            Console.WriteLine(result);
        }
    }

}
class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        Result.fizzBuzz(n);
    }
}


//var serviceProvider = new ServiceCollection()
//    .AddSingleton<IDbManager, SqliteDbManager>()
//    .AddSingleton<IStatServiceAggregator, StatServiceAggregator>()
//    .BuildServiceProvider();

//var aggregator = serviceProvider.GetService<IStatServiceAggregator>();

//if (aggregator == null)
//{
//    Console.WriteLine("Failed to get aggregator");
//    throw new Exception("Failed to get aggregator");
//}

//Console.WriteLine("Application Started.");

//var dbManager = serviceProvider.GetService<IDbManager>();

//if (dbManager == null)
//{
//    Console.WriteLine("Failed to get dbManager");
//    throw new Exception("Failed to get dbManager");
//}

//aggregator.AddSource(new ConcreteStatService(), 1);
//aggregator.AddSource(new SqliteStatService(dbManager), 2);

//Console.WriteLine("Sources added.");

//var items = aggregator.AggregateData();

//Console.WriteLine("Aggregated results: ");
//Console.WriteLine();

//foreach (var item in items)
//{
//    Console.WriteLine($"{item.Item1}: {item.Item2}");
//}

//Console.WriteLine();
//Console.WriteLine("Done.");
//Console.WriteLine("Press any key to end...");

//Console.ReadKey();