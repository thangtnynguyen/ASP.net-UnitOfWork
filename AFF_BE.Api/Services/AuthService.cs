using AFF_BE.Api.Services.Interfaces;
using AFF_BE.Core.Configs;
using AFF_BE.Core.Constants;
using AFF_BE.Core.Data.Identity;
using AFF_BE.Core.Models.Auth;
using AFF_BE.Core.Models.Common;
using AFF_BE.Core.Models.Mail;
using AFF_BE.Data;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Management;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AFF_BE.Api.Services
{
    public class AuthService:IAuthService
    {
        private readonly AffContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(AffContext dbContext, IConfiguration config, UserService userService, UserManager<User> userManager, IMailService mailService, IMapper mapper, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _config = config;
            _userService = userService;
            _userManager = userManager;
            _mailService = mailService;
            _mapper = mapper;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }
        

        public async Task<string> CreateToken(User user)
        {
            var key = _config["JwtTokenSettings:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Không thể tải cấu hình Key Jwt!");
            }

            var minuteValidToken = _config["JwtTokenSettings:TokenValidityInMinutes"];

            if (string.IsNullOrEmpty(minuteValidToken))
            {
                throw new ArgumentNullException(nameof(minuteValidToken), "Không thể tải cấu hình TokenValidityInMinutes Jwt!");
            }

            var issuer = _config["JwtTokenSettings:Issuer"];

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Không thể tải cấu hình Issuer Jwt!");
            }

            var audience = _config["JwtTokenSettings:Audience"];

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Không thể tải cấu hình Audience Jwt!");
            }

            var permissions = await _userService.GetPermissionByUserAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypeConstant.Id, user.Id.ToString()),
                new Claim(ClaimTypeConstant.UserName, user.Name),
                new Claim(ClaimTypeConstant.Email, user.Email),

            };

            foreach (var permission in permissions)
            {
                claims.Add(new Claim(ClaimTypeConstant.Permission, permission));
            }

            var subject = new ClaimsIdentity(claims);
            var creds = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.Now.AddDays(30);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = expires,
                SigningCredentials = creds,
                Issuer = issuer,
                Audience = audience
            };



            var tokenHandler = new JwtSecurityTokenHandler();

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);

            return token;
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private async Task<bool> RevokeRefreshToken(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new BadHttpRequestException("Tên người dùng không tồn tại trong hệ thống!");
            }

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            return true;
        }

        public async Task<bool> SendOtpMail(string toMail, string subject, string body)
        {

            var mail = new SendMailRequest
            {
                ToEmail = toMail,
                Subject = subject,
                Body = body,
            };
            await _mailService.SendMail(mail);

            return true;
        }

        public async Task<LoginResult> RefreshToken(string refreshToken)
        {
            var authorizationHeader = _httpContextAccessor?.HttpContext?.Request.Headers[HeaderRequest.Authorization].ToString();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                throw new BadHttpRequestException("AccessToken không tồn tại trong yêu cầu!");
            }

            var token = authorizationHeader.Split(' ').LastOrDefault();

            if (string.IsNullOrEmpty(token))
            {
                throw new BadHttpRequestException("AccessToken không hợp lệ!");
            }

            var principal = GetPrincipalFromExpiredToken(token);
            string username = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypeConstant.Email).Value;


            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new BadHttpRequestException("Token chứa thông tin người dùng không tồn tại trong hệ thống!");
            }

            #region ver-pro
            //if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            //{
            //    throw new BadHttpRequestException("RefreshToken không hợp lệ hoặc đã hết hạn!");
            //}

            //var userClaims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim(ClaimType.UserName, user.UserName), 
            //};

            //var roles = await _userManager.GetRolesAsync(user);
            //foreach (var role in roles)
            //{
            //    userClaims.Add(new Claim(ClaimTypes.Role, role));
            //}
            //Create token

            //var newAccessToken = await this.CreateToken(user);

            //var newAccessToken = this.CreateToken(userClaims);

            //var newRefreshToken = this.CreateRefreshToken();

            //user.RefreshToken = newRefreshToken;

            //user.RefreshTokenExpiryTime = DateTime.Now.AddDays(30);
            //user.IsRefreshToken = false;

            //await _userManager.UpdateAsync(user);

            ////var tokenString = new JwtSecurityTokenHandler().WriteToken(newAccessToken);

            //var newAccessToken = await this.CreateToken(user);

            //var loginResult = new LoginResult()
            //{
            //    AccessToken = newAccessToken,
            //    //RefreshToken = newRefreshToken,
            //    RefreshToken = user.RefreshToken,
            //    //Expiration = newAccessToken.ValidTo
            //    Expiration = DateTime.Now.AddDays(30)

            //};

            //var tokenString = new JwtSecurityTokenHandler().WriteToken(newAccessToken);

            #endregion


            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(30);
            user.IsRefreshToken = false;

            await _userManager.UpdateAsync(user);

            var newAccessToken = await this.CreateToken(user);

            var loginResult = new LoginResult()
            {
                AccessToken = newAccessToken,
                RefreshToken = user.RefreshToken,
                Expiration = DateTime.Now.AddDays(30)

            };

            return loginResult;
        }
        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var key = _config["Jwt:Key"];

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Không thể tải cấu hình Key Jwt!");
            }

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Token không hợp lệ!");
            }

            return principal;

        }

        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();

            User user = await GetUserCurrentAsync();

            await this.RevokeRefreshToken(user.UserName);

            return true;
        }
        private async Task<User> GetUserCurrentAsync()
        {
            var username = _httpContextAccessor?.HttpContext?.User.FindFirst(x => x.Type == ClaimTypeConstant.Email)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                throw new UnauthorizedAccessException("Người dùng chưa đăng nhập hoặc phiên làm việc đã hết hạn.");
            }

            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                throw new BadHttpRequestException("Người dùng không tồn tại trong hệ thống!");
            }

            return user;
        }

        public Task<string> VerifyOtpLoginEmail(VerifyOtpLoginEmailRequest request)
        {
            throw new NotImplementedException();
        }

        public JwtSecurityToken CreateToken(List<Claim> claims)
        {
            throw new NotImplementedException();
        }

        Task<bool> IAuthService.RevokeRefreshToken(string username)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResult> RefreshTokenNoTranRole(string refreshToken)
        {
            throw new NotImplementedException();
        }

    }
}