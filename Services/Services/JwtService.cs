using Common;
using Common.Settings;
using Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.IServices;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class JwtService :  IJwtService, IAsMarkScopeDependency
    {
        public SignInManager<User> SignInManager { get; }
        private readonly SiteSettings _SiteSettings;
        public JwtService(IOptionsSnapshot<SiteSettings> options,SignInManager<User> signInManager)
        {
            _SiteSettings = options.Value;
            SignInManager = signInManager;
        }

        

        /// <summary>
        /// تولید توکن Jwt
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<AccessToken> GenerateAsync(User user)
        {
            var secretkey = Encoding.UTF8.GetBytes(_SiteSettings.JwtSettings.SecretKey); //this var must be longer than 16 character
            var signinCredential = new SigningCredentials(new SymmetricSecurityKey(secretkey), SecurityAlgorithms.HmacSha256Signature);

            //برای امنیت توکن 
            var encriptKey = Encoding.UTF8.GetBytes(_SiteSettings.JwtSettings.EncriptKey); //this var must be longer than 16 character
            var encriptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encriptKey), SecurityAlgorithms.Aes128KW,SecurityAlgorithms.Aes128CbcHmacSha256);


            var claims =await _GetClaimsAsync(user);
            var descrite = new SecurityTokenDescriptor()
            {
                //برای ایجاد امنیت توکن
                EncryptingCredentials = encriptingCredentials,
                //تولید کننده
                Issuer = _SiteSettings.JwtSettings.Issure,
                //مصرف کننده
                Audience = _SiteSettings.JwtSettings.Audience,
                //زمان تولید توکن
                IssuedAt = DateTime.Now,
                //زمان انقضا توکن
                Expires = DateTime.Now.AddMinutes(_SiteSettings.JwtSettings.ExpirationMinutes),
                // از چه زمانی به بعد می توان از توکن استفاده کرد
                NotBefore = DateTime.Now.AddMinutes(_SiteSettings.JwtSettings.NotBeforMinutes),
                SigningCredentials = signinCredential,
                //اطلاعات اضافی در توکن
                Subject = new ClaimsIdentity(claims)

            };

            //از مپ کردن کلیم های دانتی و جی دبلو تی به همدیگر جلوگیری میکند
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            //JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
            //JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateJwtSecurityToken(descrite);
            //var Jwt = tokenHandler.WriteToken(securityToken);
            return new AccessToken(securityToken);
        }
        private async Task<List<Claim>> _GetClaimsAsync(User user)
        {
            //JwtRegisteredClaimNames.NameId = user.Id,
            //JwtRegisteredClaimNames.UniqueName = user.UserName
            var claims =await SignInManager.ClaimsFactory.CreateAsync(user);
            var list = new List<Claim>(claims.Claims);
            list.Add(new Claim(ClaimTypes.MobilePhone,"09127420118"));
            return list;




            //var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;
            //List<Claim> claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name,user.UserName),
            //    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            //    new Claim(ClaimTypes.MobilePhone,"09124720118"),
            //    new Claim(securityStampClaimType,user.SecurityStamp.ToString())
            //};

            //List<Role> roles = new List<Role>() { new Role() { Name = "Admin" }, new Role() { Name = "SupperAdmin" } };
            //foreach (var item in roles)
            //    claims.Add(new Claim(ClaimTypes.Role, item.Name));

           // return claims;

        }
    }
}
