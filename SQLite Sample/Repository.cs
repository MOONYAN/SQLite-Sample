using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using SQLite.Net.Interop;
using SQLiteNetExtensions.Extensions;

namespace SQLite_Sample
{
    public class Repository
    {
        private Random _random = new Random();

        private static SQLiteConnection DbConnection
        {
            get
            {
                System.Diagnostics.Debug.WriteLine(ApplicationData.Current.LocalFolder.Path);
                return new SQLiteConnection(new SQLitePlatformWinRT(), Path.Combine(ApplicationData.Current.LocalFolder.Path, "Storage.sqlite"));
            }
        }

        // Create the table if it does not exist 
        public void CreateTable()
        {
            using (var db = DbConnection)
            {
                db.CreateTable<Person>();
                db.CreateTable<Group>();
                db.GetMapping(typeof(Person));
            }
        }

        // Populate the database
        public void PopulateDB()
        {
            using (var db = DbConnection)
            {
                Person person = new Person()
                {
                    Name = "Trump" + _random.Next(100)
                };
                person.Name = "Trump" + _random.Next(100);
                
                // insert to auto assign id
                db.Insert(person);
                System.Diagnostics.Debug.WriteLine(person.Id + "-----------just insert person id");

                Group group = new Group()
                {
                    Name = "Group GG",
                    Persons = new List<Person>() { person}
                };
                // insert to auto assign id
                db.Insert(group);
                System.Diagnostics.Debug.WriteLine(group.Id + "-----------just insert group id");
                // update foreign key and person
                db.UpdateWithChildren(group);

                person = new Person()
                {
                    Name = "Obama" + _random.Next(100),
                    Group = group
                };
                // insert to auto assign id
                db.Insert(person);
                // update foreign key
                db.UpdateWithChildren(person);
            }
        }

        // Retrieve all Persons
        public void RetrievePersons()
        {
            using (var db = DbConnection)
            {
                //List<Person> people = (from p in db.Table<Person>() select p).ToList();

                //List<Person> people = db.GetAllWithChildren<Person>();
                //people.ForEach(p => p.Show());

                // get group with id = 1
                Group group = db.GetWithChildren<Group>(1);
                group.show();
            }
        }
    }
}
