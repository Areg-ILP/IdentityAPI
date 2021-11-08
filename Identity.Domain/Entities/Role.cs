using System.Collections.Generic;

namespace Identity.Domain.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public ICollection<Claim> UserClaims { get; set; }
    }
}
