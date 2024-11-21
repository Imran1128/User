using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Nodes;

namespace UserRegistration.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTimeOffset LastLogin { get; set; }
        //public string? Address { get; set; }
        public int loginCount { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<LoginCount> loginCountsByDate { get; set; }
    }

}