using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace WW2Weekend.Classes
{
    class Event
    {
            [BsonId]
            public ObjectId id { get; set; }
            [BsonElement("Name")]
            public string name { get; set; }
            [BsonElement("Description")]
            public string description { get; set; }
            [BsonElement("Location")]
            public string location { get; set; }
            [BsonElement("DateTime")]
            public DateTime datetime { get; set; }
    }
}
