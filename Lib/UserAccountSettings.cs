using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntellidateLib
{
    public class UserAccountSettings
    {
        /// <summary>
        /// The user identifier
        /// </summary>
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        /// <summary>
        /// The reference ID from the collection or MySQL Database if any..
        /// </summary>
        public int _RefID { get; set; }

        /// <summary>
        ///login user id
        /// </summary>
        public int UserRefId { get; set; }

        /// <summary>
        ///login IsAccount Deleted deleted=true, active=false
        /// </summary>
        public bool IsAccountDeleted { get; set; }

        /// <summary>
        ///comment for why he/she deleting the account
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Time stamp to maintain the history 
        /// </summary>
        public DateTime TimeStamp { get; set; }
    }

}
