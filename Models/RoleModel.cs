namespace ServiceUser.Models
{
    public partial class RoleModel
    {
        public RoleId RoleId { get; set; }
        public string RoleName { get; set; }
        public virtual List<UserModel> Users { get; set; }
    }
    public enum RoleId
    {
        Admin = 0,
        User = 1
    }
}
