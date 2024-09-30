using Backend;
using Backend.Services;
using Moq;
using Xunit;

namespace BackendTests
{
    public class StatServiceAggregatorTests
    {
        private IStatService _source1;
        private IStatService _source2;

        public StatServiceAggregatorTests()
        {
            var src1 = new Mock<IStatService>();
            var src2 = new Mock<IStatService>();

            src1.Setup(x => x.GetCountryPopulations())
                .Returns(new List<Tuple<string, int>>() {
                    Tuple.Create("Country1", 1),
                    Tuple.Create("Country2", 2),
                    Tuple.Create("Country3", 3)});

            src2.Setup(x => x.GetCountryPopulations())
                .Returns(new List<Tuple<string, int>>()
                { Tuple.Create("Country2", 1),
                    Tuple.Create(" country3  ", 4) });

            _source1 = src1.Object;
            _source2 = src2.Object;
        }

        [Fact]
        public void TestServiceCombinesAllsourcesCorrectly()
        {
            var aggregator = new StatServiceAggregator();
            aggregator.AddSource(_source1, 1);
            aggregator.AddSource(_source2, 2);

            var items = aggregator.AggregateData();

            Assert.Equal(3, items.Count);
        }

        [Fact]
        public void TestServiceSourcesPriority()
        {
            var aggregator = new StatServiceAggregator();
            aggregator.AddSource(_source1, 1);
            aggregator.AddSource(_source2, 2);

            var items = aggregator.AggregateData();

            var result = items.Find(x => x.Item1.Equals("Country2"));

            Assert.Equal(1, result?.Item2);

            aggregator = new StatServiceAggregator();
            aggregator.AddSource(_source2, 2);
            aggregator.AddSource(_source1, 1);

            items = aggregator.AggregateData();

            result = items.Find(x => x.Item1.Equals("Country2"));

            Assert.Equal(1, result?.Item2);
        }
    }
}
