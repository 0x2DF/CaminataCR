using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class DonationBusiness
{
    DonationData donationData = new DonationData();
    public DonationBusiness()
    {
       
    }

    public void Donate(Donation donation)
    {
        donationData.InsertDonation(donation);
    }
}