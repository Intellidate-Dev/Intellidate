using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntellidateLib
{
    public class ProfilesViewedByMe
    {
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int UserID { get; set; }
        public string FullName { get; set; }
        public string ImagePath { get; set; }
    }
}
