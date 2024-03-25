namespace ServiceUser.Models
{
    public partial class UserModel : BaseModel
    {
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } = null!;
        public RoleId RoleId { get; set; }
        public virtual RoleModel Role { get; set; }

    }
}
