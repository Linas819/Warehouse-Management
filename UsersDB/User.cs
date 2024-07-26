using System;
using System.Collections.Generic;

namespace warehouse_management.UsersDB;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime CreatedDateTime { get; set; }

    public virtual ICollection<UsersAccess> UsersAccesses { get; } = new List<UsersAccess>();
}
