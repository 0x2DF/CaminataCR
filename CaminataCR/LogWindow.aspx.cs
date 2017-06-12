using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LogWindow : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (DDLstHora.Items.Count == 0)
        {
            for (int i = 0; i < 24; i++)
                DDLstHora.Items.Add(String.Format(i.ToString()));

            for (int i = 0; i < 60; i++)
                DDLstMin.Items.Add(String.Format(i.ToString()));

            for (int i = 0; i < 60; i++)
                DDLstSeg.Items.Add(String.Format(i.ToString()));

            DDLstTipoCambio.Items.Add("INSERT");
            DDLstTipoCambio.Items.Add("DELETE");
            DDLstTipoCambio.Items.Add("UPDATE");

            DDLstObjeto.Items.Add("Regular");
            DDLstObjeto.Items.Add("Usuario");
            DDLstObjeto.Items.Add("Nivel de calidad");
            DDLstObjeto.Items.Add("Nivel de dificultad");
            DDLstObjeto.Items.Add("Nivel de precio");
            DDLstObjeto.Items.Add("Tipo de caminata");
        }
    }

    protected void Query(object sender, EventArgs e)
    {
        Query query = new Query();

        if (ChxActivarFecha.Checked)
        {
            query.StartDate = DtInicio.Text;
            query.FinalDate = DtFinal.Text;
        }

        if (ChxActivarHora.Checked)
        {
            query.Time = DDLstHora.Text + ":" + DDLstMin.Text + ":" + DDLstSeg.Text + ".000";
        }

        if (ChxActivarTipoCambio.Checked)
        {
            query.Type = DDLstTipoCambio.Text;
        }

        if (ChxActivarObjeto.Checked)
        {
            query.Table = DDLstObjeto.Text;
        }

        WriteQuery(ReadQuery(query));
    }

    List<Query> ReadQuery(Query query)
    {
        
    }

    void WriteQuery(List<Query> list)
    {

    }

    public string Date(string date)
    {
        string[] subString = date.Split('-');
        string dateCorrected = subString[0] + subString[1] + subString[2];

        return dateCorrected;
    }
}