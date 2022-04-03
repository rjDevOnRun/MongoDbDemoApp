using System;

namespace MongoDbDemo
{
    public class Program
    {
        static void Main(string[] args)
        {
            MongoCRUD db = new MongoCRUD("AddressBook");

            #region Insert New

            //var person = new PersonModel 
            //{ 
            //    FirstName = "Carl", 
            //    LastName = "Segan",
            //    PrimaryAddress = new AddressModel
            //    {
            //        City = "Boston",
            //        State = "MA",
            //        ZipCode ="12345",
            //        StreetAddress = "123 Street"
            //    }
            //};

            //db.InsertRecord("Users", person); 

            #endregion

            #region Get Records

            var recs = db.LoadRecords<PersonModel>("Users");

            foreach (var rec in recs)
            {
                Console.WriteLine($"{rec.Id}: {rec.FirstName} {rec.LastName}");
                if (rec.PrimaryAddress != null)
                {
                    Console.WriteLine(rec.PrimaryAddress.City);
                }
                Console.WriteLine();
            }

            #endregion

            #region Get Record by Id

            var oneRec = db.LoadRecordById<PersonModel>
                ("Users",new Guid("b45ad05e-e283-4a27-a4a5-0453cef2f964"));

            Console.WriteLine($"{oneRec.Id}: {oneRec.FirstName} {oneRec.LastName}");

            if (oneRec.PrimaryAddress != null)
            {
                Console.WriteLine(oneRec.PrimaryAddress.City);
            }

            oneRec.DateOfBirth = new DateTime(1976, 03, 02, 0, 0, 0, DateTimeKind.Utc);

            db.UpsertRecord("Users", oneRec.Id, oneRec);

            oneRec = db.LoadRecordById<PersonModel>
                ("Users", new Guid("b45ad05e-e283-4a27-a4a5-0453cef2f964"));

            Console.WriteLine($"{oneRec.Id}: {oneRec.FirstName} {oneRec.LastName} {oneRec.DateOfBirth}");
            #endregion

            #region Delete Record

            db.DeleteRecord<PersonModel>(
                "Users", new Guid("55cf7000-627c-46fd-a1ac-a1d18bde4558"));

            #endregion

            Console.ReadLine();
        }
    }
}
