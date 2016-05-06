using System;
using System.Collections.Generic;
using System.IO;
using LiteDB;

namespace HairDress.VOL
{
    public class Person
    {
        public int ID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public DateTime BirthDay { set; get; }
        public string HairColor { set; get; }
        public List<Address> Addresses { set; get; }
        public List<Email> Emails { set; get; }
        public List<Phone> Phones { set; get; }
        public List<Operation2Person> OperationsList { set; get; }

        [LiteMapper]
        private string PictureID { set; get; }

        [LiteMapper(Ignore = true)]
        public Stream Picture
        {
            set
            {
                if (DatabaseConnection.Instance.FileStorage.Exists(PictureID ?? ""))
                    DatabaseConnection.Instance.FileStorage.Delete(PictureID);

                PictureID = ObjectId.NewObjectId().ToString();
                DatabaseConnection.Instance.FileStorage.Upload(PictureID, value);
            }
            get { return string.IsNullOrEmpty(PictureID) ? null : DatabaseConnection.Instance.FileStorage.FindById(PictureID).OpenRead(); }
        }
    }
}