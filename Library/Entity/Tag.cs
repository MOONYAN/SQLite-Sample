using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace Library.Entity
{
    public class Tag
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        [ManyToMany(typeof(MangaTag), CascadeOperations = CascadeOperation.All)]
        public List<Manga> Mangas { get { return _mangas; } set { _mangas = value; } }

        private List<Manga> _mangas = new List<Manga>();
    }
}
