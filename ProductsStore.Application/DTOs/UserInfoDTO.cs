namespace ProductsStore.Application.DTOs
{
    public class UserInfoDTO
    {
        public required string UserId { get; set; }
        public required string UserName { get; set; }

        public List<string> Roles { get; set; } = [];
    }
}
