using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace TCClient.Models
{
    public class TitleData
    {
        int titleId = -1;
        string title = "";

        public int TitleId
        {
            get { return titleId; }
            set { titleId = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }
    }
    public class TitlesModel
    {
        public BsonDocument GetData(int id)
        {
            IMongoClient client = new MongoClient("mongodb://readonly:turner@ds043348.mongolab.com:43348/dev-challenge");
            IMongoDatabase database = client.GetDatabase("dev-challenge");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Titles");

            var queryFilter = Builders<BsonDocument>.Filter.Eq("TitleId", id);

            BsonDocument doc = collection.Find(queryFilter).First();
            return doc;
        }

        public List<TitleData> GetTitles(string filter)
        {
            List<TitleData> titles = new List<TitleData>();

            IMongoClient client = new MongoClient("mongodb://readonly:turner@ds043348.mongolab.com:43348/dev-challenge");
            IMongoDatabase database = client.GetDatabase("dev-challenge");
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("Titles");

            var queryFilter = string.IsNullOrEmpty(filter) ? Builders<BsonDocument>.Filter.Empty : Builders<BsonDocument>.Filter.Regex("TitleName", BsonRegularExpression.Create(new Regex(filter, RegexOptions.IgnoreCase)));

            foreach (BsonDocument doc in collection.Find(queryFilter).ToList())
            {
                List<BsonElement> data = doc.ToList();
                TitleData val = new TitleData();
                val.Title = doc.GetElement("TitleName").Value.ToString();
                val.TitleId = doc.GetElement("TitleId").Value.ToInt32();
                titles.Add(val);
            }
            return titles;
        }
    }
}