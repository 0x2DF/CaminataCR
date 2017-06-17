using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClassificationOfRoutes : System.Web.UI.Page
{

    IctConsultation consultationUsers = new IctConsultation();
    protected void Page_Load(object sender, EventArgs e)
    {
        makeTable();
     
    }


    public void makeTableTemp(string name,int table) {
        TableRow tRow = new TableRow();

        TableCell tCell = new TableCell();
        tCell.Height = 40;
        tCell.Width = 100;
        tCell.Text = "cantidad Puntos";
        tRow.Cells.Add(tCell);

        TableCell tCell2 = new TableCell();
        tCell2.Height = 40;
        tCell2.Width = 100;
        tCell2.Text = "Likes";
        tRow.Cells.Add(tCell2);

        TableCell tCell3 = new TableCell();
        tCell3.Height = 40;
        tCell3.Width = 100;
        tCell3.Text = name;
        tRow.Cells.Add(tCell3);

        

        if (table==1) {
            TableIdtipoDeCaminata.Rows.Add(tRow);
            consultationUsers.loadClassificationOfRoutesTemp(groupList.Items[0].Value, "", "");

            for (int i = 0; i < consultationUsers.getClassificationOfRoutesSizeTemp(); i++)
            {
                TableRow tRow2 = new TableRow();

                for (int j = 0; j < 3; j++)
                {

                    TableCell tCellt = new TableCell();
                    tCellt.Height = 40;
                    tCellt.Width = 100;
                    tCellt.Text = consultationUsers.getClassificationOfRoutesMatrixTemp(i, j);
                    tRow2.Cells.Add(tCellt);

                }
                TableIdtipoDeCaminata.Rows.Add(tRow2);
            }

        }
        if (table == 2) {
            TableIdNivelDePrecio.Rows.Add(tRow);

            consultationUsers.loadClassificationOfRoutesTemp(groupList.Items[1].Value, "", "");
            for (int i = 0; i < consultationUsers.getClassificationOfRoutesSizeTemp(); i++)
            {
                TableRow tRow2 = new TableRow();

                for (int j = 0; j < 3; j++)
                {

                    TableCell tCellt = new TableCell();
                    tCellt.Height = 40;
                    tCellt.Width = 100;
                    tCellt.Text = consultationUsers.getClassificationOfRoutesMatrixTemp(i, j);
                    tRow2.Cells.Add(tCellt);

                }
                TableIdNivelDePrecio.Rows.Add(tRow2);
            }

        }
        if (table == 3) {
            TableIdNivelDeCalidad.Rows.Add(tRow);

            consultationUsers.loadClassificationOfRoutesTemp(groupList.Items[2].Value, "", "");
            for (int i = 0; i < consultationUsers.getClassificationOfRoutesSizeTemp(); i++)
            {
                TableRow tRow2 = new TableRow();

                for (int j = 0; j < 3; j++)
                {

                    TableCell tCellt = new TableCell();
                    tCellt.Height = 40;
                    tCellt.Width = 100;
                    tCellt.Text = consultationUsers.getClassificationOfRoutesMatrixTemp(i, j);
                    tRow2.Cells.Add(tCellt);

                }
                TableIdNivelDeCalidad.Rows.Add(tRow2);
            }

        }
        if (table == 4) {
            TableIdNivelDeDificultad.Rows.Add(tRow);
            consultationUsers.loadClassificationOfRoutesTemp(groupList.Items[3].Value, "", "");
            for (int i = 0; i < consultationUsers.getClassificationOfRoutesSizeTemp(); i++)
            {
                TableRow tRow2 = new TableRow();

                for (int j = 0; j < 3; j++)
                {

                    TableCell tCellt = new TableCell();
                    tCellt.Height = 40;
                    tCellt.Width = 100;
                    tCellt.Text = consultationUsers.getClassificationOfRoutesMatrixTemp(i, j);
                    tRow2.Cells.Add(tCellt);

                }
                TableIdNivelDeDificultad.Rows.Add(tRow2);
            }

        }

    }

    public void makeTable() {
        consultationUsers.LoadClassificationOfRoutes();
        TableRow tRow = new TableRow();

        TableCell tCell = new TableCell();
        tCell.Height = 40;
        tCell.Width = 160;
        tCell.Text = "Caminata";
        tRow.Cells.Add(tCell);

        TableCell tCell2 = new TableCell();
        tCell2.Height = 40;
        tCell2.Width = 160;
        tCell2.Text = "Cantidad de Rutas";
        tRow.Cells.Add(tCell2);

        TableCell tCell3 = new TableCell();
        tCell3.Height = 40;
        tCell3.Width = 160;
        tCell3.Text = "Likes";
        tRow.Cells.Add(tCell3);


        for (int i = 0; i < consultationUsers.getUsersForContributionsSize(); i++)
        {
            TableRow tRow2 = new TableRow();

            for (int j = 0; j < 3; j++)
            {

                TableCell tCellt = new TableCell();
                tCellt.Height = 40;
                tCellt.Width = 160;
                tCellt.Text = consultationUsers.getUsersForContributionsMatrix(i, j);
                tRow2.Cells.Add(tCellt);

            }
            tableClassification.Rows.Add(tRow2);
        }


    }

    protected void aply_Click(object sender, EventArgs e)
    {
        TableIdtipoDeCaminata.Rows.Clear();
        TableIdNivelDePrecio.Rows.Clear();
        TableIdNivelDeCalidad.Rows.Clear();
        TableIdNivelDeDificultad.Rows.Clear();

        if (groupList.Items[0].Selected == true)
        {
            makeTableTemp(groupList.Items[0].Text, 1);
        }

        if (groupList.Items[1].Selected == true)
        {
            makeTableTemp(groupList.Items[1].Text, 2);
        }

        if (groupList.Items[2].Selected == true)
        {
            makeTableTemp(groupList.Items[2].Text, 4);
        }

        if (groupList.Items[3].Selected == true)
        {
            makeTableTemp(groupList.Items[3].Text, 3);
        }


    }
}