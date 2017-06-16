using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LogWindow : System.Web.UI.Page
{
    QueryBusiness queryBusiness = new QueryBusiness();

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

        if(!ChxActivarFecha.Checked && !ChxActivarHora.Checked && !ChxActivarTipoCambio.Checked && !ChxActivarObjeto.Checked)
        {
            query.All = true;
        }

        WriteQuery(ReadQuery(query));
    }

    List<Query> ReadQuery(Query query)
    {
        return queryBusiness.ReadQuery(query);
    }

    void WriteQuery(List<Query> list)
    {
        TableRow trTitle = new TableRow();

        TableCell tcTitleDate = new TableCell();
        TableCell tcTitleDescription = new TableCell();
        TableCell tcTitleUser = new TableCell();
        TableCell tcTitleType = new TableCell();
        TableCell tcTitleTable = new TableCell();

        tcTitleDate.Text = "Fecha y Hora";
        tcTitleDescription.Text = "Descripcion";
        tcTitleUser.Text = "Usuario";
        tcTitleType.Text = "Tipo de Cambio";
        tcTitleTable.Text = "Objeto modificado";

        trTitle.Cells.Add(tcTitleDate);
        trTitle.Cells.Add(tcTitleDescription);
        trTitle.Cells.Add(tcTitleUser);
        trTitle.Cells.Add(tcTitleType);
        trTitle.Cells.Add(tcTitleTable);

        LogTable.Rows.Add(trTitle);

        foreach (var e in list)
        {
            TableRow tr = new TableRow();

            TableCell tcDate = new TableCell();
            TableCell tcDescription = new TableCell();
            TableCell tcUser = new TableCell();
            TableCell tcType = new TableCell();
            TableCell tcTable = new TableCell();

            tcDate.Text = e.Date;
            tcDescription.Text = e.Description;
            tcUser.Text = e.User;
            tcType.Text = e.Type;
            tcTable.Text = e.Table;

            tr.Cells.Add(tcDate);
            tr.Cells.Add(tcDescription);
            tr.Cells.Add(tcUser);
            tr.Cells.Add(tcType);
            tr.Cells.Add(tcTable);

            LogTable.Rows.Add(tr);
        }
    }

    public string Date(string date)
    {
        string[] subString = date.Split('-');
        string dateCorrected = subString[0] + subString[1] + subString[2];

        return dateCorrected;
    }
}