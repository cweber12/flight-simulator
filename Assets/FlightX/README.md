# FlightX

FlightX is a Unity 3D arcade/sim flight prototype. The first playable foundation focuses on simple Rigidbody aircraft motion, inspector-driven input, crash/reset behavior, a chase camera, HUD readouts, runway detection, and basic landing evaluation.

## Folder Structure

- `Art/` - source and imported visual assets.
- `Audio/` - engine, UI, and future sound assets.
- `Input/` - Input System assets for player controls.
- `Materials/` - shared prototype and gameplay materials.
- `Prefabs/` - reusable aircraft, camera, environment, and UI prefabs.
- `Scenes/` - FlightX-specific scenes.
- `Scripts/` - gameplay code organized by namespace and feature area.
- `ScriptableObjects/` - reusable data assets.
- `Settings/` - project-specific tuning assets, including aircraft settings.

## First Prototype Goal

The initial prototype should let a player assemble a test scene, throttle up, take off, fly with pitch/roll/yaw controls, land on a runway, see basic flight data on a HUD, crash on hard impact, and reset the aircraft.

## Commit Hygiene

Unity-generated folders such as `Library/`, `Temp/`, `Logs/`, `Obj/`, `Build/`, `Builds/`, and `UserSettings/` should not be committed. Asset `.meta` files should be committed when Unity creates them.

## Development Order

1. Assemble the manual `TestFlight` scene from `README_SETUP.md`.
2. Create and tune the `AircraftSettings` asset.
3. Wire the Input System actions into `AircraftInput`.
4. Tune aircraft force values in short playtest passes.
5. Convert stable scene objects into prefabs.
6. Add only the next smallest gameplay system needed for the prototype.
