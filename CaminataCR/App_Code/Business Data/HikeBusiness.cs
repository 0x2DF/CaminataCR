using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class HikeBusiness
{
    HikeData hikeData = new HikeData();

    public HikeBusiness()
    {
    }

    public List<Hike> LoadListOfHikes(RegularUser regularUser)
    {

        regularUser.ListOfHikes = hikeData.LoadListOfHikes(regularUser);
        foreach(Hike h in regularUser.ListOfHikes)
        {
            h.Route = hikeData.LoadRoute(h);
        }
        return regularUser.ListOfHikes;
    }

    public void InsertHike(ref Hike hike, ref RegularUser regularUser)
    {
        int resultId = hikeData.InsertHike(ref hike, ref regularUser);
        resultId = hikeData.InsertRoute(resultId);
        foreach(Point p in hike.Route.ListOfPoints)
        {
            hikeData.InsertPoint(p, resultId);
        }
    }
    public void InsertHikeWithNewRoute(ref Hike hike, ref RegularUser regularUser)
    {
        int resultId = hikeData.InsertHikeWithHikeId(ref hike, ref regularUser);
        resultId = hikeData.InsertRoute(resultId);
        foreach (Point p in hike.Route.ListOfPoints)
        {
            hikeData.InsertPoint(p, resultId);
        }
    }
    public void InsertHikeWithExistingRoute(ref Hike hike, ref RegularUser regularUser)
    {
        int resultId = hikeData.InsertHikeWithHikeId(ref hike, ref regularUser);
        resultId = hikeData.InsertRouteWithRouteId(resultId, hike.Route.RouteId);
        foreach (Point p in hike.Route.ListOfPoints)
        {
            hikeData.InsertPointWithPointId(p, resultId);
        }
    }
    //ShowHike -> Existing Route
    public Hike loadHike(Hike hike)
    {
        Hike h = new Hike();
        h = hikeData.loadHikeInfo(hike.Route);
        h.Route.ListOfPoints = hikeData.loadPointInfo(h.Route);
        return h;
    }
    //HikeLobby -> Filter Hikes -> get info on one filtered hike
    public Hike loadInfoOfListOfHikes(Hike hike)
    {
        Hike h = new Hike();
        h = hikeData.loadHikeInfo(hike);
        h.ListOfRoutes = hikeData.loadRouteInfo(hike);
        foreach(Route r in h.ListOfRoutes)
        {
            r.ListOfPoints = hikeData.loadPointInfo(r);
        }
        return h;
    }

    public List<Hike> filterHikes(Hike hike, bool GPS)
    {
        return hikeData.LoadListOfHikes(hike, GPS);
    }

    public List<string> getProvinces()
    {
        return hikeData.LoadListOfProvinces();
    }
    public List<string> getCantons(string province)
    {
        return hikeData.LoadListOfCantons(province);
    }
    public List<string> getDistricts(string canton)
    {
        return hikeData.LoadListOfDistricts(canton);
    }
    public List<string> getHikeTypes()
    {
        return hikeData.LoadListOfHikeTypes();
    }
    public List<string> getDifficultyLevels()
    {
        return hikeData.LoadListOfDifficultyLevels();
    }
    public List<string> getQualityLevels()
    {
        return hikeData.LoadListOfQualityLevels();
    }
    public List<string> getPriceLevels()
    {
        return hikeData.LoadListOfPriceLevels();
    }
}