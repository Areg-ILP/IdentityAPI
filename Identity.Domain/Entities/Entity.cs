using System;

namespace Identity.Domain.Entities
{
    public class Entity
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Today;
    }
}
