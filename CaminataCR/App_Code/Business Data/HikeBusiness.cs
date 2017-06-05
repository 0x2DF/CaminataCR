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

        return hikeData.LoadListOfHikes(regularUser);
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