# Unity CI Notes

FlightX does not have an active GitHub Actions Unity workflow yet.

Unity CI can be added later with `game-ci/unity-test-runner`, but it needs project-specific setup before it should run on every push:

- Confirm the exact Unity editor version used by the project.
- Choose a Unity license activation strategy.
- Add the required GitHub secrets for license activation.
- Decide whether CI should run EditMode tests only or both EditMode and PlayMode tests.

For now, run tests locally through Unity Test Runner:

`Window > General > Test Runner`
