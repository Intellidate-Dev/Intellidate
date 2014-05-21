using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace AdminModule.Secured.Admins
{
    public partial class Default : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadRoles();
                LoadForumsCheckboxes();
            }
        }

        private void LoadRoles()
        {
            try
            {
                DataSet _Roles = new DataSet();
                _Roles.ReadXml(Server.MapPath("~") + "\\App_Data\\Roles.xml");

                _Roles.Tables[0].DefaultView.RowFilter = "Name <> 'SuperAdmin'";

                chkRoles.DataSource = _Roles.Tables[0];
                chkRoles.DataTextField = "Name";
                chkRoles.DataTextFormatString ="Manage {0}";
                chkRoles.DataValueField= "Name";
                chkRoles.DataBind();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void LoadForumsCheckboxes()
        {
            try
            {
                // Get the list from lib
                IntellidateLib.ForumCategory _ForumCategory = new IntellidateLib.ForumCategory();
                var _ForumCategories =  _ForumCategory.GetAllForumCategories();
                chkForums.DataSource = _ForumCategories;
                chkForums.DataTextField = "CategoryName";
                chkForums.DataValueField = "_id";
                chkForums.DataBind();
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}