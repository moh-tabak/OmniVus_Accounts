namespace OmniVus_Accounts.Models.ViewModels
{
#nullable disable
    public class NavbarViewModel
    {
        public NavbarViewModel(string currentRoute)
        {
            CurrentRoute = currentRoute;
        }

        public NavbarViewModel(string currentRoute, string accountId, string fullName) : this(currentRoute)
        {
            AccountId = accountId;
            FullName = fullName;
        }

        public string CurrentRoute { get; set; }

        public string AccountId { get; set; }

        public string FullName { get; set; }
    }
}
