using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IntellidateLib;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace IntellidateUser.uc
{
    public partial class uc_whosavedme : System.Web.UI.UserControl
    {
        private string name;

        protected void Page_Load(object sender, EventArgs e)
        {
            string m_ControlName = this.Name.ToString();
            GetResponse("",m_ControlName);
        }

        /// <summary>
        /// This Method is written for calling the API from the code behind 
        /// and frame the response of API to the desired format for user representation
        /// </summary>
        /// <param name="id"></param>
        public void GetResponse(string id,string cntrlName)
        {
            string m_ServiceUrl=string.Empty;
            
            HttpClient clientObj = new HttpClient();

            string m_Host = HttpContext.Current.Request.Url.Host;
            if (HttpContext.Current.Request.Url.Port.ToString() != "")
            {
                m_Host = "http://" + m_Host + ":" + HttpContext.Current.Request.Url.Port + "/";
            }
            else
            {
                m_Host = "http://" + m_Host + "/";
            }
            string m_BaseUrl = m_Host;
            clientObj.BaseAddress = new Uri(m_BaseUrl);

            // Add an Accept header for JSON format.
            clientObj.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
            if (cntrlName == "WhoSaved")
            {
                m_ServiceUrl = "API/WhoSavedMe/?inputParam=2&ControlName=WhoSaved";
            }
            else if(cntrlName=="WhoViewed")
            {
                m_ServiceUrl = "API/WhoSavedMe/?inputParam=2&ControlName=WhoViewed";
            }
            else if (cntrlName == "MySaved")
            {
                m_ServiceUrl = "API/WhoSavedMe/?inputParam=2&ControlName=MySaved";
            }
            HttpResponseMessage responseObj = clientObj.GetAsync(m_ServiceUrl).Result;        
            
            if (responseObj.IsSuccessStatusCode)
            {
                // Parse the response body. Blocking!
                Task<string> m_message = responseObj.Content.ReadAsAsync<string>();
                string m_Response = m_message.Result;
                if (m_Response != null)
                {
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    List<WhoSavedMe> wss = jss.Deserialize<List<WhoSavedMe>>(m_Response);
                    lstSavedProfile.DataSource = wss;
                    lstSavedProfile.DataBind();
                }

            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
      
    }
}