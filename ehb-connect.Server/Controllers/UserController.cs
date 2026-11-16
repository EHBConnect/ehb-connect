using ehb_connect.Server.DTOs;
using ehb_connect.Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ehb_connect.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // Get all active users

        [HttpGet]
        public async Task<IActionResult> GetAllUsers() 
        {
            var users = await _userManager.Users
                .Where(u => u.IsVisible)
                .Select(u => new
                {
                    u.Id,
                    u.FirstName, 
                    u.LastName,
                    u.Email,
                    u.Role,
                    u.Campus,
                    u.Programme,
                    u.Availability,
                    u.ProfileUpdatedAt
                })
                .ToListAsync();
            return Ok(users);
        }

        // List all active and inactive users admin

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsersForAdmin()
        {
            var users = await _userManager.Users
                .Select(u => new
                {
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Email,
                    u.Role,
                    u.Campus,
                    u.Programme,
                    u.IsVisible, 
                    u.ProfileUpdatedAt
                })
                .ToListAsync();

            return Ok(users);
        }

        // To get a specific profile by ID

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new {message = "User not found"});
            }

            return Ok(new
            {
                user.Id, 
                user.FirstName,
                user.LastName,
                user.Email, 
                user.Role,
                user.Campus,
                user.Programme,
                user.Availability,
                user.IsVisible,
                user.ProfileUpdatedAt
            });
        }

        // Update a user

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDto model)
        {
            var currentUserId = _userManager.GetUserId(User);
            if (currentUserId != id && !await IsAdminAsync())
            {
                return Forbid(); 
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Campus = model.Campus;
            user.Programme = model.Programme;
            user.Availability = model.Availability;
            user.IsVisible = model.IsVisible;
            user.ProfileUpdatedAt = DateTime.UtcNow;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Update user successfully" });
        }

        //Deactivate an account without deleting it
        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> DeactivateUser(string id)
        {
            if (!await IsAdminAsync())
            {
                return Forbid();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }
            user.IsVisible = false;
            user.ProfileUpdatedAt = DateTime.UtcNow;

            await _userManager.SetLockoutEnabledAsync(user, true);
            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "User deactivated successfully" });
        }

        [HttpPatch("{id}/activate")]
        public async Task<IActionResult> ActivateUser(string id)
        {
            if (!await IsAdminAsync())
            {
                return Forbid();
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            user.IsVisible = true;
            user.ProfileUpdatedAt = DateTime.UtcNow;

            await _userManager.SetLockoutEndDateAsync(user, null);

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "User activated successfully" });
        }

        private async Task<bool> IsAdminAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            return currentUser != null && currentUser.Role == "Admin";
        }
    }
}
