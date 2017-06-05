using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class Notification
{
    private int type;
    private string message;

    public int Type
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

    public string Message
    {
        get
        {
            return message;
        }

        set
        {
            message = value;
        }
    }

    public Notification()
    {
       
    }
}