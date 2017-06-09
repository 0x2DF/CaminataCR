using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class FriendBusiness
{
    UserData userData = new UserData();

    public FriendBusiness()
    {
    }
    
    public void LoadHikesOfFriends(ref RegularUser regularUser)
    {
        HikeBusiness hb = new HikeBusiness();
        LoadListOfFriends(ref regularUser);
        foreach (var e in regularUser.ListOfFriends)
        {
            e.ListOfHikes = hb.LoadListOfHikes(e);
        }
    }

    public void LoadListOfFriends(ref RegularUser regularUser)
    {
        regularUser.ListOfFriends = userData.LoadListOfFriends(regularUser);
    }

    public void LoadListOfRegularUsers(ref RegularUser regularUser, string name)
    {
        regularUser.ListOfFriends = userData.LoadListOfUsersNotFriends(regularUser, name);
    }

    public Tuple<int, string> InsertFriend(ref RegularUser user, ref RegularUser friend)
    {
        int id;
        string message;
        switch(userData.InsertFriend(user, friend))
        {
            case 0:
                id = 1;
                message = "Amistad creada!";
                break;
            case 1:
                id = 2;
                message = "Amistad ya existe";
                break;
            default:
                id = 2;
                message = "Error al agregar una amistad. Contacte a un administrador";
                break;
        }

        Tuple<int, string> n = new Tuple<int, string>(id, message);
        return n;
    }
    public Tuple<int, string> RemoveFriend(ref RegularUser user, ref RegularUser friend)
    {
        int id;
        string message;
        switch (userData.RemoveFriend(user, friend))
        {
            case 0:
                id = 1;
                message = "Ya no son amigos";
                break;
            case 1:
                id = 2;
                message = "Amistad no existe";
                break;
            default:
                id = 2;
                message = "Error al quitar una amistad. Contacte a un administrador";
                break;
        }

        Tuple<int, string> n = new Tuple<int, string>(id, message);
        return n;
    }
}