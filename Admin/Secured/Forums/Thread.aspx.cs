using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace AdminModule.Secured.Forums
{
    public partial class Thread : System.Web.UI.Page
    {
        public string JsonString = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int _postID = 0;
            if (Request.QueryString["LSDGNLSDNGKLSDNGKLLK"] != null)
            {
                _postID = Convert.ToInt32(Request.QueryString["LSDGNLSDNGKLSDNGKLLK"].ToString());
                hdnPostID.Value = _postID.ToString();
                LoadThreadPosts(_postID);
            }
        }

        private void LoadThreadPosts(int PostID)
        {
            try
            {
                List<IntellidateLib.Forum> _Posts = new List<IntellidateLib.Forum>();
                IntellidateLib.Forum _PostDetails = new IntellidateLib.Forum().GetPostDetails(PostID);
                _Posts.Add(_PostDetails);

                IntellidateLib.Forum[] _PostChildren = new IntellidateLib.Forum().GetPostChildren(PostID);
                if (_PostChildren != null)
                {
                    foreach (IntellidateLib.Forum _EachChild in _PostChildren)
                    {
                        _Posts.Add(_EachChild);
                    }
                }
                JsonString = JsonConvert.SerializeObject(_Posts);
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}