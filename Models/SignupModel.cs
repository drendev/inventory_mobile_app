
namespace inventory_mobile_app.Models
{
    public class SignupModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
    }
}
