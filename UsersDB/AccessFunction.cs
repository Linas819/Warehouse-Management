using System;
using System.Collections.Generic;

namespace warehouse_management.UsersDB;

public partial class AccessFunction
{
    public string AccessId { get; set; } = null!;

    public string AccessName { get; set; } = null!;

    public virtual ICollection<UsersAccess> UsersAccesses { get; } = new List<UsersAccess>();
}
