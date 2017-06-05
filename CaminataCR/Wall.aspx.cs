using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Wall : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["REG_USER"] == null)
        {
            Response.Redirect("SignInRegular.aspx");
        }
        else
        {
            FriendBusiness fb = new FriendBusiness();
            RegularUser regularUser = (RegularUser)Session["REG_USER"];

            LoggedInUsername.Text = regularUser.Account;
            if (regularUser.ProfilePicture == null)
            {
                //ImageUser.Attributes["src"] = "/css/images/defaultThumb.png";
            }
            else
            {
                //ImageUser.Attributes["src"] = "data:Image/png;base64," + Convert.ToBase64String(regularUser.ProfilePicture);
            }

            //fb.LoadHikesOfFriends(ref regularUser);
            //In the last month

            //displayHikesOfFriends(regularUser);
        }
    }

    protected void displayHikesOfFriends(RegularUser regularUser)
    {
        foreach (var e in regularUser.ListOfFriends)
        {
            TableRow tr = new TableRow();
            TableCell tc = new TableCell();

            /*foreach (var a in e.ListOfHikes)
            {
                tc.Text = string.Format(@"<div class='comments-container'>
                                            <ul id = 'comments-list' class='comments-list'>
                                                <li>
                                                    <div class='comment-main-level'>
                                                        <!-- Avatar -->
                                                        <div class='comment-avatar'><img src = 'css/images/imagen2.jpg' alt=''/></div>
                                                        <!-- Contenedor del Comentario -->
                                                        <div class='comment-box'>
                                                            <div class='comment-head'>
                                                                <h6 class='comment-name by-author'>{0}</h6>
                                                                <span>{1:d} {1:t}</span>
                                                                <span1>{2}</span1>
                                                                <span2>{3}</span2>
                                                                <span3>{4}</span3>
                                                                <div class='comment-restaurant'><img src = 'css/images/imagen2.jpg' alt=''/></div>
                                                            </div>
                                                            <div class='comment-content'>
                                                                {5}
                                                            </div>
                                                        </div>
                                                    </div>
                                                </li>
                                            </ul>
                                        </div>", e.NombreDeCuenta, a.FechaHora, a.CalificacionCalidad, a.CalificacionPrecio, a.Restaurant.Nombre, a.Descripcion);

                tr.Cells.Add(tc);
                Reviews.Rows.Add(tr);
            }*/
        }
    }
}