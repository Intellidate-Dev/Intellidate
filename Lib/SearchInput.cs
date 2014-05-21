using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntellidateLib
{
    //to set the search keys
    public class SearchInput
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string SearchKey { get; set; }
        public string[] ProfileIds{ get; set; }
        public string[] QuestionIds { get; set; }

        //insert and update search keys
        public bool AddOrUpdateSearchKey(string SearchKey,string ProfileIds,string QuestionIds)
        {
            try
            {
                if (SearchKey.Trim() != string.Empty)
                {
                    MongoDatabase CachingDatabase = CachingDbConnector.GetCachingDatabase();
                    var m_Query = Query.EQ("SearchKey", SearchKey);
                    var m_Collection = CachingDatabase.GetCollection<SearchInput>("SearchInput");
                    var m_data = m_Collection.FindOne(m_Query);
                    if (m_data == null)
                    {
                        SearchInput _CachingObject = new SearchInput
                        {

                            SearchKey = SearchKey,
                            ProfileIds = ProfileIds.Split(','),
                            QuestionIds = QuestionIds.Split(','),
                        };
                        //insert the record into cache db
                        var _newCollection = CachingDatabase.GetCollection<SearchInput>("SearchInput");
                        _newCollection.Save(_CachingObject);
                        return true;
                    }
                    else
                    {
                        bool m_Res = false;
                        if (!m_data.ProfileIds.Contains(ProfileIds) && !m_data.QuestionIds.Contains(QuestionIds))
                        {
                            var _UpdateQuery = Query.EQ("SearchKey", m_data.SearchKey);
                            var _Update = Update.Push("ProfileIds", ProfileIds)
                                                .Push("QuestionIds", QuestionIds);
                            var _SortBy = SortBy.Descending("SearchKey");
                            var _Result = m_Collection.FindAndModify(_UpdateQuery, _SortBy, _Update, true);
                            if (_Result != null)
                            {
                                m_Res = true;
                            }
                        }
                        if (!m_data.ProfileIds.Contains(ProfileIds) && m_data.QuestionIds.Contains(QuestionIds))
                        {
                            var _UpdateQuery = Query.EQ("SearchKey", m_data.SearchKey);
                            var _Update = Update.Push("ProfileIds", ProfileIds);
                            var _SortBy = SortBy.Descending("SearchKey");
                            var _Result = m_Collection.FindAndModify(_UpdateQuery, _SortBy, _Update, true);
                            if (_Result != null)
                            {
                                m_Res = true;
                            }
                        }
                        if (m_data.ProfileIds.Contains(ProfileIds) && !m_data.QuestionIds.Contains(QuestionIds))
                        {
                            var _UpdateQuery = Query.EQ("SearchKey", m_data.SearchKey);
                            var _Update = Update.Push("QuestionIds", QuestionIds);
                            var _SortBy = SortBy.Descending("SearchKey");
                            var _Result = m_Collection.FindAndModify(_UpdateQuery, _SortBy, _Update, true);
                            if (_Result != null)
                            {
                                m_Res = true;
                            }
                        }
                        return m_Res;
                    }
                }
                else
                {
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                new Error().LogError(ex, "SearchInput", "AddOrUpdateSearchKey");
                return false;
            }
        }



    }
}
