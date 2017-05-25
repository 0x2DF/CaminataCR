using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class WallBusiness
{
    UserData userData = new UserData();

    public WallBusiness()
    {
    }

    public void LoadFriends(RegularUser regularUser)
    {
        HikeBusiness hikeBusiness = new HikeBusiness();
        regularUser.ListOfFriends = userData.LoadListOfFriends(regularUser);
        foreach (var e in regularUser.ListOfFriends)
        {
            e.ListOfHikes = hikeBusiness.LoadListOfHikes(e);
        }
    }

    public int InsertFriend(RegularUser user, RegularUser friend)
    {
        return userData.InsertFriend(user, friend);
    }

    public string SearchUsername(RegularUser regularUser)
    {
        bool validationType = userData.CheckUsername(regularUser);

        string errorText = "";

        if (validationType)
        {
            errorText = "No se ha encontrado al usuario con el nombre ";
        }

        return errorText;
    }
}