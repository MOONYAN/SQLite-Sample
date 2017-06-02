using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Library.Entity;
using SQLiteNetExtensions.Extensions;

namespace Library
{
    public class Store
    {
        private static SQLiteConnection DbConnection
        {
            get
            {
                System.Diagnostics.Debug.WriteLine(ApplicationData.Current.LocalFolder.Path);
                return new SQLiteConnection(new SQLitePlatformWinRT(), Path.Combine(ApplicationData.Current.LocalFolder.Path, "Storage.sqlite"));
            }
        }

        public void CreateTable()
        {
            using (var db = DbConnection)
            {
                db.CreateTable<Tag>();
                db.CreateTable<Manga>();
                db.CreateTable<MangaTag>();
            }
        }

        public Manga AddManga(Manga manga)
        {
            using (var db = DbConnection)
            {
                db.Insert(manga);
            }
            return manga;
        }

        public Manga UpdateManga(Manga manga)
        {
            using (var db = DbConnection)
            {
                db.Update(manga);
                db.UpdateWithChildren(manga);
            }
            return manga;
        }

        public Tag UpdateTag(Tag tag)
        {
            using (var db = DbConnection)
            {
                db.Update(tag);
                db.UpdateWithChildren(tag);
            }
            return tag;
        }

        public Tag AddTag(Tag tag)
        {
            using (var db = DbConnection)
            {
                db.Insert(tag);
            }
            return tag;
        }
    }
}
