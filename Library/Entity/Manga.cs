using System;
using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace Library.Entity
{
    public class Manga
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(512)]
        public string Name { get; set; }

        public string Path { get; set; }

        public string Author { get; set; }

        public DateTime LastEditTime { get; set; }

        [ManyToMany(typeof(MangaTag), CascadeOperations = CascadeOperation.All)]
        public List<Tag> Tags { get { return _tags; } set { _tags = value; } }
        
        private List<Tag> _tags = new List<Tag>();
    }
}
