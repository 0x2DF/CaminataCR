using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserRegular : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        UsersBusiness business = new UsersBusiness();

        List<User> regularUserList = business.getRegularUsers();
        showRegularUsers(regularUserList);      

    }

    protected void showRegularUsers(List<User> regularUsersList)
    {
        regularUsersS.InnerHtml = "";

        foreach (User user in regularUsersList)
        {
            System.Web.UI.WebControls.Table tableInfo = new System.Web.UI.WebControls.Table();
            tableInfo.Attributes["class"] = "table table-striped table-bordered table-hover";

            TableRow tableRow = new TableRow();
            TableCell tableCell1 = new TableCell();
            tableCell1.Attributes["class"] = "col-md-3 text-center";
            TableCell tableCell2 = new TableCell();
            tableCell2.Attributes["class"] = "col-md-3 text-center";

            Label labelUserName = new Label();
            labelUserName.Text = user.Account;

            tableCell1.Controls.Add(labelUserName);
            tableRow.Cells.Add(tableCell1);

            tableInfo.Rows.Add(tableRow);

            tableCell2.Controls.Add(addButton(user.Account,user.State));
            tableRow.Cells.Add(tableCell2);

            regularUsersS.Controls.Add(tableInfo);

        }
    }

    public Button addButton(string userName, bool state)
    {
        Button btn = new Button();
        btn.Attributes["class"] = "btn btn-outline btn-success";
        btn.Click += new System.EventHandler(editRegularUser);
        if (state)
        {
            btn.Text = "Inhabilitar";
        }

        else
        {
            btn.Text = "Habilitar";
        }
        
        btn.ID = userName;
        return btn;
    }   

    protected void editRegularUser(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string userName = btn.ID;
        string stateName = btn.Text;
        int state;

        if(stateName.Equals("Habilitar"))
        {
            state = 1;
        }
        else
        {
            state = 0;
        }

        UsersBusiness business = new UsersBusiness();
        business.editRegularUser(userName, state);
        Response.Redirect("UserRegular.aspx");
    }
}