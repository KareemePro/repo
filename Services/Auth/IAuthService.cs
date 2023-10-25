using FoodDelivery.Models.DominModels.Auth;

namespace FoodDelivery.Services.Auth
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> CustomerSignUp(CustomerSignUpModel model);
        Task<AuthModel> ChiefSignUp(ChiefSignUpModel model);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        Task<AuthModel> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}