# EhB Connect — Security rules for developers

*Owner: Tom (security officer) · v1.0 · 2026-11-16 · lives in `docs/security/dev-rules.md`*

These rules apply to every pull request. Tom reviews each PR against them; the J1 security gate checks some of them automatically. The numbers in brackets point to the OWASP ASVS 5.0 requirement or to our security acceptance tests (SATs). Questions → Tom.

## Login and access (we use JWT)

1. **Every endpoint requires login by default.** That's set up centrally. Only add `[AllowAnonymous]` for things like register and login, and explain why in the PR. *(8.2.1, SAT 10)*
2. **"Who is this user?" always comes from the token** (`User` claims), never from the request body, route or query string. Before reading or changing data that belongs to someone, check ownership on the server with the shared ownership handler. *(8.2.2, 8.3.1, SAT 1, 2)*
3. **Don't handle tokens yourself.** Token validation lives in `SecurityExtensions.cs`. In Angular, only the `AuthService` stores the token and it clears it at logout. Never put a token in `localStorage` or in a URL. *(V9, 14.2.1, 14.3.1)*
4. **Don't change `SecurityExtensions.cs`** without Tom as reviewer.

## Data in and out

5. **Request DTOs only.** Never bind an entity directly from the request. Request DTOs never contain `UserId`, `Role`, `IsAdmin` or similar fields. *(SAT 4)*
6. **Response DTOs only.** Never return an entity. Return only the fields the screen needs; e-mail addresses only after an accepted contact request. *(15.3.1, SAT 5)*
7. **Validate every input on the server** with validation attributes on the DTO (required, max length, format). Angular validation is only for the user's convenience. *(2.2.1, 2.2.2)*
8. **Database: EF Core LINQ only.** No `FromSqlRaw` / `ExecuteSqlRaw` with string concatenation or interpolation. Sorting by a user-chosen column goes through an allowlist. *(1.2.4, SAT 12)*
9. **Check the state before every step in a flow.** For example, you can only accept a contact request that is pending and addressed to you. *(2.3.1)*

## Front end

10. **Show user text with `{{ }}`.** No `[innerHTML]`, no `bypassSecurityTrust…`, no `eval`. Links entered by users must start with `https://`. *(3.2.2, 1.3.2, 1.2.2, SAT 6)*

## HTTP and errors

11. **Changes only via POST, PUT, PATCH or DELETE**, never via GET. *(3.5.3)*
12. **Don't return exception messages.** Let the global error handler answer with a generic error: no stack traces, SQL or file paths in responses. *(SAT 13)*

## Secrets, packages and data

13. **No secrets in code or `appsettings.json`.** Use `dotnet user-secrets` locally. The gitleaks check blocks a PR with a secret. If one leaks anyway, tell Tom at once: the secret must be replaced, deleting it from the code is not enough. *(13.3.1)*
14. **New package?** Mention it in the PR description. The npm and NuGet audits must be green. *(15.2.1)*
15. **Fictitious test data only.** No real names or e-mail addresses of EhB students, not in code, tests, seed data or screenshots. *(Definition of Done, GDPR)*

---

## For the PR template

Paste this section into `.github/pull_request_template.md`:

```markdown
### Security (see docs/security/dev-rules.md)
- [ ] New endpoints require login, or the PR explains why `[AllowAnonymous]` is needed
- [ ] The user ID comes from the token; ownership is checked on the server
- [ ] Request and response DTOs used; no entities in or out
- [ ] Input is validated on the server
- [ ] No raw SQL built from strings; no `[innerHTML]` or `bypassSecurityTrust…`
- [ ] No secrets, tokens or real personal data in code, tests or logs
- [ ] `SecurityExtensions.cs` unchanged, or Tom is a reviewer
```
