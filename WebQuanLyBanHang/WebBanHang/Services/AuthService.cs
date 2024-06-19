using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NguyenTuanK55.Models;
namespace NguyenTuanK55.Services
{

    public class AuthService : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navigationManager;
        private readonly IServiceScopeFactory _scopeFactory;
        private static NguoiDung _currentUser;

        public event Action OnChange;

        public AuthService(IJSRuntime jsRuntime, NavigationManager navigationManager, IServiceScopeFactory scopeFactory)
        {
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _scopeFactory = scopeFactory;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "user");
            var identity = string.IsNullOrEmpty(user) ? new ClaimsIdentity() : new ClaimsIdentity(ParseClaimsFromJwt(user), "jwt");

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        public async Task Login(UserLoginModel user)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var dbUser = await dbContext.NguoiDungs
                    .FirstOrDefaultAsync(u => u.Email == user.Username && u.MatKhau == user.Password);

                if (dbUser != null)
                {
                    _currentUser = dbUser;
                    var token = GenerateJwtToken(dbUser);
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "user", token);
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                    OnChange?.Invoke();
                    _navigationManager.NavigateTo("/");
                }
            }
        }

        public async Task Logout()
        {
            _currentUser = null;
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "user");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            OnChange?.Invoke();
            _navigationManager.NavigateTo("/login");
        }

        private string GenerateJwtToken(NguoiDung user)
        {
            // Simulate token generation
            return $"{user.Email}.{user.HoTen}.{user.NguoiDungId}.{user.Cap}";
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var parts = jwt.Split('.');
            return new List<Claim>
        {
            new Claim(ClaimTypes.Name, parts[0]),
            new Claim("FullName", parts[1]),
            new Claim(ClaimTypes.NameIdentifier, parts[2]),
            new Claim("Cap", parts[3])
        };
        }

        public bool IsAuthenticated()
        {
            return _currentUser != null;
        }

        public string GetUserName()
        {
            return _currentUser?.HoTen;
        }

        public int GetUserId()
        {
            return _currentUser?.NguoiDungId ?? 0;
        }

        public int GetUserCap()
        {
            return _currentUser?.Cap ?? 3;
        }
    }
}