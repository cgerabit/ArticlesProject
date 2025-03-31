namespace ProductsStore.Application.DTOs
{
    public class LoginResponseDTO
    {

        public bool IsSuccess { get; set; }
        public JwtResponse Response { get; set; }
    }
}
