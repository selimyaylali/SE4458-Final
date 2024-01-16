using System;
using System.Collections.Generic;

namespace se4458_api.Model;

public partial class Transaction
{
    public int TransactionId { get; set; }

    public int? PharmacyId { get; set; }

    public DateTime? Date { get; set; }

    public decimal? TotalAmount { get; set; }

    public virtual Pharmacy? Pharmacy { get; set; }
}
