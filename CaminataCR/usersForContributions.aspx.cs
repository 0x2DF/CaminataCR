using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class usersForContributions : System.Web.UI.Page
{
    string name;
    string lastName;
    string order;
    string ascOdesc;
    IctConsultation consultationUsers = new IctConsultation();

    protected void Page_Load(object sender, EventArgs e)
    {
        makeTable();
    }

    public void makeTable() {
        string name;
        string lastName;

        if (inputDataName.Text == "") name = null; else name = inputDataName.Text;
        if (inputDataLastName.Text == "") lastName = null; else lastName = inputDataLastName.Text;

        consultationUsers.loadUserContributions(name, lastName, orderASCDES.SelectedValue, orderBy.SelectedValue);

        TableRow tRow = new TableRow();

        TableCell tCell = new TableCell();
        tCell.Height = 40;
        tCell.Width = 160;
        tCell.Text = "Nombre";
        tRow.Cells.Add(tCell);

        TableCell tCell2 = new TableCell();
        tCell2.Height = 40;
        tCell2.Width = 160;
        tCell2.Text = "Apellido";
        tRow.Cells.Add(tCell2);

        TableCell tCell3 = new TableCell();
        tCell3.Height = 40;
        tCell3.Width = 160;
        tCell3.Text = "Correo";
        tRow.Cells.Add(tCell3);

        TableCell tCell4 = new TableCell();
        tCell4.Height = 40;
        tCell4.Width = 160;
        tCell4.Text = "Celular";
        tRow.Cells.Add(tCell4);

        TableCell tCell7 = new TableCell();
        tCell7.Height = 40;
        tCell7.Width = 220;
        tCell7.Text = "Cantidad de caminatas hechas";
        tRow.Cells.Add(tCell7);

        TableCell tCell8 = new TableCell();
        tCell8.Height = 40;
        tCell8.Width = 220;
        tCell8.Text = "Puntos geográficos reportados";
        tRow.Cells.Add(tCell8);

        TableCell tCell9 = new TableCell();
        tCell9.Height = 40;
        tCell9.Width = 160;
        tCell9.Text = "likes recibidos";
        tRow.Cells.Add(tCell9);

        infoTable.Rows.Add(tRow);

        for (int i = 0; i < consultationUsers.getSizeUsersContributionsMatrix(); i++) {
            TableRow tRow2 = new TableRow();

            for (int j = 0; j < 7; j++) {
                
                TableCell tCellt = new TableCell();
                tCellt.Height = 40;
                tCellt.Width = 160;
                tCellt.Text = consultationUsers.getMatrixUsersContributionsElement(i, j);
                tRow2.Cells.Add(tCellt);

            }
            infoTable.Rows.Add(tRow2);
        }

    }



    protected void search_Click(object sender, EventArgs e)
    {
        
        infoTable.Rows.Clear();
        makeTable();
    }





    protected void comeBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("StrategicInformation.aspx");
    }
}