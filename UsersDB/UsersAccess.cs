using System;
using System.Collections.Generic;

namespace warehouse_management.UsersDB;

public partial class UsersAccess
{
    public int UserAccessId { get; set; }

    public string AccessId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual AccessFunction Access { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
