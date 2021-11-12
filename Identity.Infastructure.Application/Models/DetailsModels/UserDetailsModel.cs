namespace Identity.Infastructure.Application.Models.DetailsModels
{
    public class UserDetailsModel : BaseDetailsModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int AccessFailedCount { get; set; }
        public string PhoneNumber { get; set; }
        public string SecurityStamp { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
