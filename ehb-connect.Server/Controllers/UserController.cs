using ehb_connect.Server.DTO;
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

        // Get all visible users: public fields only, no e-mail (ASVS 15.3.1, R10, SAT 5)

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userManager.Users
                .Where(u => u.IsVisible)
                .Select(u => new PublicProfileDto(
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Campus,
                    u.Programme,
                    u.Availability,
                    u.ProfileUpdatedAt))
                .ToListAsync();
            return Ok(users);
        }

        // List all users, visible and hidden: admins only (ASVS 8.2.1, R04)

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsersForAdmin()
        {
            if (!await IsAdminAsync())
            {
                return Forbid();
            }

            var users = await _userManager.Users
                .Select(u => new FullProfileDto(
                    u.Id,
                    u.FirstName,
                    u.LastName,
                    u.Email,
                    u.Role,
                    u.Campus,
                    u.Programme,
                    u.Availability,
                    u.IsVisible,
                    u.ProfileUpdatedAt))
                .ToListAsync();

            return Ok(users);
        }

        // Get one profile by ID.
        // Owner or admin: full profile. Anyone else: public fields only, and hidden profiles
        // answer exactly like a non-existing user, so their existence isn't revealed (ASVS 8.2.2, R10, SAT 3).

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            var isOwner = _userManager.GetUserId(User) == user.Id;
            if (isOwner || await IsAdminAsync())
            {
                return Ok(ToFullProfile(user));
            }

            if (!user.IsVisible)
            {
                return NotFound(new { message = "User not found" });
            }

            return Ok(ToPublicProfile(user));
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

        private static PublicProfileDto ToPublicProfile(User u) =>
            new(u.Id, u.FirstName, u.LastName, u.Campus, u.Programme, u.Availability, u.ProfileUpdatedAt);

        private static FullProfileDto ToFullProfile(User u) =>
            new(u.Id, u.FirstName, u.LastName, u.Email, u.Role, u.Campus, u.Programme, u.Availability, u.IsVisible, u.ProfileUpdatedAt);
    }
}
