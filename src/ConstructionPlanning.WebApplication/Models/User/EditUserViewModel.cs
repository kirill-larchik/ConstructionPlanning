namespace ConstructionPlanning.WebApplication.Models.User
{
    public class EditUserViewModel : CreateUserViewModel
    {
        public string Id { get; set; }

        public bool IsCurrentAdmin { get; set; }
    }
}
