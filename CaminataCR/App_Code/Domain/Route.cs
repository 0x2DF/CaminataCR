using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Route
{
    private int routeId;
    private List<Point> listOfPoints;

    public Route()
    {
    }

    public List<Point> ListOfPoints
    {
        get
        {
            return listOfPoints;
        }

        set
        {
            listOfPoints = value;
        }
    }

    public int RouteId
    {
        get
        {
            return routeId;
        }

        set
        {
            routeId = value;
        }
    }
}