//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP_Gym_App.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HealthRecord
    {
        public int HealthRecordId { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> Height { get; set; }
        public Nullable<decimal> BMR { get; set; }
        public Nullable<decimal> BMI { get; set; }
        public Nullable<decimal> KCAL { get; set; }
        public Nullable<decimal> TargetBMI { get; set; }
    
        public virtual User User { get; set; }
    }
}
