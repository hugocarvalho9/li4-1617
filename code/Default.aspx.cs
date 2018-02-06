using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["noresults"] != null)
        {
            if (Request.QueryString["noresults"] == "true")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                "alertMessage",
                "alert('Não foram encontrados resultados para a pesquisa.');", true);
            }
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        if (TextBox1.Text.Length == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
            "alertMessage",
            "alert('Tags inválidas ou vazias.');", true);
            return;
        }

        String tags_input = TextBox1.Text;
        String positivas_input = TextBox2.Text;
        String negativas_input = TextBox3.Text;

        Response.Redirect("Resultados.aspx" + "?tags=" + tags_input + "&positive=" + positivas_input + "&negative=" + negativas_input);

        /*
        StringBuilder sb = new StringBuilder();
        sb.Append("Resultados.aspx?tags");

        String tags_input = TextBox1.Text;
        List<String> tags = tags_input.Split(',').ToList<String>();
        foreach (var element in tags)
        {
            Regex.Replace(element, @"\s+", "");
            sb.Append(element);
        }

        String positivas_input = TextBox2.Text;
        if (positivas_input.Length != 0)
        {

        }
        List<String> tags = tags_input.Split(',').ToList<String>();
        List<String> positivas = positivas_input.Split(',').ToList<String>();
        List<String> negativas = negativas_input.Split(',').ToList<String>();

        Response.Redirect("Resultados.aspx" + "?tags=" + tags + "&positive=" + positivas + "&negative=" + negativas);

         */
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}