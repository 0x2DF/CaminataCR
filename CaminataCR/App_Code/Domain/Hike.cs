using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Hike
{
    private int hikeId;
    private string hikeType;
    private string difficulty;
    private string price;
    private string quality;
    private string province;
    private string canton;
    private string district;

    private string nameOfLocation;
    private string details;
    private double longitud;
    private double latitud;

    private DateTime dateTime;
    private byte[] image;
    private string comment;
    private bool finished;

    private Route route;
    private List<Route> listOfRoutes;
    private int likes;

    public string HikeType
    {
        get
        {
            return hikeType;
        }

        set
        {
            hikeType = value;
        }
    }

    public string Difficulty
    {
        get
        {
            return difficulty;
        }

        set
        {
            difficulty = value;
        }
    }

    public string Price
    {
        get
        {
            return price;
        }

        set
        {
            price = value;
        }
    }

    public string Quality
    {
        get
        {
            return quality;
        }

        set
        {
            quality = value;
        }
    }

    public string Province
    {
        get
        {
            return province;
        }

        set
        {
            province = value;
        }
    }

    public string Canton
    {
        get
        {
            return canton;
        }

        set
        {
            canton = value;
        }
    }

    public string District
    {
        get
        {
            return district;
        }

        set
        {
            district = value;
        }
    }

    public string NameOfLocation
    {
        get
        {
            return nameOfLocation;
        }

        set
        {
            nameOfLocation = value;
        }
    }

    public string Details
    {
        get
        {
            return details;
        }

        set
        {
            details = value;
        }
    }

    public double Longitud
    {
        get
        {
            return longitud;
        }

        set
        {
            longitud = value;
        }
    }

    public double Latitud
    {
        get
        {
            return latitud;
        }

        set
        {
            latitud = value;
        }
    }

    public DateTime DateTime
    {
        get
        {
            return dateTime;
        }

        set
        {
            dateTime = value;
        }
    }

    public byte[] Image
    {
        get
        {
            return image;
        }

        set
        {
            image = value;
        }
    }

    public string Comment
    {
        get
        {
            return comment;
        }

        set
        {
            comment = value;
        }
    }

    public bool Finished
    {
        get
        {
            return finished;
        }

        set
        {
            finished = value;
        }
    }

    public Route Route
    {
        get
        {
            return route;
        }

        set
        {
            route = value;
        }
    }

    public int Likes
    {
        get
        {
            return likes;
        }

        set
        {
            likes = value;
        }
    }

    public int HikeId
    {
        get
        {
            return hikeId;
        }

        set
        {
            hikeId = value;
        }
    }

    public List<Route> ListOfRoutes
    {
        get
        {
            return listOfRoutes;
        }

        set
        {
            listOfRoutes = value;
        }
    }

    public Hike()
    {

    }
}