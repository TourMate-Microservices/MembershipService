using System;
using System.Collections.Generic;

namespace TourMate.MembershipService.Repositories.Models;

public partial class MembershipPackage
{
    public int MembershipPackageId { get; set; }

    public string Name { get; set; } = null!;

    public float Price { get; set; }

    public int Duration { get; set; }

    public string BenefitDesc { get; set; } = null!;
}
