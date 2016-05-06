using System.Collections.Generic;
using System.Linq;
using HairDress.VOL;
using LiteDB;

namespace HairDress
{
    public class DatabaseConnection : LiteDatabase
    {
        private const string TablePerson = "Person";
        private const string TablePhone = "Phone";
        private const string TableAddress = "Address";
        private const string TableEmail = "Email";
        private const string TableOperation2Person = "Operation2Person";
        private const string TableOperation = "Operation";
        private static DatabaseConnection _instance;

        public DatabaseConnection() : base("data")
        {
        }

        public static DatabaseConnection Instance => _instance ?? (_instance = new DatabaseConnection());

        public LiteCollection<Person> Person => GetCollection<Person>(TablePerson);
        public LiteCollection<Phone> Phone => GetCollection<Phone>(TablePhone);
        public LiteCollection<Address> Address => GetCollection<Address>(TableAddress);
        public LiteCollection<Email> Email => GetCollection<Email>(TableEmail);
        public LiteCollection<Operation2Person> Operation2Person => GetCollection<Operation2Person>(TableOperation2Person);
        public LiteCollection<Operation> Operation => GetCollection<Operation>(TableOperation);

        protected override void OnModelCreating(BsonMapper mapper)
        {
            mapper.Entity<Operation2Person>()
                .Id(x => x.ID)
                .Ignore(x => x.TotalCost)
                .Index(x => x.ID)
                .DbRef(x => x.Operations, TableOperation);

            mapper.Entity<Person>()
                .Id(x => x.ID)
                .Index(x => x.ID)
                .Index(x => x.Phones)
                .Index(x => x.Emails)
                .DbRef(x => x.Phones, TablePhone)
                .DbRef(x => x.Addresses, TableAddress)
                .DbRef(x => x.Emails, TableEmail)
                .DbRef(x => x.OperationsList, TableOperation2Person);
        }

        public void InsertOrUpdatePerson(params Person[] val)
        {
            if (val == null) return;
            foreach (var v in val)
            {
                InsertOrUpdatePhone(v.Phones?.ToArray());
                InsertOrUpdateAddress(v.Addresses?.ToArray());
                InsertOrUpdateEmail(v.Emails?.ToArray());
                InsertOrUpdateOperation2Person(v.OperationsList?.ToArray());
                if (Person.Exists(x => x.ID == v.ID))
                {
                    Person.Update(v);
                }
                else
                {
                    Person.Insert(v);
                }
            }
        }

        public void InsertOrUpdatePhone(params Phone[] val)
        {
            if (val == null) return;
            foreach (var v in val)
            {
                if (Phone.Exists(x => x.ID == v.ID))
                {
                    Phone.Update(v);
                }
                else
                {
                    Phone.Insert(v);
                }
            }
        }

        public void InsertOrUpdateAddress(params Address[] val)
        {
            if (val == null) return;
            foreach (var v in val)
            {
                if (Address.Exists(x => x.ID == v.ID))
                {
                    Address.Update(v);
                }
                else
                {
                    Address.Insert(v);
                }
            }
        }

        public void InsertOrUpdateEmail(params Email[] val)
        {
            if (val == null) return;
            foreach (var v in val)
            {
                if (Email.Exists(x => x.ID == v.ID))
                {
                    Email.Update(v);
                }
                else
                {
                    Email.Insert(v);
                }
            }
        }

        public void InsertOrUpdateOperation2Person(params Operation2Person[] val)
        {
            if (val == null) return;
            foreach (var v in val)
            {
                if (Operation2Person.Exists(x => x.ID == v.ID))
                {
                    Operation2Person.Update(v);
                }
                else
                {
                    Operation2Person.Insert(v);
                }
            }
        }

        public void InsertOrUpdateOperation(params Operation[] val)
        {
            if (val == null) return;
            foreach (var v in val)
            {
                if (Operation.Exists(x => x.ID == v.ID))
                {
                    Operation.Update(v);
                }
                else
                {
                    Operation.Insert(v);
                }
            }
        }

        public void DeletePerson(params Person[] val)
        {
            if (val == null) return;
            foreach (var v in val)
            {
                DeletePhone(v.Phones?.ToArray());
                DeleteAddress(v.Addresses?.ToArray());
                DeleteEmail(v.Emails?.ToArray());
                DeleteOperation2Person(v.OperationsList?.ToArray());
                Person.Delete(v.ID);
            }
        }

        public void DeletePhone(params Phone[] val)
        {
            if (val == null) return;
            foreach (var v in val)
            {
                Phone.Delete(v.ID);
            }
        }

        public void DeleteAddress(params Address[] val)
        {
            if (val == null) return;
            foreach (var v in val)
            {
                Address.Delete(v.ID);
            }
        }

        public void DeleteEmail(params Email[] val)
        {
            if (val == null) return;
            foreach (var v in val)
            {
                Email.Delete(v.ID);
            }
        }

        public void DeleteOperation2Person(params Operation2Person[] val)
        {
            if (val == null) return;
            foreach (var v in val)
            {
                Operation2Person.Delete(v.ID);
            }
        }

        public Person FindPersonById(int Id)
        {
            return Person.FindById(Id);
        }

        public Person FindPersonByPhone(Phone val)
        {
            return Person.FindAll().FirstOrDefault(x => x.Phones.Exists(y => y.PhoneData.Contains(val.PhoneData)));
        }

        public Person FindPersonByAddress(Address val)
        {
            return Person.FindAll()
                .FirstOrDefault(x => x.Addresses.Exists(y => y.AddressData.Contains(val.AddressData)));
        }

        public Person FindPersonByEmail(Email val)
        {
            return Person.FindAll().FirstOrDefault(x => x.Emails.Exists(y => y.EmailData.Contains(val.EmailData)));
        }

        public IEnumerable<Person> FindPersonByName(string name)
        {
            return Person.Find(x => x.FirstName.StartsWith(name));
        }

        public IEnumerable<Operation> FindOperationByDescription(string val)
        {
            return Operation.Find(x => x.Description.Contains(val));
        }
    }
}