using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class User
{
    protected int userId;
    protected string account;
    protected string password;
    protected int roleId;

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

    public int RoleId
    {
        get
        {
            return roleId;
        }

        set
        {
            roleId = value;
        }
    }

    public User()
    {
    }
}