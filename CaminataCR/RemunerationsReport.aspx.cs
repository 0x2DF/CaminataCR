using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Windows.Forms;




public partial class RemunerationsReport : System.Web.UI.Page
{

    IctConsultation consultationUsers = new IctConsultation();
    protected void Page_Load(object sender, EventArgs e)
    {
        makeTable();
    }

    public void makeTable()
    {

        if (inputTop.Text != "" && startDay.Text!="" && startMonth.Text!="" && startYear.Text!="" && endDay.Text!="" && endMonth.Text!="" && endYear.Text!="")
        {
            string startDate= startDay.Text+"-" + startMonth.Text + "-"+ startYear.Text + " 00:00:00.000";
            string endtDate = endDay.Text + "-" + endMonth.Text + "-" + endYear.Text + " 10:30:31.000";

           

            consultationUsers.loadRemunerationsReport(Int32.Parse(inputTop.Text), startDate, endtDate);

            TableRow tRow = new TableRow();

            TableCell tCell = new TableCell();
            tCell.Height = 40;
            tCell.Width = 160;
            tCell.Text = "Nombre Completo";
            tRow.Cells.Add(tCell);

            TableCell tCell2 = new TableCell();
            tCell2.Height = 40;
            tCell2.Width = 160;
            tCell2.Text = "ID";
            tRow.Cells.Add(tCell2);

            TableCell tCell3 = new TableCell();
            tCell3.Height = 40;
            tCell3.Width = 160;
            tCell3.Text = "Monto Total";
            tRow.Cells.Add(tCell3);


            tableTemp.Rows.Add(tRow);

            for (int i = 0; i < consultationUsers.getRemunerationsReportSize(); i++)
            {
                TableRow tRow2 = new TableRow();

                for (int j = 0; j < 3; j++)
                {

                    TableCell tCellt = new TableCell();
                    tCellt.Height = 40;
                    tCellt.Width = 160;
                    tCellt.Text = consultationUsers.getRemunerationsReportMatrixElement(i, j);
                    tRow2.Cells.Add(tCellt);

                }

                tableTemp.Rows.Add(tRow2);

            }


        }

     }

    protected void makeConsult_Click(object sender, EventArgs e)
    {
        tableTemp.Rows.Clear();
        makeTable();

  
    }

    private void inputTop_KeyPress(object sender, KeyPressEventArgs e)
    {
        if (Char.IsDigit(e.KeyChar))
        {
            e.Handled = false;
        }
        else if (Char.IsControl(e.KeyChar))
        {
            e.Handled = false;
        }
        else if (Char.IsSeparator(e.KeyChar))
        {
            e.Handled = false;
        }
        else
        {
            e.Handled = true;
        }
    }




    protected void inputTop_TextChanged(object sender, EventArgs e)
    {

    }

    protected void comeBack2_Click(object sender, EventArgs e)
    {
        Response.Redirect("StrategicInformation.aspx");
    }
}