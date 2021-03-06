﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Library;
using Library.Entity;
using System.Collections.Generic;

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
        public void TestFindMangaByName()
        {
            PopulateData();
            List<Manga> mangas = _store.FindMangaByName("manga1");
            Assert.AreEqual(1, mangas.Count);
            Assert.AreEqual(2, _store.FindMangaById(1).Tags.Count);
            Assert.AreEqual(2, mangas[0].Tags.Count);
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

            #region attach tag
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
        public void TestUpdateManga2()
        {
            PopulateData();
            #region get 1 manga
            Manga manga = _store.FindMangaById(1);
            Assert.AreEqual(2, manga.Tags.Count);
            Assert.AreEqual(2, _store.GetAllTag().Count);
            #endregion

            #region detach tag
            manga.Tags.RemoveAll(tag => tag.Id == 1);
            manga = _store.UpdateManga(manga);
            Assert.AreEqual(1, manga.Tags.Count);
            Assert.AreEqual(1, _store.FindMangaById(manga.Id).Tags.Count);
            Assert.AreEqual(2, _store.GetAllTag().Count);
            Assert.AreEqual(1, _store.FindTagById(1).Mangas.Count);
            #endregion
        }

        [TestMethod]
        public void TestDeleteTag()
        {
            PopulateData();
            Assert.AreEqual(2, _store.GetAllTag().Count);
            Assert.AreEqual(2, _store.FindMangaById(1).Tags.Count);
            Tag tag = _store.FindTagById(1);
            _store.DeleteTag(tag);
            Assert.AreEqual(1,_store.GetAllTag().Count);
            Assert.AreEqual(1, _store.FindMangaById(1).Tags.Count);
        }

        [TestMethod]
        public void TestFindTagById()
        {
            Tag tag = new Tag()
            {
                Name = "Well"
            };
            #region add new tag
            Assert.AreEqual(0, tag.Id);
            Assert.AreSame(tag, _store.AddTag(tag));
            Assert.IsTrue(tag.Id > 0);
            Assert.IsTrue(tag.Mangas.Count == 0);
            #endregion

            #region find by id
            Tag goal = _store.FindTagById(tag.Id);
            Assert.AreEqual(1, goal.Id);
            Assert.AreNotSame(tag, goal);
            Assert.IsTrue(goal.Mangas.Count == 0);
            Assert.IsTrue(tag.Mangas.Count == 0);
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

            #region attach manga
            tag.Name = "Good";
            tag.Mangas.Add(manga);
            Assert.AreSame(tag, _store.UpdateTag(tag));
            Assert.IsTrue(_store.FindMangaById(1).Tags.Count == 1);
            Assert.IsFalse(manga.Tags.Count == 1);
            Assert.IsTrue(tag.Mangas.Count == 1);
            #endregion
        }

        [TestMethod]
        public void TestGetAllManga()
        {
            PopulateData();
            List<Manga> mangas = _store.GetAllManga();
            Assert.AreEqual(2, mangas.Count);
            Assert.AreEqual(2, mangas[0].Tags.Count);
            Assert.AreEqual(2, mangas[1].Tags.Count);
        }

        [TestMethod]
        public void TestGetAllTag()
        {
            PopulateData();
            List<Tag> tags = _store.GetAllTag();
            Assert.AreEqual(2, tags.Count);
            Assert.AreEqual(2, tags[0].Mangas.Count);
            Assert.AreEqual(2, tags[1].Mangas.Count);
        }

        private void PopulateData()
        {
            #region prepare entity
            Manga manga1 = new Manga() { Name = "manga1" };
            Manga manga2 = new Manga() { Name = "manga2" };
            Tag tag1 = new Tag() { Name = "tag1" };
            Tag tag2 = new Tag() { Name = "tag2" };
            #endregion

            #region insert entity
            _store.AddManga(manga1);
            _store.AddManga(manga2);
            _store.AddTag(tag1);
            _store.AddTag(tag2);
            #endregion

            #region attach
            manga1.Tags.Add(tag1);
            manga1.Tags.Add(tag2);
            manga2.Tags.Add(tag1);
            manga2.Tags.Add(tag2);
            _store.UpdateManga(manga1);
            _store.UpdateManga(manga2);
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
