using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class Donation
{
    private float amount;
    private DateTime dateTime;
    public Donation()
    {
    }

    public float Amount
    {
        get
        {
            return amount;
        }

        set
        {
            amount = value;
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
}