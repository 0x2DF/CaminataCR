using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserAdministrator : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UsersBusiness business = new UsersBusiness();
            int role = 1;
            string loggedUser = (string)Session["ADMIN"];

            List<User> userList = business.getUsersList(role,loggedUser);

            showUsers(userList);
            Session["UsersList"] = userList;
        }
        else
        {
            List<User> userList = (List<User>)Session["UsersList"];
            showUsers(userList);
        }
    }

    protected void showUsers(List<User> usersList)
    {
        administratorS.InnerHtml = "";

        foreach (User user in usersList)
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

            tableCell2.Controls.Add(addButton(user));
            tableRow.Cells.Add(tableCell2);

            administratorS.Controls.Add(tableInfo);

        }
    }

    public Button addButton(User user)
    {
        Button btn = new Button();
        btn.Attributes["class"] = "btn btn-outline btn-success";
        btn.Click += new System.EventHandler(editUser);
        btn.Text = "Editar";

        btn.ID = user.Account;
        return btn;
    }

    protected void editUser(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        string userName = btn.ID;
        Session["editUser"] = userName;
        Response.Redirect("EditAdministrator.aspx");
    }

    protected void addAdministrator(object sender, EventArgs e)
    {
        Response.Redirect("AddAdministrator.aspx");
    }
}