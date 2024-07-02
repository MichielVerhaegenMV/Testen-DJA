using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Order_Viewer_Web_Application
{
    public partial class _Default : Page
    {
        //Hier aanpassen en in Web.config
        private static string connectionString = "db_DJATESTENConnectionString";
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlDataSource source = new SqlDataSource();
            source.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            source.SelectCommand = "SELECT * FROM [Orders] ORDER BY [TimeStamp], [FileName]";
            this.Controls.Add(source);
            GridView1.DataSource = source;
            GridView1.DataBind();
        }

    }
}