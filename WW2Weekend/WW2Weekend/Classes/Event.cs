using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WW2Weekend.Classes
{
    public class Event
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Location")]
        public string Location { get; set; }
        [BsonElement("DateTime")]
        public string Datetime { get; set; }
    }
}
