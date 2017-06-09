using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class LikeBusiness
{
    LikeData likeData = new LikeData();
    public LikeBusiness()
    {
    }
    public Tuple<int, string> addLike(RegularUser user, Hike hike)
    {
        int id;
        string message;
        switch (likeData.InsertLike(user, hike))
        {
            case 0:
                id = 1;
                message = "Le has dado like a la caminata!";
                break;
            case 1:
                id = 2;
                message = "Ya le habias dado like a esta caminata!";
                break;
            default:
                id = 2;
                message = "Error al agregar un like. Contacte a un administrador";
                break;
        }

        Tuple<int, string> n = new Tuple<int, string>(id, message);
        return n;
    }
    public Tuple<int, string> removeLike(RegularUser user, Hike hike)
    {
        int id;
        string message;
        switch (likeData.RemoveLike(user, hike))
        {
            case 0:
                id = 1;
                message = "Has removido tu like de esta caminata!";
                break;
            case 1:
                id = 2;
                message = "No se le puede remover un like a la caminata que no le has dado like.";
                break;
            default:
                id = 2;
                message = "Error al agregar un like. Contacte a un administrador";
                break;
        }

        Tuple<int, string> n = new Tuple<int, string>(id, message);
        return n;
    }
    public bool getLikeStatus(RegularUser user, Hike hike)
    {
        if(likeData.getLikeStatus(user, hike) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}