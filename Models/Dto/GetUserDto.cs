namespace ServiceUser.Models.Dto
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public RoleId RoleId { get; set; }
    }
}
