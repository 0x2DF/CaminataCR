using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class User
{
    protected int userId;
    protected string account;
    protected string password;
    protected string role;

    public int UserId
    {
        get
        {
            return userId;
        }

        set
        {
            userId = value;
        }
    }

    public string Account
    {
        get
        {
            return account;
        }

        set
        {
            account = value;
        }
    }

    public string Password
    {
        get
        {
            return password;
        }

        set
        {
            password = value;
        }
    }

    public string Role
    {
        get
        {
            return role;
        }

        set
        {
            role = value;
        }
    }

    public User()
    {
    }
}