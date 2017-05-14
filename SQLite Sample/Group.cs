using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;

namespace SQLite_Sample
{
    public class Group
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Person> Persons { get; set; }

        public void show()
        {
            System.Diagnostics.Debug.WriteLine(Id);
            System.Diagnostics.Debug.WriteLine(Name);
            Persons.ForEach(p=>p.Show());
        }
    }
}
