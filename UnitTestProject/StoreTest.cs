using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;
using Library.Entity;

namespace UnitTestProject
{
    [TestClass]
    public class StoreTest
    {
        Store _store = new Store();
        //Repository _store = new Repository();

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
            Assert.IsTrue(manga.Tags.Count == 0);
        }

        [TestMethod]
        public void TestAddTag()
        {
            Tag tag = new Tag()
            {
                Name = "Well"
            };
            Assert.AreEqual(0, tag.Id);
            Assert.ReferenceEquals(tag, _store.AddTag(tag));
            Assert.IsTrue(tag.Id > 0);
            Assert.IsTrue(tag.Mangas.Count == 0);
        }

        [TestMethod]
        public void TestFindMangaById()
        {
            Manga manga = new Manga()
            {
                Author = "Obama",
                Name = "ObamaAdvanture"
            };
            #region add new Manga
            Assert.AreEqual(0, manga.Id);
            Assert.AreSame(manga, _store.AddManga(manga));
            Assert.IsTrue(manga.Id > 0);
            Assert.IsTrue(manga.Tags.Count == 0);
            #endregion

            #region find by id
            Manga goal = _store.FindMangaById(manga.Id);
            Assert.AreEqual(1, goal.Id);
            Assert.AreNotSame(manga, goal);
            Assert.IsTrue(goal.Tags.Count == 0);
            Assert.IsTrue(manga.Tags.Count == 0);
            #endregion
        }

        [TestMethod]
        public void TestUpdateManga()
        {
            Manga manga = new Manga()
            {
                Author = "Obama",
                Name = "ObamaAdvanture"
            };
            #region add new Manga
            Assert.AreEqual(0, manga.Id);
            Assert.AreSame(manga, _store.AddManga(manga));
            Assert.IsTrue(manga.Id > 0);
            Assert.IsTrue(manga.Tags.Count == 0);
            #endregion

            Tag tag = new Tag()
            {
                Name = "Well"
            };
            #region add tag
            Assert.AreEqual(0, tag.Id);
            Assert.ReferenceEquals(tag, _store.AddTag(tag));
            Assert.IsTrue(tag.Id > 0);
            Assert.IsTrue(tag.Mangas.Count == 0);
            #endregion

            #region update
            manga.Author = "Trump";
            manga.Name = "TrumpAdvanture";
            manga.Tags.Add(tag);
            Assert.AreSame(manga, _store.UpdateManga(manga));
            Assert.IsTrue(_store.FindTagById(1).Mangas.Count == 1);
            Assert.IsTrue(manga.Tags.Count == 1);
            Assert.IsFalse(tag.Mangas.Count == 1);
            #endregion
        }

        [TestMethod]
        public void TestUpdateTag()
        {
            Manga manga = new Manga()
            {
                Author = "Obama",
                Name = "ObamaAdvanture"
            };
            #region add new Manga
            Assert.AreEqual(0, manga.Id);
            Assert.AreSame(manga, _store.AddManga(manga));
            Assert.IsTrue(manga.Id > 0);
            Assert.IsTrue(manga.Tags.Count == 0);
            #endregion

            Tag tag = new Tag()
            {
                Name = "Well"
            };
            #region add tag
            Assert.AreEqual(0, tag.Id);
            Assert.ReferenceEquals(tag, _store.AddTag(tag));
            Assert.IsTrue(tag.Id > 0);
            Assert.IsTrue(tag.Mangas.Count == 0);
            #endregion

            #region update
            tag.Name = "Good";
            tag.Mangas.Add(manga);
            Assert.AreSame(tag, _store.UpdateTag(tag));
            Assert.IsTrue(_store.FindMangaById(1).Tags.Count == 1);
            Assert.IsFalse(manga.Tags.Count == 1);
            Assert.IsTrue(tag.Mangas.Count == 1);
            #endregion
        }

        [TestCleanup]
        public void CleanUp()
        {
            System.Diagnostics.Debug.WriteLine("onClean");
            _store.DropTable();
        }
    }
}
