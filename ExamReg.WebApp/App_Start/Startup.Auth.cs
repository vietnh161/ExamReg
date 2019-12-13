using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ExamReg.Data;
using ExamReg.Model.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Owin;


[assembly: OwinStartup(typeof(ExamReg.WebApp.App_Start.Startup))]

namespace ExamReg.WebApp.App_Start
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(ExamRegDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

      // Enable the application to use a cookie to store information for the signed in user
      // and to use a cookie to temporarily store information about a user logging in with a third party login provider
      // Configure the sign in token base
      app.CreatePerOwinContext<UserManager<ApplicationUser>>(CreateManager);

      app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
      {
        TokenEndpointPath = new PathString("/oauth/token"),
        Provider = new AuthorizationServerProvider(),
        AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(360),
        AllowInsecureHttp = true,

      });
      app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());



      // Configure the sign in cookie
      //app.UseCookieAuthentication(new CookieAuthenticationOptions
      //{
      //  AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
      //  LoginPath = new PathString("/Account/Login"),
      //  Provider = new CookieAuthenticationProvider
      //  {
      //    // Enables the application to validate the security stamp when the user logs in.
      //    // This is a security feature which is used when you change a password or add an external login to your account.
      //    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
      //            validateInterval: TimeSpan.FromMinutes(30),
      //            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
      //  }
      //});
      //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});

        }
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
      public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
      {

        //UserManager<ApplicationUser> userManager = context.OwinContext.GetUserManager<UserManager<ApplicationUser>>();
        //ApplicationUser user;
        //try
        //{
        //  user = await userManager.FindAsync(context.UserName, context.Password);
        //}



        context.Validated();
      }
      public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
      {
        var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");

        if (allowedOrigin == null) allowedOrigin = "*";

        context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

        UserManager<ApplicationUser> userManager = context.OwinContext.GetUserManager<UserManager<ApplicationUser>>();
        ApplicationUser user;
        try
        {
          user = await userManager.FindAsync(context.UserName, context.Password);
        }
        catch
        {
          // Could not retrieve the user due to error.
          context.SetError("server_error");
          context.Rejected();
          return;
        }
        if (user != null)
        {


          var role = userManager.GetRoles<ApplicationUser, string>(user.Id);

          ClaimsIdentity identity = await userManager.CreateIdentityAsync(
                                                 user,
                                                 DefaultAuthenticationTypes.ExternalBearer);
        //  var identity = new ClaimsIdentity(context.Options.AuthenticationType);

          var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                  {"fullname" , user.FullName },
                  {"role", role[0] }
                });

          var ticket = new AuthenticationTicket(identity, props);
          context.Validated(ticket);

       //   context.Validated(identity);

        }
        else
        {
          context.SetError("invalid_grant", "Tài khoản hoặc mật khẩu không đúng.'");
          context.Rejected();
        }
      }
      public override Task TokenEndpoint(OAuthTokenEndpointContext context)
      {
        foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
        {
          context.AdditionalResponseParameters.Add(property.Key, property.Value);
        }

        return Task.FromResult<object>(null);
      }
    }
    


    private static UserManager<ApplicationUser> CreateManager(IdentityFactoryOptions<UserManager<ApplicationUser>> options, IOwinContext context)
    {
      var userStore = new UserStore<ApplicationUser>(context.Get<ExamRegDbContext>());
      var owinManager = new UserManager<ApplicationUser>(userStore);
      return owinManager;
    }
  }
}



