using Backend;
using System;
using Backend.Services;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IDbManager, SqliteDbManager>()
    .AddSingleton<IStatServiceAggregator, StatServiceAggregator>()
    .BuildServiceProvider();

var aggregator = serviceProvider.GetService<IStatServiceAggregator>();

if (aggregator == null)
{
    Console.WriteLine("Failed to get aggregator");
    throw new Exception("Failed to get aggregator");
}

Console.WriteLine("Application Started.");

var dbManager = serviceProvider.GetService<IDbManager>();

if (dbManager == null)
{
    Console.WriteLine("Failed to get dbManager");
    throw new Exception("Failed to get dbManager");
}

aggregator.AddSource(new ConcreteStatService(), 1);
aggregator.AddSource(new SqliteStatService(dbManager), 2);

Console.WriteLine("Sources added.");

var items = aggregator.AggregateData();

Console.WriteLine("Aggregated results: ");
Console.WriteLine();

foreach (var item in items)
{
    Console.WriteLine($"{item.Item1}: {item.Item2}");
}

Console.WriteLine();
Console.WriteLine("Done.");
Console.WriteLine("Press any key to end...");

Console.ReadKey();