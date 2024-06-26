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

        // Sự kiện thông báo khi trạng thái xác thực thay đổi
        public event Action OnChange;

        // Khởi tạo AuthService với các phụ thuộc
        public AuthService(IJSRuntime jsRuntime, NavigationManager navigationManager, IServiceScopeFactory scopeFactory)
        {
            _jsRuntime = jsRuntime;
            _navigationManager = navigationManager;
            _scopeFactory = scopeFactory;
        }

        // Lấy trạng thái xác thực hiện tại
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Lấy thông tin người dùng từ sessionStorage
            var user = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "user");
            // Tạo danh tính người dùng từ thông tin JWT
            var identity = string.IsNullOrEmpty(user) ? new ClaimsIdentity() : new ClaimsIdentity(ParseClaimsFromJwt(user), "jwt");

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        //---------------------------// Đăng nhập người dùng-------------------------------------------------------------------------------------------
        public async Task Login(UserLoginModel user)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Kiểm tra người dùng trong cơ sở dữ liệu
                var dbUser = await dbContext.NguoiDungs
                    .FirstOrDefaultAsync(u => u.Email == user.Username && u.MatKhau == user.Password);

                if (dbUser != null)
                {
                    // Lưu thông tin người dùng hiện tại
                    _currentUser = dbUser;
                    // Tạo token JWT
                    var token = GenerateJwtToken(dbUser);
                    // Lưu token vào sessionStorage
                    await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "user", token);
                    // Thông báo thay đổi trạng thái xác thực
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                    OnChange?.Invoke();
                    // Điều hướng về trang chủ
                    _navigationManager.NavigateTo("/");
                }
            }
        }

        // Đăng xuất người dùng
        public async Task Logout()
        {
            _currentUser = null;
            // Xóa token từ sessionStorage
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "user");
            // Thông báo thay đổi trạng thái xác thực
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            OnChange?.Invoke();
            // Điều hướng về trang đăng nhập
            _navigationManager.NavigateTo("/login");
        }

        // Giả lập tạo token JWT từ thông tin người dùng
        private string GenerateJwtToken(NguoiDung user)
        {
            return $"{user.Email}.{user.HoTen}.{user.NguoiDungId}.{user.Cap}";
        }

        // Phân tích token JWT để lấy các claim
        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var parts = jwt.Split('.');
            return new List<Claim>
            {
                new Claim(ClaimTypes.Name, parts[0]),         // Email
                new Claim("FullName", parts[1]),              // Họ tên
                new Claim(ClaimTypes.NameIdentifier, parts[2]), // ID người dùng
                new Claim("Cap", parts[3])                    // Cấp độ người dùng
            };
        }

        // Kiểm tra xem người dùng có được xác thực không
        public bool IsAuthenticated()
        {
            return _currentUser != null;
        }

        // Lấy tên người dùng hiện tại
        public string GetUserName()
        {
            return _currentUser?.HoTen;
        }

        // Lấy ID người dùng hiện tại
        public int GetUserId()
        {
            return _currentUser?.NguoiDungId ?? 0;
        }

        // Lấy cấp độ người dùng hiện tại
        public int GetUserCap()
        {
            return _currentUser?.Cap ?? 3;
        }
    }
}