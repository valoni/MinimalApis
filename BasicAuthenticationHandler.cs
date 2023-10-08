using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;

namespace WebApplication4
{

    /* basic authentication */
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock
            ) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authHeader = Request.Headers["Authorization"].ToString();
            if (authHeader != null && authHeader.StartsWith("basic", StringComparison.OrdinalIgnoreCase))
            {
                var applicants = authHeader.Substring("Basic ".Length).Trim();

                System.Console.WriteLine(applicants);
                var credentialstring = Encoding.UTF8.GetString(Convert.FromBase64String(applicants));

                var credentials = credentialstring.Split(':');

                string uname = "";
                string upass = "";

                uname = credentials[0];
                upass = credentials[1];


                Console.WriteLine("Uname =" + uname);

                Console.WriteLine("upass =" + upass);
              
                if (uname == "MobileKamarieri" && upass == "ca6fd90c8a1c324872e90640f459ef9e")
                {
                    var claims = new[] { new Claim("name", uname), new Claim(ClaimTypes.Role, "Admin") };
                    var identity = new ClaimsIdentity(claims, "Basic");

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }

                if (uname == "bitMobile" && upass == "ca6fd90c8a1c324872e90640f459ef9e")
                {
                    var claims = new[] { new Claim("name", uname), new Claim(ClaimTypes.Role, "Admin") };
                    var identity = new ClaimsIdentity(claims, "Basic");

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }


                if (uname == "BitMobile" && upass == "ca6fd90c8a1c324872e90640f459ef9e")
                {
                    var claims = new[] { new Claim("name", uname), new Claim(ClaimTypes.Role, "Admin") };
                    var identity = new ClaimsIdentity(claims, "Basic");

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }


                if (uname == "bitMobile" && upass == "80f3eadc8d56e42a7715ab9718c09ece")
                {
                    var claims = new[] { new Claim("name", uname), new Claim(ClaimTypes.Role, "Admin") };
                    var identity = new ClaimsIdentity(claims, "Basic");

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }


                if (uname == "BitMobile" && upass == "80f3eadc8d56e42a7715ab9718c09ece")
                {
                    var claims = new[] { new Claim("name", uname), new Claim(ClaimTypes.Role, "Admin") };
                    var identity = new ClaimsIdentity(claims, "Basic");

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }


                if (uname == "mobileKamarieri" && upass == "80f3eadc8d56e42a7715ab9718c09ece")
                {
                    var claims = new[] { new Claim("name", uname), new Claim(ClaimTypes.Role, "Admin") };
                    var identity = new ClaimsIdentity(claims, "Basic");

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
                }


                if (DataAccess.CheckForIt(uname, upass))
                {
                    var claims = new[] { new Claim("name", uname), new Claim(ClaimTypes.Role, "Admin") };
                    var identity = new ClaimsIdentity(claims, "Basic");

                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));

                }



                Response.StatusCode = 401;
                Response.Headers.Add("WWW-Authenticate", "Basic realm=\"bit-ks.com\"");
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }
            else
            {
                Response.StatusCode = 401;
                Response.Headers.Add("WWW-Authenticate", "Basic realm=\"bit-ks.com\"");
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header"));
            }
        }
    }
}
