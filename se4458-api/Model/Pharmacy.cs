using System;
using System.Collections.Generic;

namespace se4458_api.Model;

public partial class Pharmacy
{
    public int PharmacyId { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? AuthenticationCredentials { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; } = new List<Prescription>();

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
