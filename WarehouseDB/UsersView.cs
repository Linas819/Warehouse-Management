using System;
using System.Collections.Generic;

namespace warehouse_management.WarehouseDB;

public partial class UsersView
{
    public string UserId { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
}
