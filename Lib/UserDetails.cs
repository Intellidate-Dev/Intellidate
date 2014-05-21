using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntellidateLib
{
    public class UserDetails
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
        /// The Age of the user
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// The Ethnicity collection RefID
        /// </summary>
        public string Ethnicity { get; set; }

        /// <summary>
        /// The Religion RefID
        /// </summary>
        public string Religion { get; set; }

        /// <summary>
        /// The JobType collection RefId
        /// </summary>
        public string Job { get; set; }

        /// <summary>
        /// The User Height in cms
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// The User Height in cms
        /// </summary>
        public string MaritalStatus { get; set; }

        /// <summary>
        /// Convert the given DOB to Age as of Now
        /// </summary>
        /// <param name="DateOfBirth"></param>
        /// <returns></returns>
        public int GetAge(DateTime DateOfBirth)
        {
            TimeSpan UserAge = DateTime.Now - DateOfBirth;
            return (UserAge.Days / 365);
        }

        public string HeightConversion(int heightCms)
        {
            double m_Centimeters = heightCms;
            double m_Inches = m_Centimeters / 2.54;
            double m_Feet = Math.Floor(m_Inches / 12);
            m_Inches -= (m_Feet * 12);
            m_Inches = Math.Round(m_Inches, MidpointRounding.ToEven);
            string m_Height=m_Feet.ToString()+"'"+m_Inches.ToString()+'"';
            return m_Height;
        }

        public UserDetails GetUserDetails(string userID)
        {
            try
            {
                int m_UserId=Convert.ToInt32(userID);

                User usObj = new User().GetUserDetails(m_UserId);
                UserProfile usProfObj = new UserProfile().GetUserProfile(m_UserId);

                UserDetails usDetObj = new UserDetails();

                if(usObj!=null)
                {
                    usDetObj.FullName = usObj.LoginName;
                    usDetObj.Age = GetAge(usObj.DateOfBirth).ToString()+"" +"Years";
                    if(usProfObj!=null)
                    {
                        usDetObj.Height = HeightConversion(usProfObj.Height);
                        usDetObj.MaritalStatus = "NA";
                        usDetObj.Religion = new Religion().GetReligionById(usProfObj.ReligionId).ReligionType.ToString();
                        usDetObj.Ethnicity = new Ethnicity().GetEthnicityById(usProfObj.EthnicityId).EthnicityName;
                        usDetObj.Job = new JobType().GetJobDetailsById(usProfObj.JobId).JobTitle;
                    }
                    
                }
                return usDetObj;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "UserDetails", "GetUserDetails");
                return null;
            }
        }
    }
}
