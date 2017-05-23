using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Point
{
    private float longitud;
    private float latitud;
    private int pos;
    private string comment;
    private byte[] image;

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

    public int Pos
    {
        get
        {
            return pos;
        }

        set
        {
            pos = value;
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

    public Point()
    {
    }
}