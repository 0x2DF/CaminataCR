using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Query
/// </summary>
public class Query
{
    private string startDate;
    private string finalDate;
    private string time;
    private string type;
    private string table;

    public string StartDate
    {
        get
        {
            return startDate;
        }

        set
        {
            startDate = value;
        }
    }

    public string FinalDate
    {
        get
        {
            return finalDate;
        }

        set
        {
            finalDate = value;
        }
    }

    public string Time
    {
        get
        {
            return time;
        }

        set
        {
            time = value;
        }
    }

    public string Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    public string Table
    {
        get
        {
            return table;
        }

        set
        {
            table = value;
        }
    }

    public Query()
    {

    }
}