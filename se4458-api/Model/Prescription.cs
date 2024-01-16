using System;
using System.Collections.Generic;

namespace se4458_api.Model;

public partial class Prescription
{
    public int PrescriptionId { get; set; }

    public int? PharmacyId { get; set; }

    public int? MedicineId {get; set;}

    public string? PatientTc { get; set; }

    public DateTime? Date { get; set; }

    public decimal? TotalCost { get; set; }

    public virtual Pharmacy? Pharmacy { get; set; }

}
