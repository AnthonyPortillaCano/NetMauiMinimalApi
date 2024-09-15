using System;
using System.Collections.Generic;

namespace DataAccess.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Clave { get; set; }

    public string? Token { get; set; }
}
