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

        [Test]
        public void AddWrongDataDoesntThrowException()
        {
            var c = query.Add("--##", null, "1", "2", "3", "4");
            Assert.AreEqual(-1, c);
        }

        [Test]
        public void DeletePTSTest()
        {
            var c = query.DeletePTS(-1);
            Assert.AreEqual(0, c);
        }

        [Test]
        public void DeletePTSTest2()
        {
            var c = query.DeletePTS(0);
            Assert.AreEqual(1, c);
        }
    }
}