using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Shareposts.Utils;
using Shareposts.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Shareposts.Core.Services;

namespace Shareposts.WebUI.Utils;

public static class ControllerUtils
{
    public const string pageSignInErrorMessage =
        "You must be logged in to access this page";
    public const string pageAlreadySignedInErrorMessage =
        "You are already signed in and cannot access this page";
    public const string actionSignInErrorMessage =
        "You must be logged in to access this functionality";
    public const string actionAlreadySignedInErrorMessage =
        "You are already signed in and cannot access this functionality";
    public const string invalidAuthTokenErrorMessage =
        "Your authentication token is either invalid or expired";

    public static async Task<(bool, string?, string?, string?)> IsAuthenticatedWithTokenOrRedirect(
        HttpRequest request, ITempDataDictionary tempData)
    {
        var token = request.Cookies["authToken"];

        if (string.IsNullOrWhiteSpace(token)) {
            tempData["errorMessage"] = ControllerUtils.actionSignInErrorMessage;
            return (false, "SignInUserPage", "Pages", null);
        }

        var jwtSecret = EnvUtils.GetJwtSecret();
        var jwtService = new JwtService(jwtSecret);
        var (isValidJwt, userId) = await jwtService.ValidateToken(token);

        if (! isValidJwt) {
            tempData["errorMessage"] = ControllerUtils.invalidAuthTokenErrorMessage;
            return (false, "SignOutUser", "Users", null);
        }

        return (true, null, null, userId);
    }

    public static (bool, string?, string?) IsAuthenticatedOrRedirect(
        HttpRequest request, ITempDataDictionary tempData)
    {
        var token = request.Cookies["authToken"];
        if (string.IsNullOrWhiteSpace(token)) {
            tempData["errorMessage"] = ControllerUtils.actionSignInErrorMessage;
            return (false, "SignInUserPage", "Pages");
        }
        return (true, null, null);
    }

    public static (bool, string?, string?) IsNotAuthenticatedOrRedirect(
        HttpRequest request, ITempDataDictionary tempData)
    {
        var token = request.Cookies["authToken"];
        if (! string.IsNullOrWhiteSpace(token)) {
            tempData["errorMessage"] = ControllerUtils.actionAlreadySignedInErrorMessage;
            return (false, "HomePage", "Pages");
        }
        return (true, null, null);
    }

    public static async Task<string?> GetAuthUserIdFromRequest(HttpRequest request, IJwtService jwtService)
    {
        var token = request.Cookies["authToken"];
        if (token == null) return null;
        return await jwtService.GetUserIdFromToken(token);
    }
}
