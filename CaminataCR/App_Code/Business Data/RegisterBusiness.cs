using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class RegisterBusiness
{
    UserData userData = new UserData();
    public RegisterBusiness()
    {

    }

    public void InsertRegularUser(RegularUser regularUser)
    {
        userData.InsertUser(regularUser);
        userData.InsertRegularUser(regularUser);
    }

    public List<string> Check(RegularUser regularUser)
    {
        List<string> errorList = new List<string>();

        if (!userData.CheckUsername(regularUser)) errorList.Add("El nombre de usuario ya existe en nuestro sistema.");
        if (!userData.CheckEmail(regularUser)) errorList.Add("El correo ya existe en nuestro sistema.");

        return errorList;
    }

}