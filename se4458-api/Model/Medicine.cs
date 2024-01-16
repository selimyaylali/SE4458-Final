using System;
using System.Collections.Generic;

namespace se4458_api.Model;

public partial class Medicine
{
    public int MedicineId { get; set; }

    public string? Name { get; set; }

    public decimal? Price { get; set; }

}
