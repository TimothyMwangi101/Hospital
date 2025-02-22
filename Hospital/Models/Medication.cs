using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Models;

[Table("medications")]
public partial class Medication
{
    [Key]
    [Column("medication_id")]
    public int MedicationId { get; set; }

    [Column("medication_description")]
    [StringLength(25)]
    [Unicode(false)]
    [Display(Name = "Description")]
    public string MedicationDescription { get; set; } = null!;

    [Column("medication_cost", TypeName = "decimal(9, 2)")]
    [Display(Name = "Cost")]
    public decimal? MedicationCost { get; set; }

    [Column("package_size")]
    [StringLength(20)]
    [Unicode(false)]
    [Display(Name = "Package Size")]
    public string? PackageSize { get; set; }

    [Column("strength")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Strength { get; set; }

    [Column("sig")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Sig { get; set; }

    [Column("units_used_ytd")]
    [Display(Name = "Units Used YTD")]
    public int? UnitsUsedYtd { get; set; }

    [Column("last_prescribed_date")]
    [Display(Name = "Last Prescribed")]
    public DateOnly? LastPrescribedDate { get; set; }
}
