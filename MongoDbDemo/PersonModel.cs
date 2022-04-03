using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDbDemo
{
    public class PersonModel
    {
        [BsonId] // Assigns this col as Id col
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressModel PrimaryAddress { get; set; }

        [BsonElement("dob")] // stores col name in db as "dob" instead of "DateOfBirth"
        public DateTime DateOfBirth { get; set; }
    }
}
