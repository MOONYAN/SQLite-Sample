﻿using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;

namespace SQLite_Sample
{
    public class Person
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime DayOfBirth { get; set; }

        public byte[] Picture { get; set; }

        [ForeignKey(typeof(Group))]
        public int GroupId { get; set; }

        [ManyToOne(inverseProperty: "Group",CascadeOperations = CascadeOperation.All)]
        public Group Group { get; set; }

        public void Show()
        {
            System.Diagnostics.Debug.WriteLine(Id);
            System.Diagnostics.Debug.WriteLine(Name);
        }
    }
}
