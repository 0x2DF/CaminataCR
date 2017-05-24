using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class RegularUser : User
{
    private string firstName;
    private string middleName;
    private string surname;
    private string secondSurname;
    private string email;
    private string telephoneNumber;
    private DateTime birthdate;
    private char sex;
    private string nacionality;
    private string bankAccount;
    private byte[] profilePicture;
    private List<RegularUser> listOfFriends;
    private List<Hike> listOfHikes;
    private bool active;

    public string FirstName
    {
        get
        {
            return firstName;
        }

        set
        {
            firstName = value;
        }
    }

    public string MiddleName
    {
        get
        {
            return middleName;
        }

        set
        {
            middleName = value;
        }
    }

    public string Surname
    {
        get
        {
            return surname;
        }

        set
        {
            surname = value;
        }
    }

    public string SecondSurname
    {
        get
        {
            return secondSurname;
        }

        set
        {
            secondSurname = value;
        }
    }

    public string Email
    {
        get
        {
            return email;
        }

        set
        {
            email = value;
        }
    }

    public string TelephoneNumber
    {
        get
        {
            return telephoneNumber;
        }

        set
        {
            telephoneNumber = value;
        }
    }

    public DateTime Birthdate
    {
        get
        {
            return birthdate;
        }

        set
        {
            birthdate = value;
        }
    }

    public char Sex
    {
        get
        {
            return sex;
        }

        set
        {
            sex = value;
        }
    }

    public string Nacionality
    {
        get
        {
            return nacionality;
        }

        set
        {
            nacionality = value;
        }
    }

    public string BankAccount
    {
        get
        {
            return bankAccount;
        }

        set
        {
            bankAccount = value;
        }
    }

    public byte[] ProfilePicture
    {
        get
        {
            return profilePicture;
        }

        set
        {
            profilePicture = value;
        }
    }

    public List<RegularUser> ListOfFriends
    {
        get
        {
            return listOfFriends;
        }

        set
        {
            listOfFriends = value;
        }
    }

    public List<Hike> ListOfHikes
    {
        get
        {
            return listOfHikes;
        }

        set
        {
            listOfHikes = value;
        }
    }

    public bool Active
    {
        get
        {
            return active;
        }

        set
        {
            active = value;
        }
    }

    public RegularUser()
    {
    }
}