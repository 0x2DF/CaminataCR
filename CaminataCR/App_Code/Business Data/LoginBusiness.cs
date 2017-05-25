using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class LoginBusiness
{
    UserData userData = new UserData();

    public LoginBusiness()
    {
    }

    public string ValidateCredentials(User user)
    {
        int errorId = userData.ValidateCredentials(user);

        string errorText = "";

        switch (errorId)
        {
            case 0:
                errorText = null;
                break;
            case 1:
                errorText = "Nombre de usuario incorrecto.";
                break;
            case 2:
                errorText = "Contraseña incorrecta.";
                break;
            case 3:
                errorText = "Su cuenta ha sido desactivada. Contacte a un administrador";
                break;
            default:
                errorText = "Hay un error no previsto. Contacte a un administrador";
                break;
        }
        return errorText;
    }

    public RegularUser GetRegularUser(User user)
    {
        RegularUser regularUser = userData.GetRegularUser(user);

        regularUser.UserId = user.UserId;
        regularUser.Account = user.Account;
        regularUser.RoleId = user.RoleId;

        return regularUser;
    }

}