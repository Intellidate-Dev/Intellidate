using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data;

namespace AdminModule.Secured
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                FillLinks();
            }
        }

        protected void lbnSignOut_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/LogoutPage.aspx");
        }

        
        private void FillLinks()
        {
            string[] _AllRoles =  Roles.GetRolesForUser();
            if (_AllRoles.Length == 1 && _AllRoles[0] == "SuperAdmin")
            {
                // Show all the links
                DataSet _Roles = new DataSet();
                _Roles.ReadXml(Server.MapPath("~") + "\\App_Data\\Roles.xml");
                _Roles.Tables[0].DefaultView.RowFilter = "Name <> 'SuperAdmin'";
                rptLeftNav.DataSource = _Roles.Tables[0];
                rptLeftNav.DataBind();
            }
            else
            {
                string _filterString = "";
                foreach (string _eachRole in _AllRoles)
                {
                    _filterString = _filterString + ",'" + _eachRole + "' ";
                }

                _filterString = (_filterString.Length>0)?_filterString.Substring(1):"";

                // show only those links
                DataSet _Roles = new DataSet();
                _Roles.ReadXml(Server.MapPath("~") + "\\App_Data\\Roles.xml");
                if (_filterString.Length > 0)
                {
                    _Roles.Tables[0].DefaultView.RowFilter = "Name in (" + _filterString + ")";
                }
                
                rptLeftNav.DataSource = _Roles.Tables[0];
                rptLeftNav.DataBind();
            }
        }

        protected void rptLeftNav_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                DataRowView _eachLink = (DataRowView)e.Item.DataItem;
                ((System.Web.UI.HtmlControls.HtmlAnchor)e.Item.FindControl("lnkLink")).InnerText = _eachLink[0].ToString();
                ((System.Web.UI.HtmlControls.HtmlAnchor)e.Item.FindControl("lnkLink")).HRef = "/Secured/" + _eachLink[0].ToString().Replace(" ","");



            }
            catch (Exception)
            {

            }
        }
    }
}