namespace Identity.Domain.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int AccessFailedCount { get; set; }

        //mb
        public string PhoneNumber { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool EmailConfirmed { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
