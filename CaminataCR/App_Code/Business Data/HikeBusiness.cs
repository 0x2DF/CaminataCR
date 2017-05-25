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

    public void InsertHike(Hike hike, RegularUser regularUser)
    {
        hikeData.InsertHike(hike, regularUser);
    }
}