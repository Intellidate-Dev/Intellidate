using IntellidateLib.DB;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntellidateLib
{
    public class ForumAttachment
    {
        public ObjectId _id { get; set; }

        /// <summary>
        /// The admin who uploaded the attachment
        /// </summary>
        public ObjectId AdminID { get; set; }

        /// <summary>
        /// The file name of the attachment
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// The path of the file
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// The type of the file. 1=Image, 2=Doc, 3=Xls, 4=PDF
        /// </summary>
        public int FileType { get; set; }

        /// <summary>
        /// The file size in bytes
        /// </summary>
        public long FileSize { get; set; }


        public ForumAttachment AttachFile(string adminId, string filePath, string fileName, long fileSize, int fileType)
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "ForumAttachment", "UploadFile");
                return null;
            }
        }

    }
}
