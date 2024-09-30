using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend;

public interface IStatServiceAggregator
{
    void AddSource(IStatService service, int weight);

    List<Tuple<string, int>> AggregateData();
    Task<List<Tuple<string, int>>> AggregateDataAsync();
}
