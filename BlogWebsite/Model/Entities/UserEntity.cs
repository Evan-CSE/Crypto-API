using Microsoft.AspNetCore.Http.HttpResults;
using System.Data;

namespace BlogWebsite.Model.Entities
{
    public enum UserRoles 
    {
        Guest,
        Subscriber,
        Admin
    }
    public class UserEntity
    {
        public Guid Id { get; set; }
        
        public string UserName { get; set; }
        public string Password { get; set; }
        public  UserRoles Roles { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset SubscriptionEndDate { get; set; }
    }
}
