using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Hike
{
    private string hikeType;
    private string dificulty;
    private string price;
    private string quality;
    private string province;
    private string canton;
    private string district;

    private string nameOfLocation;
    private string details;
    private float longitud;
    private float latitud;

    private DateTime dateTime;
    private byte[] image;
    private string comment;
    private bool finished;

    private Route route;

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

    public string Dificulty
    {
        get
        {
            return dificulty;
        }

        set
        {
            dificulty = value;
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

    public float Longitud
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

    public float Latitud
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

    public Hike()
    {

    }
}