using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SQLite_Sample
{
    public class Repository
    {
        private static SQLiteConnection DbConnection
        {
            get
            {
                return new SQLiteConnection(new SQLitePlatformWinRT(), Path.Combine(ApplicationData.Current.LocalFolder.Path, "Storage.sqlite"));
            }
        }

        // Create the table if it does not exist 
        public void CreateTable()
        {
            using (var db = DbConnection)
            {
                var c = db.CreateTable<Person>();
                var info = db.GetMapping(typeof(Person));
            }
        }

        // Populate the database
        public void PopulateDB()
        {
            using (var db = DbConnection)
            {
                Person person = new Person();
                person.Id = 1;
                person.Name = "Sean Connery";
                // (more property assignments here) 
                var i = db.InsertOrReplace(person);
            }
        }

        // Retrieve all Persons
        public void RetrievePersons()
        {
            using (var db = DbConnection)
            {
                List<Person> people = (from p in db.Table<Person>() select p).ToList();
            }
        }
    }
}
