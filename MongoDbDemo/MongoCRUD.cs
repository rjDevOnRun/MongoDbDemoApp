using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace MongoDbDemo
{
    public class MongoCRUD
    {
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            // Create mongo client
            var client = new MongoClient();

            // get mongo database connection
            db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string table, T record)
        {
            // collection are like tables in rdbms
            var collection = db.GetCollection<T>(table);

            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string table)
        {
            var collection = db.GetCollection<T>(table);
            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string table, Guid id)
        {
            var collect = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collect.Find(filter).First();
        }

        public void UpsertRecord<T>(string table, Guid id, T record)
        {
            var collection = db.GetCollection<T>(table);

            var result = collection.ReplaceOne(
                new BsonDocument("_id", id), // Checks internal '_id' col if record exists
                record,
                new UpdateOptions { IsUpsert = true } // update/insert
                );
        }

        public void DeleteRecord<T>(string table, Guid id)
        {
            var collection = db.GetCollection<T>(table);
            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.DeleteOne(filter);
        }
    }
}
