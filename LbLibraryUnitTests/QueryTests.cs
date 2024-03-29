﻿using DbLibrary.Controller;
using NUnit.Framework;
using System.Data;

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
            var data = query.ReadTLGRs();
            Assert.IsNotNull(data);
            Assert.AreEqual(2, data.Rows.Count);

            data = query.ReadTNames();
            Assert.IsNotNull(data);
            Assert.AreEqual(8, data.Rows.Count);
        }

        [Test]
        public void ReadFirmTest()
        {
            var data = query.ReadFirm("Деловые линии");
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Rows.Count);
            Assert.AreEqual(1, data.Columns.Count);

            Assert.AreEqual("2", data.Rows[0].ItemArray[0].ToString());

            data = query.ReadFirm("Зевс");
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Rows.Count);
            Assert.AreEqual(1, data.Columns.Count);

            Assert.AreEqual("1", data.Rows[0].ItemArray[0].ToString());

            data = query.ReadFirm("Юпитер");
            Assert.IsNotNull(data);
            Assert.AreEqual(0, data.Rows.Count);
            Assert.AreEqual(1, data.Columns.Count);
        }

        [Test]
        public void ReadTipTR()
        {
            var data = query.ReadTipTR("Автомобиль-тягач");
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Rows.Count);
            Assert.AreEqual(1, data.Columns.Count);

            Assert.AreEqual("4", data.Rows[0].ItemArray[0].ToString());

            data = query.ReadTipTR("Специальный прицеп");
            Assert.IsNotNull(data);
            Assert.AreEqual(1, data.Rows.Count);
            Assert.AreEqual(1, data.Columns.Count);

            Assert.AreEqual("7", data.Rows[0].ItemArray[0].ToString());

            data = query.ReadTipTR("Автомобиль тягач");
            Assert.IsNotNull(data);
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

            c = query.DeletePTS(15);
            Assert.AreEqual(1, c);
        }

        [Test]
        public void ReadDetailedDataTest()
        {
            var data = query.ReadDetailedData();
            Assert.IsNotNull(data);

            Assert.AreEqual(7, data.Columns.Count);
        }

        [Test]
        public void UpdatePTSTest()
        {
            var detailedData = query.ReadDetailedData();
            Assert.IsNotNull(detailedData);

            var row =
                detailedData.Rows.OfType<DataRow>()
                   .FirstOrDefault(row => row.Field<int>("Учетный номер ТС") == 4);
            Assert.IsNotNull(row);
            Assert.AreEqual(7, detailedData.Columns.Count);
            Assert.AreEqual("Зевс", row[1]);
            Assert.AreEqual("Самоходный тягач", row[3]);
            Assert.AreEqual("Седельный тягач", row[4]);
            Assert.AreEqual(30, row[5]);
            Assert.AreEqual(25, row[6]);

            var c = query.UpdatePTS(null, null, null, "40", "50", "4");
            Assert.AreEqual(-1, c);

            c = query.UpdatePTS("4", null, null, "40", "50", "4");
            Assert.AreEqual(1, c);

            detailedData = query.ReadDetailedData();
            Assert.IsNotNull(detailedData);

            row =
                detailedData.Rows.OfType<DataRow>()
                   .FirstOrDefault(row => row.Field<int>("Учетный номер ТС") == 4);
            Assert.IsNotNull(row);
            Assert.AreEqual(7, detailedData.Columns.Count);
            Assert.AreEqual("Зевс", row[1]);
            Assert.AreEqual("Самоходный тягач", row[3]);
            Assert.AreEqual("Седельный тягач", row[4]);
            Assert.AreEqual(40, row[5]);
            Assert.AreEqual(50, row[6]);
        }
    }
}