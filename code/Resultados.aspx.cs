using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Resultados : System.Web.UI.Page
{
    String html;
    Dictionary<string, string> e_names = new Dictionary<string, string>();
    Dictionary<string, string> p_names = new Dictionary<string, string>();
    Dictionary<string, string> e_id = new Dictionary<string, string>();



    public void generateHTML(string k, string e, string p, string c)
    {
        StringBuilder html = new StringBuilder(this.html).Append("<a href=\"navigation.aspx?estabelecimento=").Append(k).Append("\" style=\" text-decoration: none; color: black;\">")
                                                         .Append("<div class=\"result\" style=\"width:90%; margin: 0 auto; box-shadow: 2px 2px 40px #cecece; margin-top: 15px; border-radius:15px;    \">")
                                                         .Append("<div class=\"result_top\" style=\"background-color:#09be00; padding:5px; \">")
                                                         .Append(e)
                                                         .Append("</div>")
                                                         .Append("<div class=\"result_bottom\" style=\"padding:5px;\">")
                                                         .Append("<p><b>" + p + "</b><p>" + "<p>" + c + "</p>")
                                                         .Append("</div>")
                                                         .Append("</div></a>");

        this.html = html.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["tags"] == null)
            return;

        string tags_input = Request.QueryString["tags"];
        string positivas_input = Request.QueryString["positive"];
        string negativas_input = Request.QueryString["negative"];

        List<String> tags = tags_input.Split(',').ToList<String>();
        List<String> positivas = new List<string>();
        List<String> negativas = new List<string>();

        if (positivas_input.Length != 0)
            positivas = positivas_input.Split(',').ToList<String>();

        if (negativas_input.Length != 0)
            negativas = negativas_input.Split(',').ToList<String>();


        StringBuilder command = new StringBuilder();
        int i = 0;
        foreach (var element in tags)
        {
            System.Diagnostics.Debug.WriteLine(element);
            element.Replace(" ", string.Empty);

            if (i++ > 0)
                command.Append("UNION ALL ");

            command.Append("SELECT E.nome, P.nome, P.numero_produto, E.id_estabelecimento FROM etiquetasProduto AS EP")
                   .Append(" INNER JOIN produto AS P ON EP.numero_produto = P.numero_produto")
                   .Append(" INNER JOIN estabelecimento AS E ON P.id_estabelecimento = E.id_estabelecimento")
                   .Append(" WHERE EP.etiqueta = '").Append(element).Append("'");

            if (positivas_input.Length != 0)
            {
                foreach (var pos in positivas)
                {
                    command.Append(" AND EXISTS(SELECT * FROM produtoConstituinte PC")
                           .Append(" WHERE P.numero_produto = PC.numero_produto")
                           .Append(" AND PC.constituinte ='").Append(pos).Append("')");
                }
            }

            if (negativas_input.Length != 0)
            {
                foreach (var neg in negativas)
                {
                    command.Append(" AND NOT EXISTS(SELECT * FROM produtoConstituinte PC")
                           .Append(" WHERE P.numero_produto = PC.numero_produto")
                           .Append(" AND PC.constituinte = '").Append(neg).Append("')");
                }
            }
        }

        System.Diagnostics.Debug.WriteLine(command.ToString());


        using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["data_base"].ToString()))
        {
            cn.Open();
            using (SqlCommand cmd = new SqlCommand(command.ToString(), cn))
            {
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (rdr.HasRows) {
                        while (rdr.Read())
                    {
                        e_names.Add(rdr[2].ToString(), rdr[0].ToString());
                        p_names.Add(rdr[2].ToString(), rdr[1].ToString());
                        e_id.Add(rdr[2].ToString(), rdr[3].ToString());
                    }
                } else
                {
                    Response.Redirect("Default.aspx?noresults=true");
                }

                cn.Close();
            }
        }


        foreach (KeyValuePair<string, string> entry in e_names)
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["data_base"].ToString()))
            {
                cn.Open();

                StringBuilder command_c = new StringBuilder("select constituinte from produtoConstituinte where numero_produto =").Append(entry.Key);
                StringBuilder constituintes = new StringBuilder();

                using (SqlCommand cmd_c = new SqlCommand(command_c.ToString(), cn))
                {
                    SqlDataReader rdr_c = cmd_c.ExecuteReader();

                    while (rdr_c.Read())
                        constituintes.Append(rdr_c[0].ToString()).Append(", ");

                    generateHTML(e_id[entry.Key], entry.Value, p_names[entry.Key], constituintes.ToString());
                    interact.InnerHtml = html;
                }
            }
        }


    }

}