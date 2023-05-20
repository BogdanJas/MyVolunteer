﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyVolunteer_API.Helper;
using MyVolunteer_Common;
using MyVolunteer_Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyVolunteer_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController:Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly APISettings _aPISettings;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IOptions<APISettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _aPISettings = options.Value;
        }
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDTO signUpRequestDto)
        {
            if (signUpRequestDto == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = new IdentityUser
            {
                UserName = signUpRequestDto.Email,
                Email = signUpRequestDto.Email,
                PhoneNumber = signUpRequestDto.PhoneNumber,
                EmailConfirmed = true
            };
            var result = await _userManager.CreateAsync(user, signUpRequestDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO()
                {
                    IsRegisterationSuccessful = false,
                    Errors = result.Errors.Select(u => u.Description)
                });
            }

            var roleResult = await _userManager.AddToRoleAsync(user, SD.Role_Customer);
            if (!roleResult.Succeeded)
            {
                return BadRequest(new SignUpResponseDTO()
                {
                    IsRegisterationSuccessful = false,
                    Errors = result.Errors.Select(u => u.Description)
                });
            }

            return StatusCode(201);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDTO signInRequestDTO)
        {
            if (signInRequestDTO == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _signInManager.PasswordSignInAsync(signInRequestDTO.UserName, signInRequestDTO.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(signInRequestDTO.UserName);
                if (user == null)
                {
                    return Unauthorized(new SignInResponseDTO
                    {
                        IsAuthSuccessful = false,
                        ErrorMessage = "Invalid Authentication"
                    });
                }

                var signinCredentials = GetSigningCredentials();
                var claims = await GetClaims(user);

                var tokenOptions = new JwtSecurityToken(
                    issuer: _aPISettings.ValidIssuer,
                    audience: _aPISettings.ValidAudience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(30),
                    signingCredentials: signinCredentials);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                return Ok(new SignInResponseDTO()
                {
                    IsAuthSuccessful = true,
                    Token = token,
                    VolunteerDTO = new VolunteerDTO()
                    {
                        SignInId = user.Id,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber
                    }
                });

            }
            else
            {
                return Unauthorized(new SignInResponseDTO
                {
                    IsAuthSuccessful = false,
                    ErrorMessage = "Invalid Authentication"
                });
            }
            return StatusCode(201);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_aPISettings.SecretKey));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim("Id",user.Id)
            };

            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(user.Email));
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
