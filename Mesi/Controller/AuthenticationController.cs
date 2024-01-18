
using Mesi.Helpers;
using Mesi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mesi.Controller;

public class AuthenticationController : ControllerBase
{
    private readonly JwtService jwtService;
    private readonly ModelDbContext _dbContext;

    public AuthenticationController(JwtService jwtService, ModelDbContext modelDbContext)
    {
        this.jwtService = jwtService;
        this._dbContext = modelDbContext;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Validate user credentials
        if (IsValidUser(request.Name, request.Role))
        {
            var random = new Random();
            // Generate JWT token with user roles
            var token = jwtService.GenerateJwt(userId: request.Name.GetHashCode().ToString(), username: request.Name, role: request.Role);

            // Return the token to the client
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }

    private bool IsValidUser(string username, string role)
    {
        if(role == "Doctor")
            return _dbContext.Doctors.Any(x => x.Name == username);
        else
            return _dbContext.Patients.Any(x => x.Name == username);
    }
}