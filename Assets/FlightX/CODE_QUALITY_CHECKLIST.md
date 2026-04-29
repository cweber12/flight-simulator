# FlightX Code Quality Checklist

- Unity compiles with no console errors.
- EditMode tests pass.
- PlayMode tests pass or known placeholders are documented.
- No generated folders are staged.
- Scripts are under `Assets/FlightX/Scripts`.
- Serialized fields are private.
- Public API is intentional.
- No old `Input.GetKey` usage unless explicitly justified.
- No scene-name hard dependencies.
- `README_SETUP.md` is updated when setup changes.
- Commit is small and scoped.
