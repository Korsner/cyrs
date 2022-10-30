using DbLibrary.Controller;
using NUnit.Framework;

namespace LbLibraryUnitTests
{
    [TestFixture]
    public class QueryTests
    {
        private Query query;
        [SetUp]
        public void Init()
        {
            var connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Test.mdb";
            query = new Query(connString);
        }



        [Test]
        public void ReadDataDoesntThrowException()
        {
            var data = query.ReadData();
            Assert.IsNotNull(data);
        }
    }
}