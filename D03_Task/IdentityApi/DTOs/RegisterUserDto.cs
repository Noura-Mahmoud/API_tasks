namespace IdentityApi.DTOs
{
    public record RegisterUserDto(string UserName, string Email, string Password, string Department);
}
