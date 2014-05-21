using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AdminModule.Secured.Criteria
{
    /// <summary>
    /// The Criteria Service Handler
    /// </summary>
    public class Service : IHttpHandler
    {
        private const string BlankJson = "[]";

        /// <summary>
        /// The main method for processing the incoming request from ajax
        /// </summary>
        /// <param name="context">HttpContext: The request data along with the User & Form variables</param>
        public void ProcessRequest(HttpContext context)
        {
            string _MethodOutput = "";
            string _MethodCall = "";
            if (context.Request.QueryString["f"] != null)
            {
                _MethodCall = context.Request.QueryString["f"].ToString();
            }
            switch (_MethodCall)
            {
                case "A":
                    {
                        _MethodOutput = AddNewCriteria(context);
                        break;
                    }
                case "G":
                    {
                        _MethodOutput = GetAllCriterias();
                        break;
                    }
            }

            context.Response.ContentType = "text/json";
            context.Response.Write(_MethodOutput);
        }

        /// <summary>
        /// Private method for getting all Criterias
        /// </summary>
        /// <returns>string</returns>
        private string GetAllCriterias()
        {
            var _return = new IntellidateLib.Criteria().GetActiveCriterias();
            return JsonConvert.SerializeObject(_return);
        }

        /// <summary>
        /// This method is to Add the Criteria details.
        /// </summary>
        /// <param name="context">HttpContext: The form data from ajax</param>
        /// <returns>JSON string</returns>
        private string AddNewCriteria(HttpContext context)
        {
            string _CriteriaName = context.Request.Form["CriteriaName"].ToString();
            string _CriteriaType = context.Request.Form["CriteriaType"].ToString();
            string _UserQuestion = context.Request.Form["UserQuestion"].ToString();
            string _PreferenceQuestion = context.Request.Form["PreferenceQuestion"].ToString();
            string _MismatchText = context.Request.Form["MismatchText"].ToString();
            
            bool ShowMatch = Convert.ToBoolean(context.Request.Form["ShowMatch"]);
            string _RefID = context.Request.Form["hdnCriteriaID"].ToString();


            if (_CriteriaType == "1" || _CriteriaType == "2")
            {
                bool IncludeAllPreference = Convert.ToBoolean(context.Request.Form["IncludeAllPreference"]);
                string IncludeAllPreferenceText = context.Request.Form["IncludeAllPreferenceText"].ToString();
                List<string> _Options = new List<string>();
                bool _continue = true;
                int _pos = 0;
                while (_continue)
                {
                    if (context.Request.Form["Items[" + _pos.ToString() + "][OptionText]"] != null)
                    {
                        _Options.Add(context.Request.Form["Items[" + _pos.ToString() + "][OptionText]"].ToString());
                        _pos = _pos + 1;
                    }
                    else
                    {
                        _continue = false;
                    }
                }

                int _out;
                
                var _return = new IntellidateLib.Criteria().AddNewCriteria(_CriteriaName, _UserQuestion, _PreferenceQuestion, Convert.ToInt32(_CriteriaType), _Options.ToArray(), _MismatchText, IncludeAllPreference, IncludeAllPreferenceText, ShowMatch, out _out);
                return JsonConvert.SerializeObject(_return);
            }
            else
            {
                string _MinValue = context.Request.Form["MinValue"].ToString();
                string _MaxValue = context.Request.Form["MaxValue"].ToString();
                string _RangeType = context.Request.Form["RangeType"].ToString();
                int _out;

                double _dMinValue = Convert.ToDouble(_MinValue);
                double _dMaxValue = Convert.ToDouble(_MaxValue);
                
                var _return = new IntellidateLib.Criteria().AddNewCriteria(_CriteriaName, _UserQuestion, _PreferenceQuestion, Convert.ToInt32(_CriteriaType), Convert.ToInt32(_RangeType), _dMinValue , _dMaxValue, _MismatchText , ShowMatch, out _out);
                return JsonConvert.SerializeObject(_return);
            }
        }

        private string[] GetHeights(int StartFeets, int EndFeets)
        {
            List<string> _Heights = new List<string>();
            for (int i = StartFeets; i <= EndFeets; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    _Heights.Add(i.ToString() + "." + j.ToString());
                }
            }

            return _Heights.ToArray();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}