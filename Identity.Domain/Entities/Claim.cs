
namespace Identity.Domain.Entities
{
    public class Claim : Entity
    {
        public string Name { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
