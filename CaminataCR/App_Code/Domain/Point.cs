using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Point
{
    private double longitud;
    private double latitud;
    private int pos;
    private string comment;
    private byte[] image;

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