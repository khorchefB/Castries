using System.Security.Claims;
using System.Threading.Tasks;
using Duende.IdentityModel;
using IdentityServer.Models;
using IdentityServer.Pages.Account.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityServer.Pages.Register;
[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    public UserManager<ApplicationUser> _userManager { get; }
    public Index(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    [BindProperty]
    public RegisterViewModel Input {get;set;}
    [BindProperty]
    public bool RegisterSuccess {get;set;}

    public IActionResult OnGet(string returnUrl)
    {
        Console.WriteLine($"returnUrl == {returnUrl}");
        Input = new RegisterViewModel{
            ReturnUrl = returnUrl
        };

        return Page();
    }

    public async Task<IActionResult> OnPost(){
        if(Input.Button != "register") return Redirect("~/");
        if(ModelState.IsValid){
            var user= new ApplicationUser{
                UserName = Input.Username,
                Email = Input.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, Input.Password);

            if(result.Succeeded){
                await _userManager.AddClaimsAsync(user, new Claim[]{
                    new Claim(JwtClaimTypes.Name, Input.FullName)
                });
                RegisterSuccess = true;
            }else{
                Console.WriteLine($"_userManager.CreateAsync do not work {result.ToString()}");
            }
        }
        return Page();
    }
}