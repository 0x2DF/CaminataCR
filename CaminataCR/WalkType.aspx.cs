using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

public partial class WalkType : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WalkTypeBusiness business = new WalkTypeBusiness();

            List<string> walkTypeList = business.getWalkTypes();
            showWalkTypes(walkTypeList);
            Session["WalkTypesList"] = walkTypeList;
        }
        else
        {
            List<string> walkTypeList = (List<string>)Session["WalkTypesList"];
            showWalkTypes(walkTypeList);
        }
    }

    public void showWalkTypes(List<string> walkTypeList)
    {
        walkTypeListS.InnerHtml = "";      
        
        foreach (string walkTypeName in walkTypeList)
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
            
            walkTypeListS.Controls.Add(tableInfo);                    
                
        }        
    }


    public Button addButton(string walkType)
    {
        Button btn = new Button();
        btn.Attributes["class"] = "btn btn-outline btn-success";
        btn.Click += new System.EventHandler(editWalkType);
        btn.Text = "Editar";
        btn.ID = walkType;
        return btn;
    }

    protected void editWalkType(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string name = btn.ID;
        Session["EditWalk"] = name;
        Response.Redirect("EditWalkType.aspx");
    }

    protected void addWalktype(object sender, EventArgs e)
    {
        Response.Redirect("AddWalkType.aspx");
    }
}