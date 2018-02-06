using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class navigation : System.Web.UI.Page
{
    string coordenadas;
    protected string MyProperty { get { return coordenadas; } }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["estabelecimento"] == null)
            return;

        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["data_base"].ToString()))
        {
            cn.Open();

            string command = "SELECT coordenadas FROM estabelecimento WHERE id_estabelecimento=" + Request.QueryString["estabelecimento"];
            System.Diagnostics.Debug.WriteLine(command);
            using (SqlCommand cmd = new SqlCommand(command, cn))
            {
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (rdr.HasRows)
                {
                    rdr.Read();
                    coordenadas = rdr.GetString(0);
                }
            }
        }
    }
}