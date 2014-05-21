using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminModule.Secured.Forums
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadForumCategories();
            }
        }

        private void LoadForumCategories()
        {
            try
            {
                int _AdminID = Convert.ToInt32(User.Identity.Name);
                IntellidateLib.ForumCategory[] _ForumCategories = new IntellidateLib.ForumCategory().GetAdminForumCategories(_AdminID);
                cboForumCategories.DataSource = _ForumCategories;
                cboForumCategories.DataTextField = "CategoryName";
                cboForumCategories.DataValueField = "CategoryRefId";
                cboForumCategories.DataBind();

                hdnCurrentCategoryID.Value = _ForumCategories[0].CategoryRefID.ToString();
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}