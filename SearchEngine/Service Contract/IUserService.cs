using SearchEngine.Dtos;

namespace SearchEngine.Service_Contract
{
    public interface IUserService
    {
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<bool> CheckEmailExistAsync(string email);

        Task<UserDto> RegisterAsync(RegisterDto registerDto);





    }
}
