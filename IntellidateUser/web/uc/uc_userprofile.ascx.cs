using IntellidateLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace IntellidateUser.web.uc
{
    public partial class uc_userprofile : System.Web.UI.UserControl
    {
        private string m_ControlName;

        protected void Page_Load(object sender, EventArgs e)
        {
            string m_ControlName = this.ControlName.ToString();
            GetResponse("", m_ControlName);
        }

        public void GetResponse(string id, string cntrlName)
        {
            string m_ServiceUrl = string.Empty;

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
            if (cntrlName == "UserDetails")
            {
                m_ServiceUrl = "API/UserDetails/?inputParam=1&ControlName=UserDetails";
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
                    UserDetails wss = jss.Deserialize<UserDetails>(m_Response);
                    List<UserDetails> lstUserDetObj = new List<UserDetails>();
                    lstUserDetObj.Add(wss);
                    lstUserDetails.DataSource = lstUserDetObj;
                    lstUserDetails.DataBind();
                }

            }
        }

        public string ControlName
        {
            get { return m_ControlName; }
            set { m_ControlName = value; }
        }

    }
}