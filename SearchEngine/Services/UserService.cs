using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SearchEngine.Data;
using SearchEngine.Dtos;
using SearchEngine.Entity;
using SearchEngine.Service_Contract;

namespace SearchEngine.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly AppDbContext appDbContext;
     

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            this.appDbContext = appDbContext;
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            try
            {
                var user = await appDbContext.Users.FirstOrDefaultAsync(x=>x.Email==loginDto.Email);
                if (user == null) return null;

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
                if (!result.Succeeded) return null;

                return new UserDto()
                {
                    FullName = user.FullName,
                    Email = user.Email,
                    Token = await _tokenService.CreateTokenAsync(user, _userManager),
                };
            }
            catch (Exception ex)
            {
                // Log the error if logging is implemented
                // Logger.LogError(ex, "Error occurred during login.");
                throw new InvalidOperationException("An error occurred while processing the login.", ex);
            }
        }



        public async Task<bool> CheckEmailExistAsync(string email)
        {
            try
            {
                return await _userManager.FindByEmailAsync(email) is null;
            }
            catch (Exception ex)
            {
                // Log the error if logging is implemented
                // Logger.LogError(ex, "Error occurred while checking email existence.");
                throw new InvalidOperationException("An error occurred while checking the email existence.", ex);
            }
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            try
            {
                // Check if the user already exists based on the email
                var user = await appDbContext.Users.FirstOrDefaultAsync(x => x.Email == registerDto.Email);
                if (user != null)
                    throw new InvalidOperationException("A user with this email already exists.");

                // Create a new user object
                var newUser = new AppUser
                {
                    FullName = registerDto.FullName,
                    Email = registerDto.Email,
                    UserName = registerDto.Email, // You can also generate a UserName from FullName if you prefer
                    PhoneNumber = registerDto.PhoneNumber,
                    Address = registerDto.Address,
                    Age = registerDto.Age
                };

                // Attempt to create the user with the password
                var result = await _userManager.CreateAsync(newUser, registerDto.Password);
                if (!result.Succeeded)
                {
                    // Log the errors or return them for debugging
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"User creation failed: {errors}");
                }

                // Optionally, assign a default role
                // await _userManager.AddToRoleAsync(newUser, "User");

                // Create a token for the new user
                var token = await _tokenService.CreateTokenAsync(newUser, _userManager);

                // Return the UserDto with the created user's information and token
                return new UserDto
                {
                    FullName = newUser.FullName,
                    Email = newUser.Email,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                // Log the exception or handle further as needed
                throw new InvalidOperationException("An error occurred during registration.", ex);
            }
        }
    }
    }
