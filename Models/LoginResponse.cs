﻿
namespace inventory_mobile_app.Models
{
    public class LoginResponse
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public string? UserName { get; set; }
    }
}
