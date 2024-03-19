using System;
using System.Collections.Generic;

namespace MyAdmin2022.Models
{
    public partial class Account
    {
        public Account()
        {
            ArticleCategories = new HashSet<ArticleCategory>();
            Articles = new HashSet<Article>();
            ContactCategories = new HashSet<ContactCategory>();
            Contacts = new HashSet<Contact>();
        }

        public string Username { get; set; } = null!;
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        public string? Thumb { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public string? Address { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? AccountCategoryId { get; set; }

        public virtual ICollection<ArticleCategory> ArticleCategories { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<ContactCategory> ContactCategories { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
