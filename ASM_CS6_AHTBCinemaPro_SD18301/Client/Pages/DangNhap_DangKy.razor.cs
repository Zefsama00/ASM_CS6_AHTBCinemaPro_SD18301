using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;
using static System.Net.WebRequestMethods;
using ASM_CS6_AHTBCinemaPro_SD18301.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using ASM_CS6_AHTBCinemaPro_SD18301.Client.Service;
using System.Collections.Generic;
using System.Net.Http;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Client.Pages
{
    public partial class DangNhap_DangKy
    {
        private KhachHang khachHang = new KhachHang();
        private string passwordConfirm;
        private ResponseMessage responseMessage;
        private LoginModel loginModel = new LoginModel();
        private string errorMessage;
        private bool isLoggedIn = false;
        private Timer timer;

     

        private async Task HandleValidSubmit()
        {
            if (khachHang.Password != passwordConfirm)
            {
                responseMessage = new ResponseMessage
                {
                    Success = false,
                    Message = "Mật khẩu xác nhận không khớp!"
                };
                ShowMessage();
                return;
            }

            try
            {
                var response = await HttpClient.PostAsJsonAsync("api/DangNhap/Register", khachHang);
                if (response.IsSuccessStatusCode)
                {
                    responseMessage = new ResponseMessage
                    {
                        Success = true,
                        Message = "Đăng ký thành công!"
                    };
                    // Reset form or redirect
                    khachHang = new KhachHang();
                    passwordConfirm = string.Empty;
                }
                else
                {
                    var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
                    responseMessage = new ResponseMessage
                    {
                        Success = false,
                        Message = errorResponse?.Message ?? "Đã xảy ra lỗi khi đăng ký."
                    };
                }
                ShowMessage();
            }
            catch (Exception ex)
            {
                responseMessage = new ResponseMessage
                {
                    Success = false,
                    Message = $"Lỗi: {ex.Message}"
                };
                ShowMessage();
            }
        }

        private void HandleInputUsername(ChangeEventArgs e)
        {
            loginModel.Username = e.Value.ToString();
        }

        private void HandleInputPassword(ChangeEventArgs e)
        {
            loginModel.Password = e.Value.ToString();
        }

        private async Task HandleLogin()
        {
            errorMessage = null;

            try
            {
                var response = await HttpClient.PostAsJsonAsync("api/DangNhap/LogIn", loginModel);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResult>();
                    var userClaims = ParseToken(result.Token);

                    var username = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                    var role = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                    if (username != null && role != null)
                    {
                        AuthenticationStateProvider.MarkUserAsAuthenticated(username, role);

                        await JSRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", result.Token);

                        isLoggedIn = true;

                        if (role == "Admin")
                        {
                            Navigation.NavigateTo("/Admin/Index", true);
                        }
                        else
                        {
                            Navigation.NavigateTo("/", true);
                        }
                    }
                }
                else
                {
                    errorMessage = "Tài khoản hoặc mật khẩu không chính xác.";
                    ShowMessage();
                }
            }
            catch (Exception ex)
            {
                errorMessage = $"Đã xảy ra lỗi: {ex.Message}";
                ShowMessage();
            }
        }

        private async Task HandleLogout()
        {
            await JSRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
            AuthenticationStateProvider.MarkUserAsLoggedOut();
            isLoggedIn = false;
            Navigation.NavigateTo("/login", true);
        }

        private void ShowMessage()
        {
            timer = new Timer(HideMessage, null, 3000, Timeout.Infinite);
        }

        private void HideMessage(object state)
        {
            responseMessage = null;
            errorMessage = null;
            InvokeAsync(StateHasChanged);
        }

        private IEnumerable<Claim> ParseToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            return jwtToken.Claims;
        }

        public class ResponseMessage
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }

        public class ErrorResponse
        {
            public string Message { get; set; }
        }

        public class LoginResult
        {
            public string Token { get; set; }
        }
    }
}
