using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Diagnostics;

public partial class Donations : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["REG_USER"] == null)
        {
            Response.Redirect("SignInRegular.aspx");
        }
        else
        {
            RegularUser regularUser = (RegularUser)Session["REG_USER"];
            LoggedInUsername.Text = regularUser.Account;
        }
    }
    protected void Donate(object sender, EventArgs e)
    {
        List<string> errorList = new List<string>();
        errorList = validateInput();

        if (!errorList.Any())
        {
            RegularUser regularUser = (RegularUser)Session["REG_USER"];

            LoginBusiness lb = new LoginBusiness();
            User user = new User();
            user.Account = regularUser.Account;
            user.Password = password.Text;
            user.RoleId = 3;

            string errorText = lb.ValidateCredentials(user);

            if (errorText == null)
            {
                DonationBusiness db = new DonationBusiness();
                Donation donation = new Donation();
                donation.Amount = Convert.ToDouble(amount.Text);
                db.Donate(donation);

                Notification n = new Notification();
                n.Type = 1;
                n.Message = donation.Amount.ToString() + " fue donado exitosamente!";
                Session["NOTIFICATION"] = n;
                Response.Redirect("Notifications.aspx");
            }
            else
            {
                Errors.Text = errorText;
            }
        }
        else
        {
            //right_side.Attributes.Add("style", "display:block");
            outputErrors(errorList);
        }
    }

    protected List<string> validateInput()
    {
        List<string> errorList = new List<string>();
        
        //rPassword : 1 upper case, 1 lower case, 1 digit, 1 special char, min 8 length
        Regex rPassword = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#?!@$%^&*-])[A-Za-z\\d#?!@$%^&*-]{8,}$");
        //rBankAccount : numeric
        Regex rAmount = new Regex("^[0-9]*$");
       
        //Contraseña
        if ((password.Text.Length == 0) || (password.Text.Length > 20)) errorList.Add("Contraseña : Tamaño incorrecto [20] maximo.");
        if (!rPassword.IsMatch(password.Text)) errorList.Add("Contraseña : Minimo 1 caracter en mayuscula, 1 en minuscula, 1 numero, y con longitud [8] minimo.");

       
        //Cantidad
        if ((amount.Text.Length == 0) || (amount.Text.Length > 20)) errorList.Add("Cantidad : Tamaño incorrecto [20] maximo.");
        if (!rAmount.IsMatch(amount.Text)) errorList.Add("Cantidad : Solo caracteres numericos.");
        if (Int32.Parse(amount.Text) <= 0) errorList.Add("Cantidad : Tiene que ser mayor a 0");


        return errorList;
    }

    protected void outputErrors(List<string> errorList)
    {
        foreach (string error in errorList)
        {
            Errors.Text += error;
            Errors.Text += " <br> ";
        }
    }
}