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

        [ManyToMany(typeof(Manga), CascadeOperations = CascadeOperation.All)]
        public List<Manga> Mangas { get; set; }
    }
}
