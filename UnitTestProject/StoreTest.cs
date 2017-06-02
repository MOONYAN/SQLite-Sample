using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;
using Library.Entity;

namespace UnitTestProject
{
    [TestClass]
    class StoreTest
    {
        Store _store = new Store();

        [TestInitialize]
        public void Initialize()
        {
            System.Diagnostics.Debug.WriteLine("onInit");
            _store.CreateTable();
        }

        [TestMethod]
        public void TestAddManga()
        {
            Manga manga = new Manga()
            {
                Author = "Obama",
                Name = "ObamaAdvanture"
            };
            Assert.AreEqual(0, manga.Id);
            Assert.ReferenceEquals(manga, _store.AddManga(manga));
            Assert.IsTrue(manga.Id > 0);
        }

        [TestMethod]
        public void TestAddTag()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TestUpdateManga()
        {

        }

        [TestMethod]
        public void TestUpdateTag()
        {

        }

        [TestCleanup]
        public void CleanUp()
        {
            System.Diagnostics.Debug.WriteLine("onClean");
            _store.CreateTable();
        }
    }
}
