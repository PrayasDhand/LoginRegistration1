using LoginRegistration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoginRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly LoginContext loginContext;

        public UserController(IConfiguration configuration,LoginContext loginContext)
        {
            this.configuration = configuration;
            this.loginContext = loginContext;
        }

        [HttpGet]
        public IActionResult Get() {
            return Ok(loginContext.Users.ToList());
        }

        [HttpGet("id")]
        public IActionResult GetById(int id) { return Ok(loginContext.Users.FirstOrDefault(x => x.Id == id)); }

        [HttpPost]
        public IActionResult Post(User user) { loginContext.Users.Add(user); loginContext.SaveChanges(); return Ok("SUCCESSFUL"); }

        [HttpPut("id")]
        public IActionResult Put(int id, User user) { var use = loginContext.Users.FirstOrDefault(c => c.Id == user.Id);
        if (use == null) { return BadRequest(); }
         use.firstname = user.firstname;
            use.lastname = user.lastname;
            use.email = user.email;
            use.password = user.password;
            use.gender = user.gender;

            loginContext.Users.Update(use);
            loginContext.SaveChanges();

            return Ok("User Updated");
        }

        [HttpDelete("id")]
        public IActionResult DeleteById(int id) { var use = loginContext.Users.FirstOrDefault(c => c.Id==id); if (use == null) { return BadRequest(); } 
        loginContext.Users.Remove(use);
            loginContext.SaveChanges();
            return Ok("Employee Deleted");
        }
        [AllowAnonymous]
        [HttpPost("CreateUser")]
        public IActionResult Create(User user)
        {
            loginContext.Users.Add(user);
            loginContext.SaveChanges();
            return Ok("Successful");
        }
        [AllowAnonymous]
        [HttpGet("LoginUser")]
        public IActionResult Login(string email, string password)
        {
            var userAvailable = loginContext.Users.Where(u => u.email == email && u.password == password).FirstOrDefault();
            if (userAvailable != null) { return Ok(new JwtService(configuration).GenerateToken(
                userAvailable.Id.ToString(),
                userAvailable.firstname,
                userAvailable.lastname,
                userAvailable.email,
                userAvailable.gender
                )); }
            return Ok("Failure");
        }
    }
}
