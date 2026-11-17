namespace ehb_connect.Server.DTO
{
    // What any logged-in user may see about another user.
    // No e-mail (only after an accepted contact request, R10 / SAT 5), no role, no visibility flag.
    public record PublicProfileDto(
        string Id,
        string FirstName,
        string LastName,
        string Campus,
        string Programme,
        string? Availability,
        DateTime ProfileUpdatedAt);

    // What the owner of the profile and admins may see.
    public record FullProfileDto(
        string Id,
        string FirstName,
        string LastName,
        string? Email,
        string Role,
        string Campus,
        string Programme,
        string? Availability,
        bool IsVisible,
        DateTime ProfileUpdatedAt);
}
