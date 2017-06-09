using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DificultyLevel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DificultyLevelBusiness business = new DificultyLevelBusiness();

            List<string> dificultyLevelList = business.getDificultyLevels();
            showDificultyLevels(dificultyLevelList);
            Session["DificultyLeveslList"] = dificultyLevelList;
        }
        else
        {
            List<string> dificultyLevelList = (List<string>)Session["DificultyLeveslList"];
            showDificultyLevels(dificultyLevelList);
        }
    }

    protected void showDificultyLevels(List<string> dificultyList)
    {
        dificultyLevelS.InnerHtml = "";

        foreach (string walkTypeName in dificultyList)
        {
            System.Web.UI.WebControls.Table tableInfo = new System.Web.UI.WebControls.Table();
            tableInfo.Attributes["class"] = "table table-striped table-bordered table-hover";

            TableRow tableRow = new TableRow();
            TableCell tableCell1 = new TableCell();
            tableCell1.Attributes["class"] = "col-md-3 text-center";
            TableCell tableCell2 = new TableCell();
            tableCell2.Attributes["class"] = "col-md-3 text-center";

            Label labelWalkType = new Label();
            labelWalkType.Text = walkTypeName;

            tableCell1.Controls.Add(labelWalkType);
            tableRow.Cells.Add(tableCell1);

            tableInfo.Rows.Add(tableRow);

            tableCell2.Controls.Add(addButton(walkTypeName));
            tableRow.Cells.Add(tableCell2);

            dificultyLevelS.Controls.Add(tableInfo);

        }
    }

    public Button addButton(string walkType)
    {
        Button btn = new Button();
        btn.Attributes["class"] = "btn btn-outline btn-success";
        btn.Click += new System.EventHandler(editDificultyLevel);
        btn.Text = "Editar";
        btn.ID = walkType;
        return btn;
    }

    public void editDificultyLevel(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string dificulty = btn.ID;
        Session["EditDificulty"] = dificulty;
        Response.Redirect("EditDificultyLevel.aspx");
    }

    public void addDificultyLevel(object sender, EventArgs e)
    {
        Response.Redirect("AddDificultyLevel.aspx");
    }
}