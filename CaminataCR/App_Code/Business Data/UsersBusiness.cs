using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de UsersBusiness
/// </summary>
public class UsersBusiness
{
    UserData userData = new UserData();
    public UsersBusiness()
    {
    }

    public List<User> getRegularUsers()
    {
        List<User> regularUsers = userData.getRegularUsers();
        return regularUsers;
    }

    public void editRegularUser(string userName, int state)
    {
        userData.editRegularUser(userName, state);
    }

    public string addUser(User user)
    {
        string error = userData.InsertUser(user);
        string alreadyExists = null;
        if(!error.Equals("-------------------------------------------------------------------"))
        {
            alreadyExists = "Este nombre de usuario ya existe";
            return alreadyExists;
        }
        else
        {
            return alreadyExists;
        }
    }

    public List<User> getUsersList(int roleId, string userName)
    {
        List<User> usersList = userData.getUsers(roleId, userName);
        return usersList;
    }

   

    public string editUser(string newUserName, string oldUserName)
    {
        int error = userData.editUser(newUserName,oldUserName);
        string errorResponse;
        if( error == 1)
        {
            errorResponse = "Este nombre de usuario ya existe";
        }
        else
        {
            errorResponse = null;
        }
        return errorResponse;
    }

    public void deleteUser(string userName)
    {
        userData.deletUser(userName);
    }
}