using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de signInICT
/// </summary>
public class signInICT
{

    UserData userData = new UserData();

    public signInICT()
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
            default:
                errorText = "Hay un error no previsto. Contacte a un administrador";
                break;
        }
        return errorText;
    }


    public RegularUser GetRegularUser(User user)
    {
        RegularUser regularUser = userData.GetRegularUser(user);

        regularUser.Account = user.Account;
        regularUser.Password = user.Password;
        regularUser.RoleId = user.RoleId;

        return regularUser;
    }

}