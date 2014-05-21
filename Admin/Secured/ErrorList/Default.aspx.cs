using IntellidateLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminModule.Secured.ErrorList
{
    public partial class Default : System.Web.UI.Page
    {
        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }
        }
        string _sortDirection = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            BindData();
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DataTable"];
            {
                string SortDir = string.Empty;
                if (dir == SortDirection.Ascending)
                {
                    dir = SortDirection.Descending;
                    SortDir = "Desc";
                }
                else
                {
                    dir = SortDirection.Ascending;
                    SortDir = "Asc";
                }
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + SortDir;
                GridView1.DataSource = sortedView;
                GridView1.DataBind();
            }
        }
        protected void BindData()
        {
            try
            {
                EventLog myEventLog = new EventLog(Constants.EventLogName);
                DataTable dt = new DataTable();
                DataColumn dc1 = new DataColumn("SNo");
                DataColumn dc2 = new DataColumn("MethodName");
                DataColumn dc3 = new DataColumn("ClassName");
                DataColumn dc4 = new DataColumn("TimeStamp");
                DataColumn dc5 = new DataColumn("Ex");
                DataColumn dc6 = new DataColumn("InnerException");
                DataColumn dc7 = new DataColumn("TimeGenerated");
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);
                dt.Columns.Add(dc4);
                dt.Columns.Add(dc5);
                dt.Columns.Add(dc6);
                dt.Columns.Add(dc7);
                var d = EventLog.GetEventLogs();
                foreach (EventLog l in d)
                {
                    if (l.LogDisplayName == Constants.EventLogName)
                    {
                        for (int i = 0; i < myEventLog.Entries.Count; i++)
                        {
                            string JsonString = myEventLog.Entries[i].Message.ToString();
                            Error dtObj = new Error();
                            dtObj = JsonConvert.DeserializeObject<Error>(JsonString);
                            DataRow dr = dt.NewRow();
                            dr[0] = i + 1;
                            dr[1] = dtObj.MethodName;
                            dr[2] = dtObj.ClassName;
                            dr[3] = dtObj.TimeStamp;
                            dr[4] = dtObj.Ex.Message;
                            dr[5] = dtObj.Ex.ToString();
                            dr[6] = myEventLog.Entries[i].TimeGenerated.ToString();
                            dt.Rows.Add(dr);
                        }
                    }

                }
                GridView1.DataSource = dt;
                ViewState["DataTable"] = dt;
                GridView1.DataBind();
            }
            catch (Exception)
            {

            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Clear")
                {
                    EventLog.Delete(Constants.EventLogName);
                    BindData();
                }
            }
            catch (Exception)
            {
            }
        }
    }
}