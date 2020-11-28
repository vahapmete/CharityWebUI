using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using CharityWebUI.DataAccess.IMainRepository;
using CharityWebUI.Models.DbModels;
using CharityWebUI.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using IEmailSender = Myvas.AspNetCore.Email.IEmailSender;

namespace CharityWebUI.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _uow;


        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork uow,
            Myvas.AspNetCore.Email.IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _uow = uow;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Name and Surname")]
            public string NameSurname { get; set; }
            public int? CharityId { get; set; }
            public string Role { get; set; }
            public IEnumerable<SelectListItem> CharityList { get; set; }
            public IEnumerable<SelectListItem> RoleList { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            Input = new InputModel()
            {
                CharityList = _uow.Charity.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                RoleList =_roleManager.Roles.Where(r => r.Name != ProjectConstant.Role_User_Guest)
                    .Select(x => x.Name).Select((i => new SelectListItem
                    {
                        Text = i,
                        Value = i
                    }))
            };
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                //var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    CharityId = Input.CharityId,
                    Role = Input.Role,
                    NameSurname = Input.NameSurname,
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    if (!await _roleManager.RoleExistsAsync(ProjectConstant.Role_User_GeneralAdmin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(ProjectConstant.Role_User_GeneralAdmin));
                    }
                    if (!await _roleManager.RoleExistsAsync(ProjectConstant.Role_User_CharityAdmin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(ProjectConstant.Role_User_CharityAdmin));
                    }
                    if (!await _roleManager.RoleExistsAsync(ProjectConstant.Role_User_Member))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(ProjectConstant.Role_User_Member));
                    }
                    if (!await _roleManager.RoleExistsAsync(ProjectConstant.Role_User_Member))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(ProjectConstant.Role_User_Member));
                    }

                    //await _userManager.AddToRoleAsync(user, ProjectConstant.Role_User_GeneralAdmin);

                    if (user.Role==null)
                    {
                        await _userManager.AddToRoleAsync(user, ProjectConstant.Role_User_Guest);
                    }
                    else
                    {
                        if (user.CharityId>0)
                        {
                            await _userManager.AddToRoleAsync(user, ProjectConstant.Role_User_CharityAdmin);
                        }
                        await _userManager.AddToRoleAsync(user, user.Role);
                    }

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        if (user.Role==null)
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "User", new {Area = "GeneralAdmin"});
                        }
                       
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
