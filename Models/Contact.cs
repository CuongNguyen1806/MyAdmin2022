using System;
using System.Collections.Generic;

namespace MyAdmin2022.Models
{
    public partial class Contact
    {
        public int ContactId { get; set; }
        public string? FullName { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Content { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ContactCategoryId { get; set; }
        public string? ApproveBy { get; set; }

        public virtual Account? ApproveByNavigation { get; set; }
        public virtual ContactCategory? ContactCategory { get; set; }
    }
}
