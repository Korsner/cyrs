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
        public void ReadDataReturnsCorrectNumberOfRows()
        {
            var data = query.ReadData();
            Assert.AreEqual(22, data.Rows.Count);

            data = query.ReadTLGRs();
            Assert.AreEqual(2, data.Rows.Count);

            data = query.ReadTNames();
            Assert.AreEqual(8, data.Rows.Count);
        }

        [Test]
        public void ReadFirmTest()
        {
            var data = query.ReadFirm("Деловые линии");
            Assert.AreEqual(1, data.Rows.Count);
            Assert.AreEqual(1, data.Columns.Count);

            Assert.AreEqual("2", data.Rows[0].ItemArray[0].ToString());

            data = query.ReadFirm("Зевс");
            Assert.AreEqual(1, data.Rows.Count);
            Assert.AreEqual(1, data.Columns.Count);

            Assert.AreEqual("1", data.Rows[0].ItemArray[0].ToString());

            data = query.ReadFirm("Юпитер");
            Assert.AreEqual(0, data.Rows.Count);
            Assert.AreEqual(1, data.Columns.Count);
        }

        [Test]
        public void ReadTipTR()
        {
            var data = query.ReadTipTR("Автомобиль-тягач");
            Assert.AreEqual(1, data.Rows.Count);
            Assert.AreEqual(1, data.Columns.Count);

            Assert.AreEqual("4", data.Rows[0].ItemArray[0].ToString());

            data = query.ReadTipTR("Специальный прицеп");
            Assert.AreEqual(1, data.Rows.Count);
            Assert.AreEqual(1, data.Columns.Count);

            Assert.AreEqual("7", data.Rows[0].ItemArray[0].ToString());

            data = query.ReadTipTR("Автомобиль тягач");
            Assert.AreEqual(0, data.Rows.Count);
            Assert.AreEqual(1, data.Columns.Count);
        }

        [Test]
        public void AddTests()
        {
            var c = query.Add("--##", null, "1", "2", "3", "4");
            Assert.AreEqual(-1, c);

            c = query.Add("100", "2", "1", "2", "3", "10/10/2022");
            Assert.AreEqual(1, c);
        }

        [Test]
        public void DeletePTSTest()
        {
            var c = query.DeletePTS(-1);
            Assert.AreEqual(0, c);
        }
    }
}