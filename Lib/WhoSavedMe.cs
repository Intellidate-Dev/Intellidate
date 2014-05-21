using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntellidateLib
{
    public class WhoSavedMe
    {
        /// <summary>
        /// The full name of the user. Max=100 characters, Min=10 characters
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// The user gender. Taken from the enum Gender
        /// </summary>
        public int UserGender { get; set; }

        /// <summary>
        /// The date of birth of the user
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// The timestamp when the user was last online.
        /// </summary>
        public DateTime LastOnlineTime { get; set; }

        /// <summary>
        /// get the Age calculated from DOB
        /// </summary>
        /// <param name="DateOfBirth"></param>
        /// <returns></returns>
        public int GetAge(DateTime DateOfBirth)
        {
            TimeSpan UserAge = DateTime.Now - DateOfBirth;
            return (UserAge.Days / 365);
        }

        /// <summary>
        /// The User Profile Saved Time
        /// </summary>
        public DateTime SavedTime { get; set; }

        /// <summary>
        /// The Religion 
        /// </summary>
        public string Religion { get; set; }

        /// <summary>
        /// The JobType collection RefId
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// The Location collection RefId
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        public decimal MatchPercentage { get; set; }

        public int CountOfSaved { get; set; }

        public string ViewedTimes { get; set; }
    }
}
