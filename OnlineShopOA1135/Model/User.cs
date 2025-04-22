using System;
using System.Collections.Generic;

namespace OnlineShopOA1135.Model;
public partial class User
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }

    public static explicit operator UserModel(User user)
    {
        return new UserModel
        {
            Id = user.Id,
            RoleId = user.RoleId,
            Login = user.Login,
            Password = user.Password,
            Name = user.Name,
            Email = user.Email,
            Orders = user.Orders,
        };
    }
}

public partial class UserModel
{
    public int Id { get; set; }

    public int? RoleId { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}

public partial class ActiveUser
{
    private ActiveUser() { }

    //static ActiveUser instance;
    //public static ActiveUser Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //            instance = new ActiveUser();
    //        return instance;
    //    }
    //}
    private User user;
    public User GetUser()
    {
        return user;
    }

    public void SetUser(User value)
    {
        user = value;
    }
}