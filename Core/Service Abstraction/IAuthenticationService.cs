
using Shared.Dtos;
using Shared.OrderModels;

namespace Domain.Contracts
{
    public interface IAuthenticationService
    {
       public Task<UserResultDto> LoginAsync(LoginDto loginDto);
         public Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        //Get Current User
        public Task<UserResultDto> GetUserByEmail(string email);

        //check if email exist
        public Task<bool> CheckEmailExist(string email);

        //Update user address
        public Task<AddressDto> UpdateUserAddress(AddressDto addressDto , string email);
        //Get User address
        public Task<AddressDto> GetUserAddress(string email);

    }
}
