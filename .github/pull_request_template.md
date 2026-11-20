<!--
PR title: short and specific, e.g. "F3: create project call form".
Delete any section that doesn't apply. "None" is a fine answer.
-->

## Feature
F-ID: <!-- F1–F7, or "none" for chores/infra -->
Trello card: <!-- link, if there is one -->

## What
<!-- What changed, in 1–3 bullets -->
-

## Why
<!-- The reason for the change -->

## Impact on the team
<!-- Anything others must do after pulling: new packages, migrations, config, env vars. -->

## Not included
<!-- Known gaps or follow-ups -->

## How to test
<!-- Steps the reviewer can follow -->
1.

## Checklist
- [ ] Builds and tests pass locally
- [ ] No secrets, connection strings or real personal data in the code or commits
- [ ] Feature status updated, if it changed
- [ ] Technical decision in this PR? Then it's recorded in `docs/decisions/` or flagged to Tom

### Security (see docs/security/dev-rules.md)
- [ ] New endpoints require login, or the PR explains why `[AllowAnonymous]` is needed
- [ ] The user ID comes from the token; ownership is checked on the server
- [ ] Request and response DTOs used; no entities in or out
- [ ] Input is validated on the server
- [ ] No raw SQL built from strings; no `[innerHTML]` or `bypassSecurityTrust…`
- [ ] No secrets, tokens or real personal data in code, tests or logs
- [ ] `SecurityExtensions.cs` unchanged, or Tom is a reviewer
