# Unity CI Setup

FlightX includes a conservative GitHub Actions workflow at `unity-tests.yml`. It connects the repository project to Unity by checking out this VS Code/Unity project, restoring a Unity `Library` cache, and running Unity Test Runner through GameCI.

The workflow is intentionally license-gated. It will not fail a fresh repository just because Unity license secrets have not been configured yet. Instead, it prints setup instructions and skips the Unity test step until the required secrets exist.

Use GitHub **repository secrets**, not repository variables or environment variables. Secrets are masked in logs; variables can be printed by Actions and third-party actions.

## What the Workflow Runs

- Repository hygiene checks for generated Unity folders and old Input polling.
- EditMode tests through Unity Test Runner.
- PlayMode tests through Unity Test Runner.
- Test result artifact upload when Unity tests run.

## Unity Version

The project currently uses:

`6000.1.14f1`

This value comes from `ProjectSettings/ProjectVersion.txt` and is set in `unity-tests.yml` as `UNITY_VERSION`.

## Required GitHub Secrets

Open the repository on GitHub, then go to:

`Settings > Secrets and variables > Actions > New repository secret`

For a Unity Personal license, add:

- `UNITY_LICENSE`

For this repository, the Personal/ULF path uses only `UNITY_LICENSE`. The license file is created locally in Unity Hub, then its contents are stored as a GitHub Actions secret.

For a Unity Pro license, add:

- `UNITY_SERIAL`
- `UNITY_EMAIL`
- `UNITY_PASSWORD`

Use exactly one activation mode. Do not configure both `UNITY_LICENSE` and `UNITY_SERIAL` at the same time.

## Special Characters In Passwords

GameCI serial activation passes `UNITY_PASSWORD` into the Docker-based Unity activation command. Passwords containing shell metacharacters such as `>`, `<`, `|`, `&`, `$`, backticks, or quotes can break the generated Docker command before Unity starts.

Recommended fix:

1. Prefer Personal/ULF activation with `UNITY_LICENSE` when possible.
2. If using Pro serial activation, use a Unity ID password that avoids shell metacharacters.
3. Store it as a GitHub Actions secret named `UNITY_PASSWORD`, not as a plain variable.
4. If a password appears in workflow logs, rotate it immediately.

GameCI documents Unity activation setup here:

- https://game.ci/docs/github/activation/
- https://game.ci/docs/github/test-runner/

## Personal ULF Setup

GameCI's current guidance for a Personal license is simpler than the older activation-file flow:

1. Sign in to Unity Hub locally.
2. Activate your Personal license in Unity Hub on your machine.
3. Locate the local `Unity_lic.ulf` file.
4. Copy the full contents of that file into the GitHub Actions secret `UNITY_LICENSE`.

Common license file locations:

- Windows: `C:\ProgramData\Unity\Unity_lic.ulf`
- macOS: `/Library/Application Support/Unity/Unity_lic.ulf`
- Linux: `~/.local/share/unity3d/Unity/Unity_lic.ulf`

The older GitHub activation-file action has been deprecated. You do not need that workflow if you already have a valid locally activated Personal license file.

## Local Workflow Before Pushing

1. Open the project in Unity Hub.
2. Let Unity import and compile.
3. Run `Window > General > Test Runner`.
4. Run EditMode tests.
5. Run PlayMode tests.
6. Commit only source, settings, tests, and docs.
7. Push to GitHub and check the `Unity Tests` workflow.

## Prompt Used To Create This Pipeline

Use this prompt when asking Codex to recreate or update the CI pipeline:

```text
You are working inside the FlightX_Unity Unity project.
Read CODEX_CONTEXT.md, Assets/FlightX/DEVELOPMENT.md, and .github/workflows/README.md.
Create or update a conservative GitHub Actions Unity CI pipeline.
Use the Unity version from ProjectSettings/ProjectVersion.txt.
Use game-ci/unity-test-runner@v4 to run EditMode and PlayMode tests.
Gate Unity test execution behind Unity license secrets so a fresh repo does not fail before secrets are configured.
Support Unity Personal secrets: UNITY_LICENSE, UNITY_EMAIL, UNITY_PASSWORD.
Support Unity Pro secrets: UNITY_SERIAL, UNITY_EMAIL, UNITY_PASSWORD.
Add a lightweight repository hygiene job that checks generated Unity folders are not tracked and FlightX runtime scripts do not use old Input.Get polling.
Cache the Unity Library folder with actions/cache.
Upload Unity test artifacts when tests run.
Do not add build/deploy steps, package changes, generated Unity folders, or Git LFS.
Update .github/workflows/README.md with setup instructions and manual steps.
Validate YAML and summarize what still needs to be configured in GitHub.
```

## Notes

This workflow tests the Unity project in CI. It does not build a player, deploy artifacts, or create releases.
