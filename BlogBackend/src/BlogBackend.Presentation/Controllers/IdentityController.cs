using BlogBackend.Core.Dtos.Models;
using BlogBackend.Core.User.Models;
using BlogBackend.Presentation.Verification.Base;
using FluentValidation;
using MediatR;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.IdentityModel.Tokens.Jwt;
using BlogBackend.Core.RefreshToken.Entity;

namespace BlogBackend.Presentation.Controllers;

[ApiController]
public class IdentityController : Controller
{
    private readonly SignInManager<User> signInManager;
    private readonly UserManager<User> userManager;
    private readonly IDataProtector dataProtector;
    private readonly IValidator<RegistrationDto> userValidator;
    private readonly IValidator<LoginDto> userLoginValidator;
    private readonly IEmailService emailService;
    private readonly ISender sender;

    public IdentityController(ISender sender, 
        IValidator<LoginDto> userLoginValidator, 
        SignInManager<User> signInManager, 
        UserManager<User> userManager, 
        IValidator<RegistrationDto> userValidator, 
        IDataProtectionProvider dataProtectionProvider, 
        IEmailService emailService)
    {
        this.sender = sender;
        this.userLoginValidator = userLoginValidator;
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.dataProtector = dataProtectionProvider.CreateProtector("identity");
        this.userValidator = userValidator;
        this.emailService = emailService;
    }

    // [HttpGet]
    // [Route("/[controller]/[action]", Name = "LoginView")]
    // public IActionResult Login(string? ReturnUrl)
    // {
    //     var errorMessage = TempData["error"];
    //     ViewBag.ReturnUrl = ReturnUrl;

    //     if (errorMessage != null)
    //     {
    //         ModelState.AddModelError("All", errorMessage.ToString()!);
    //     }

    //     return View();
    // }

    // [HttpGet]
    // [Route("/[controller]/[action]", Name = "RegistrationView")]
    // public IActionResult Registration()
    // {
    //     if (TempData["error"] != null)
    //     {
    //         ModelState.AddModelError("All", "This Email already registered");
    //     }

    //     return View();
    // }

    [HttpPost]
    [Route("/api/[controller]/[action]")]
    [ActionName("Login")]
    public async Task<IActionResult> SignIn([FromForm] LoginDto loginDto)
    {
        try
        {
            var validationResult = userLoginValidator.Validate(loginDto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByEmailAsync(loginDto.Email!);

            if (user == null)
            {
                return BadRequest("Incorrect email!");
            }

            var tokenData = $"{loginDto.Email}:{loginDto.Name}";
            var token = dataProtector.Protect(tokenData);
            var confirmationLink = Url.Action("ConfirmLogin", "Identity", new { token }, Request.Scheme);
            var message = $"Please confirm your login by clicking on the link: {HtmlEncoder.Default.Encode(confirmationLink!)}";

            await emailService.SendEmailAsync(loginDto.Email!, "Confirm your login", message);
            TempData["Email"] = loginDto.Email;
            
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("/[controller]/[action]", Name = "ConfirmLogin")]
    public async Task<IActionResult> ConfirmLogin(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Invalid token");
        }

        var tokenData = dataProtector.Unprotect(token);
        var dataParts = tokenData.Split(':');
        if (dataParts.Length != 2)
        {
            return BadRequest("Invalid token data");
        }

        var email = dataParts[0];
        var name = dataParts[1];

        var foundUser = await userManager.FindByEmailAsync(email);
        if (foundUser is null)
        {
            return BadRequest("User not found");
        }

        var signInResult = await signInManager.SignInAsync(foundUser, isPersistent: true);

        if(signInResult.Succeeded == false) {
            return base.BadRequest("Incorrect Login or Password");
        }

        var roles = await userManager.GetRolesAsync(foundUser);

        var claims = roles
            .Select(roleStr => new Claim(ClaimTypes.Role, roleStr))
            .Append(new Claim(ClaimTypes.NameIdentifier, foundUser.Id))
            .Append(new Claim(ClaimTypes.Email, foundUser.Email ?? "not set"))
            .Append(new Claim(ClaimTypes.Name, foundUser.UserName ?? "not set"));

        var signingKey = new SymmetricSecurityKey(jwtOptions.KeyInBytes);
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims: claims,
            //notBefore: DateTime.Now.AddMinutes(2),
            expires: DateTime.Now.AddMinutes(jwtOptions.LifeTimeInMinutes),
            signingCredentials: signingCredentials
        );

        var handler = new JwtSecurityTokenHandler();
        var tokenStr = handler.WriteToken(token);

        // create refresh token
        var refreshToken = new RefreshToken {
            UserId = foundUser.Id,
            Token = Guid.NewGuid(),
        };

        await dbContext.RefreshTokens.AddAsync(refreshToken);
        await dbContext.SaveChangesAsync();

        return Ok(new {
            refresh = refreshToken.Token,
            access = tokenStr,
        });
    }

    

    [HttpPost]
    [Route("/api/[controller]/[action]", Name = "RegistrationEndpoint")]
    [ActionName("Registration")]
    public async Task<IActionResult> SignUp([FromForm] RegistrationDto registrationDto, [FromForm] int[] selectedTopicIds)
    {
        try
        {
            var validationResult = userValidator.Validate(registrationDto);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View();
            }

            var tokenData = $"{registrationDto.Email}:{registrationDto.Name}";
            var token = dataProtector.Protect(tokenData);
            var confirmationLink = Url.Action("ConfirmEmail", "Identity", new { token, selectedTopicIds }, Request.Scheme);
            var message = $"Please confirm your registration by clicking on the link: {HtmlEncoder.Default.Encode(confirmationLink)}";

            await emailService.SendEmailAsync(registrationDto.Email, "Confirm your email", message);
            TempData["Email"] = registrationDto.Email;
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
            return RedirectToRoute("RegistrationView");
        }

        return RedirectToRoute("ConfirmationView");
    }

    [HttpGet]
    [Route("/[controller]/[action]", Name = "ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest("Invalid token");
        }

        var tokenData = dataProtector.Unprotect(token);
        var dataParts = tokenData.Split(':');
        if (dataParts.Length != 2)
        {
            return BadRequest("Invalid token data");
        }

        var email = dataParts[0];
        var name = dataParts[1];

        var user = new User
        {
            Email = email,
            UserName = name,
            AvatarUrl = "Assets/UserAvatar/DefaultAvatar.png"
        };

        var result = await userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            return BadRequest(string.Join("\n", result.Errors.Select(error => error.Description)));
        }

        await signInManager.SignInAsync(user, isPersistent: true);

        return RedirectToAction("ChooseTags", "Topic");
    }

    // [HttpGet]
    // [Route("[controller]/[action]", Name = "ConfirmationView")]
    // public IActionResult Confirmation()
    // {
    //     string email = TempData["Email"] as string;

    //     var model = new RegistrationDto { Email = email };
    //     return View(model);
    // }
}




