using System;
using System.Collections.Generic;

namespace Backend.Model;

public partial class Product
{
    public int Productid { get; set; }

    public string Type { get; set; } = null!;

    public string Sort { get; set; } = null!;

    public decimal Price { get; set; }

    public int Ostatok { get; set; }

    public string City { get; set; } = null!;

    public virtual ICollection<Sdelka> Sdelkas { get; set; } = new List<Sdelka>();
}
