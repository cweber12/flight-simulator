# FlightX Development

## Opening the Project

Open the repository root in Unity Hub, then open the same folder in VS Code for code editing. Let Unity finish importing packages before running tests or trusting generated project files.

## Required Tools

- Unity
- VS Code
- .NET SDK
- C# / C# Dev Kit / Unity VS Code extensions

## Recommended Packages

- Input System
- Cinemachine
- TextMeshPro

The current scaffold uses Input System and TextMeshPro. Cinemachine is recommended for future camera iteration, but the prototype currently uses a lightweight custom chase camera.

## Git Workflow

- Make small, scoped commits.
- Run a Unity compile before committing.
- Run tests before larger commits or shared changes.
- Keep generated Unity cache folders out of Git.

## Running Tests

1. Open `Window > General > Test Runner`.
2. Run `EditMode` tests for deterministic logic and safe component checks.
3. Run `PlayMode` tests for runtime smoke coverage.
4. Keep tests independent of a specific scene until stable prefabs and `TestFlight` exist.

## Do Not Commit

- `Library/`
- `Temp/`
- `Obj/`
- `Logs/`
- `Build/`
- `Builds/`
- `UserSettings/`
- Generated `.csproj` or `.sln` files

## Current Prototype Scope

FlightX is currently a first playable foundation: aircraft settings, input adapter, simplified Rigidbody flight physics, crash/reset control, chase camera, HUD display, runway trigger, landing evaluation, and manual scene setup documentation.

## Future Testing Notes

- Add prefab tests after an aircraft prefab exists.
- Add scene smoke tests after the `TestFlight` scene exists.
- Add PlayMode physics validation after flight behavior stabilizes.
- Add HUD formatting tests if UI state grows beyond simple text output.
