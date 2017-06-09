using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Diagnostics;

public partial class SignUpRegular : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["REG_USER"] != null)
        {
            Response.Redirect("ControlPanel.aspx");
        }
    }

    protected void SignUp(object sender, EventArgs e)
    {
        List<string> errorList = new List<string>();
        errorList = validateInput();

        if (!errorList.Any())
        {
            RegisterBusiness rb = new RegisterBusiness();
            
            
            RegularUser regularUser = new RegularUser();

            regularUser.Account = username.Text;
            regularUser.Password = password.Text;

            regularUser.FirstName = fname.Text;

            if (mname.Text.Length == 0)
            {
                regularUser.MiddleName = null;
            }
            else
            {
                regularUser.MiddleName = mname.Text;
            }

            regularUser.Surname = sname.Text;
            regularUser.SecondSurname = sname2.Text;
            regularUser.Email = email.Text;
            regularUser.TelephoneNumber = phone.Text;
            regularUser.Birthdate = DateTime.ParseExact(bdate.Text, "yyyy/MM/dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
            if (sex.SelectedItem.ToString() == "Masculino")
            {
                regularUser.Sex = 'm';
            }
            else
            {
                regularUser.Sex = 'f';
            }
            regularUser.Nacionality = nacionality.Text;
            regularUser.RoleId = 3;
            regularUser.BankAccount = bankAccount.Text;
            if (imageupload.HasFile)
            {
                regularUser.ProfilePicture = imageupload.FileBytes;
            }
            else
            {
                regularUser.ProfilePicture = null;
            }

            errorList.Clear();
            errorList = rb.Check(regularUser);
            if (!errorList.Any())
            {
                rb.InsertRegularUser(regularUser);
                Response.Redirect("SignInRegular.aspx");
            }
            else
            {
                //right_side.Attributes.Add("style", "display:block");
                outputErrors(errorList);
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
        //rUsername : alphanumeric, min 3 length
        Regex rUsername = new Regex("^[a-zA-Z0-9]*.{3,}$");
        //rPassword : 1 upper case, 1 lower case, 1 digit, 1 special char, min 8 length
        Regex rPassword = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#?!@$%^&*-])[A-Za-z\\d#?!@$%^&*-]{8,}$");
        //rEmail : de la forma [alphanum @ alphanum]
        Regex rEmail = new Regex("^([a-zA-Z0-9])+([a-zA-Z0-9\\._-])*@([a-zA-Z0-9_-])+([a-zA-Z0-9\\._-]+)+$");
        //rName : alpha
        Regex rName = new Regex("^[a-zA-Z]*$");
        //rNacionality : alpha || alpha + ' ' + alpha
        Regex rNacionality = new Regex("^([a-zA-Z]|([a-zA-Z]+\\s[a-zA-Z]))*$");
        //rPhone : (XXX)YYYY-ZZZZ con X,Y,Z numeric
        Regex rPhone = new Regex("^\\([0-9]{3}\\)[0-9]{4}-[0-9]{4}$");
        //rBankAccount : numeric
        Regex rBankAccount = new Regex("^[0-9]*$");

        //Nombre De Usuario
        if ((username.Text.Length == 0) || (username.Text.Length > 20)) errorList.Add("Nombre de usuario : Tamaño incorrecto [20] maximo.");
        if (!rUsername.IsMatch(username.Text)) errorList.Add("Nombre de usuario : Solo se permiten caracters alphanumericos con longitud [3] minimo.");

        //Contraseña
        if ((password.Text.Length == 0) || (password.Text.Length > 20)) errorList.Add("Contraseña : Tamaño incorrecto [20] maximo.");
        if (!rPassword.IsMatch(password.Text)) errorList.Add("Contraseña : Minimo 1 caracter en mayuscula, 1 en minuscula, 1 numero, y con longitud [8] minimo.");

        //Correo
        if ((email.Text.Length == 0) || (email.Text.Length > 50)) errorList.Add("Correo : Tamaño incorrecto [50] maximo.");
        if (!rEmail.IsMatch(email.Text)) errorList.Add("Correo : Invalido.");

        //Primer nombre
        if ((fname.Text.Length == 0) || (fname.Text.Length > 20)) errorList.Add("Primer nombre : Tamaño incorrecto [20] maximo.");
        if (!rName.IsMatch(fname.Text)) errorList.Add("Primer nombre : Solo caracteres alfabeticos.");

        //Segundo nombre
        if (mname.Text.Length != 0)
        {
            if (mname.Text.Length > 20) errorList.Add("Segundo nombre : Tamaño incorrecto [20] maximo.");
            if (!rName.IsMatch(mname.Text)) errorList.Add("Segundo nombre : Solo caracteres alfabeticos.");
        }

        //Primer apellido
        if ((sname.Text.Length == 0) || (sname.Text.Length > 20)) errorList.Add("Primer apellido : Tamaño incorrecto [20] maximo.");
        if (!rName.IsMatch(sname.Text)) errorList.Add("Primer apellido : Solo caracteres alfabeticos.");

        //Segundo apellido
        if ((sname2.Text.Length == 0) || (sname2.Text.Length > 20)) errorList.Add("Segundo apellido : Tamaño incorrecto [20] maximo.");
        if (!rName.IsMatch(sname2.Text)) errorList.Add("Segundo apellido : Solo caracteres alfabeticos.");

        //Nacionalidad
        if ((nacionality.Text.Length == 0) || (nacionality.Text.Length > 20)) errorList.Add("Nacionalidad : Tamaño incorrecto [20] maximo.");
        if (!rNacionality.IsMatch(nacionality.Text)) errorList.Add("Nacionalidad : Solo caracteres alfabeticos.");
        
        //Telefono
        //if (phone.Text.Length == 0) errorList.Add("Telefono : Tamaño incorrecto");
        //if (!rPhone.IsMatch(phone.Text)) errorList.Add("Telefono : (XXX)YYYY-ZZZZ con X,Y,Z numeric.");

        //Cuenta Bancaria
        if ((bankAccount.Text.Length == 0) || (bankAccount.Text.Length > 20)) errorList.Add("Cuenta Bancaria : Tamaño incorrecto [20] maximo.");
        if (!rBankAccount.IsMatch(bankAccount.Text)) errorList.Add("Cuenta Bancaria : Solo caracteres numericos.");

        //fotografia
        if (imageupload.HasFile)
        {
            string extension = System.IO.Path.GetExtension(imageupload.FileName);
            if ((extension != ".jpg") && (extension != ".png"))
            {
                errorList.Add("Fotografia : Solo se permiten '.jpg' y '.png'");
            }
        }

        return errorList;
    }

    protected void outputErrors(List<string> errorList)
    {
        Errors.Text = "";
        foreach (string error in errorList)
        {
            Errors.Text += error;
            Errors.Text += " <br> ";
        }
    }
}