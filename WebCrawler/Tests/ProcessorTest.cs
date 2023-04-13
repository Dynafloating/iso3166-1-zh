using Processors;

namespace Tests
{
    [TestClass]
    public class ProcessorTest
    {
        [TestMethod]
        public async Task ListTableRows()
        {
            var processor = new TWProcessor();
            var rows = await processor.ListTableRowsAsync(processor.WikiUrl);
            Assert.IsTrue(rows != null && rows.Any());
        }

        [TestMethod]
        public async Task ListTWCountries()
        {
            var processor = new TWProcessor();
            var countries = await processor.ListCountryAsync();
            Assert.IsTrue(countries != null && countries.Any());
        }

        [TestMethod]
        public async Task ListCNCountries()
        {
            var processor = new CNProcessor();
            var countries = await processor.ListCountryAsync();
            Assert.IsTrue(countries != null && countries.Any());
        }
    }
}