using AFF_BE.Api.Services;
using AFF_BE.Api.Services.Interfaces;
using AFF_BE.Core.Constants;
using AFF_BE.Core.Constants.Identity;
using AFF_BE.Core.Data.Commission;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Models.Auth;
using AFF_BE.Core.Models.Commission;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Identity.User;
using AFF_BE.Core.Models.Mail;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.WebRequestMethods;

namespace AFF_BE.Api.Controllers.Identity
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, IAuthService authService, IUserService userService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _authService = authService;
            _userService = userService;
            _mapper= mapper;
        }

        [HttpPost]
        [Route("login-by-email")]
        public async Task<ApiResult<LoginResult>> LoginByEmail([FromBody] LoginEmailRequest request)
        {
            //Check exists user
            var user = _userManager.Users.FirstOrDefault(user => user.Email == request.Email && user.EmailConfirmed == true);

            if (user == null)
            {
                return new ApiResult<LoginResult>()
                {
                    Status = false,
                    Message = "Email người dùng không tồn tại!",
                    Data = null
                };
            }

            //Verify login
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);

            if (!result.Succeeded)
            {
                return new ApiResult<LoginResult>()
                {
                    Status = false,
                    Message = "Tài khoản hoặc mật khẩu người dùng không chính xác!",
                    Data = null
                };
            }

            //Create token
            var token = await _authService.CreateToken(user);
            var refreshToken = _authService.CreateRefreshToken();

            var refreshTokenValidityInDays = _config["JwtTokenSettings:RefreshTokenValidityInDays"];

            if (string.IsNullOrEmpty(refreshTokenValidityInDays))
            {
                throw new ArgumentNullException(nameof(refreshTokenValidityInDays), "Không thể tải cấu hình RefreshTokenValidityInDays Jwt!");
            }

            var refreshTokenExpiryTime = DateTime.Now.AddDays(int.Parse(refreshTokenValidityInDays));

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;

            await _userManager.UpdateAsync(user);


            var loginResult = new LoginResult()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                Expiration = DateTime.Now.AddDays(30)
            };


            return new ApiResult<LoginResult>()
            {
                Status = true,
                Message = "Đăng nhập thành công!",
                Data = loginResult
            };

           
        }


        [HttpPost("register-verify-by-email")]
        public async Task<ApiResult<bool>> RegisterVerifyByEmail([FromBody] CreateUserRequest request)
        {
            var phoneNumber = request?.PhoneNumber?.Trim();
            var email = request?.Email?.Trim();

            var user = await _userManager.Users
                          .FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber || u.Email == email);

            if (user != null && user.EmailConfirmed)
            {
                return new ApiResult<bool>
                {
                    Status = false,
                    Message = user.PhoneNumber == phoneNumber ?
                              "Số điện thoại đã tồn tại trong hệ thống" : "Email đã tồn tại trong hệ thống",
                    Data = false
                };
            }

            if (user != null)
            {
                _mapper.Map(request, user); 
                user.EmailConfirmed = false;
                user.IsDeleted = false;
                user.Status = false;
                await _userManager.UpdateAsync(user);

            }
            else
            {
                user = _mapper.Map<CreateUserRequest, User>(request);
                user.UserName = phoneNumber;
                user.AvatarUrl = ImageConstant.Avatar;
                user.IsDeleted = false;
                user.EmailConfirmed = false;
                user.LockoutEnabled = false;
                user.Status = false;
                user.DirectCommission = new DirectCommission()
                {
                    Amount = 0,
                };
                var createResult = await _userManager.CreateAsync(user, request.Password);
                if (!createResult.Succeeded)
                {
                    return new ApiResult<bool>
                    {
                        Status = false,
                        Message = "Lỗi không xác định khi tạo người dùng",
                        Data = false
                    };
                }

                var roleResult = await _userManager.AddToRoleAsync(user, RoleConstant.Customer);
                if (!roleResult.Succeeded)
                {
                    return new ApiResult<bool>
                    {
                        Status = false,
                        Message = "Lỗi không xác định khi gán vai trò",
                        Data = false
                    };
                }
            }

            var otpCode = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user?.PhoneNumber);
            string subject = "AFF-HONIVY";
            string body = "Your verification code for AFF-HONIVY is " + otpCode;

            #region loading-longtime
            //var send = await _authService.SendOtpMail(user.Email, subject, body);

            //if (send == false)
            //{
            //    return new ApiResult<bool>
            //    {
            //        Status = false,
            //        Message = "Gửi thất bại",
            //        Data = false,
            //    };
            //}
            #endregion

            Task.Run(() => _authService.SendOtpMail(user.Email, subject, body));

            return new ApiResult<bool>
            {
                Status = true,
                Message = "Mã  đang được gửi đến mail của bạn",
                Data = true
            };

        }



        [HttpPost("confirm-email-otp-register")]
        public async Task<ApiResult<bool>> ConfirmEmailOtpRegister([FromBody] VerifyMailOtpRegisterRequest request)
        {
            var user = await _userManager.Users
                       .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return new ApiResult<bool>
                {
                    Status = false,
                    Message = "Không tìm thấy người dùng",
                    Data = false
                };
            }

            var otpValid = await _userManager.VerifyChangePhoneNumberTokenAsync(user, request.Otp, user.PhoneNumber);

            if (!otpValid)
            {

                return new ApiResult<bool>
                {
                    Status = false,
                    Message = "OTP không chính xác",
                    Data = false
                };
            }

            user.EmailConfirmed = true;
            user.Status = true;
            await _userManager.UpdateAsync(user);

            return new ApiResult<bool>
            {
                Status = true,
                Message = "Xác nhận OTP thành công",
                Data = true
            };
        }



        [HttpPost("send-email-otp")]
        public async Task<ApiResult<bool>> SendMailOtp([FromBody] SendMailRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.ToEmail.Trim());

            if (user == null)
            {
                return new ApiResult<bool>
                {
                    Status = false,
                    Message = "Tài khoản không tồn tại trong hệ thống",
                    Data = false
                };

            }

            var otpCode = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user?.PhoneNumber);


            string subject = "AFF-HONIVY";
            string body = "Your verification code for AFF-HONIVY is " + otpCode;

            //var FormatPhoneNumber = "+84" + user.PhoneNumber.Substring(1);

            var send = await _authService.SendOtpMail(user.Email, subject, body);

            if (send == false)
            {
                return new ApiResult<bool>
                {
                    Status = false,
                    Message = "Gửi thất bại",
                    Data = false,
                };
            }
            return new ApiResult<bool>
            {
                Status = true,
                Message = "Mã  đang được gửi đến mail của bạn",
                Data = true
            };
        }

        [HttpPost("refresh-token")]
        public async Task<ApiResult<LoginResult>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                var loginResult = await _authService.RefreshToken(request.RefreshToken);

                return new ApiResult<LoginResult>()
                {
                    Status = true,
                    Message = "làm mới thành công!",
                    Data = loginResult
                };
            }
            catch (BadHttpRequestException ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<ApiResult<bool>> Logout()
        {
            try
            {
                var isLogout = await _authService.Logout();

                return new ApiResult<bool>()
                {
                    Status = true,
                    Message = "Đăng xuất thành công!",
                    Data = isLogout
                };
            }
            catch (UnauthorizedAccessException ex)
            {
                throw new UnauthorizedAccessException(ex.Message);
            }
            catch (BadHttpRequestException ex)
            {
                throw new BadHttpRequestException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
