using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Services
{
    public class StatServiceAggregator : IStatServiceAggregator
    {
        private readonly List<Tuple<IStatService, int>> _sources;

        public StatServiceAggregator()
        {
            _sources = new List<Tuple<IStatService, int>>();
        }

        public void AddSource(IStatService service, int weight)
        {
            _sources.Add(Tuple.Create(service, weight));
        }
        
        public List<Tuple<string, int>> AggregateData()
        {
            var result = new Dictionary<string, Tuple<int, int>>(new CountryComparer());

            foreach (var src in _sources)
            {
                var weight = src.Item2;

                try
                {
                    var items = src.Item1.GetCountryPopulations();

                    if (items == null)
                    {
                        continue;
                    }

                    foreach (var serviceResultItem in items)
                    {
                        if (result.ContainsKey(serviceResultItem.Item1))
                        {
                            if (weight > result[serviceResultItem.Item1].Item2)
                            {
                                result[serviceResultItem.Item1] = new Tuple<int, int>(serviceResultItem.Item2, weight);
                            }
                        }
                        else
                        {
                            result[serviceResultItem.Item1] = new Tuple<int, int>(serviceResultItem.Item2, weight);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error aggregating data from source service ({src.Item1.GetType().Name}): {ex.Message}{Environment.NewLine}{ex.StackTrace}");
                    throw;
                }
            }

            return result.Select(x => new Tuple<string, int>(x.Key, x.Value.Item1)).ToList();
        }

        public Task<List<Tuple<string, int>>> AggregateDataAsync()
        {
            return Task.FromResult<List<Tuple<string, int>>>(AggregateData());
        }

        private class CountryComparer : EqualityComparer<string>
        {
            public override bool Equals(string s1, string s2)
            {
                if (s1 == null && s2 == null)
                {
                    return true;
                }

                if (s1 == null || s2 == null)
                {
                    return false;
                }

                return s1.ToLower().Trim().Equals(s2.ToLower().Trim());
            }

            public override int GetHashCode(string src)
            {
                return src.ToLower().Trim().GetHashCode();
            }
        }
    }
}
