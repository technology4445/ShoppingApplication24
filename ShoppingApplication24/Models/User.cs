namespace ShoppingApplication24.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string AccessTocken { get; set; }
        public DateTime JoinOn { get; set; }
        public virtual Role Role { get; set; }
        public int RoleId { get; set; }


    }
}
