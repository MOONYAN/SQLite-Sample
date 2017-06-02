using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;


namespace Library.Entity
{
    class MangaTag
    {
        [ForeignKey(typeof(Manga))]
        public int MangaId { get; set; }

        [ForeignKey(typeof(Tag))]
        public int TagId { get; set; }
    }
}
