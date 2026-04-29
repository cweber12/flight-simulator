# FlightX TestFlight Scene Setup

Use this guide to manually assemble the first playable `TestFlight` scene in Unity. The Codex scaffold intentionally does not create or save a Unity scene file.

## Create the Scene

1. In Unity, create a new scene named `TestFlight`.
2. Save it under `Assets/FlightX/Scenes/TestFlight.unity`.
3. Keep the default directional light, or add one if the scene is empty.

## Create a Placeholder Aircraft

1. Create an empty GameObject named `PlayerAircraft`.
2. Set its tag to `Player`.
3. Add primitive child shapes to make a readable aircraft:
   - Capsule or cube for the fuselage.
   - Thin cubes for wings.
   - Small cubes for tail surfaces.
4. Add a main Collider to the root or colliders to the child shapes.
5. Add a `Rigidbody` to the root aircraft object.

Recommended Rigidbody settings:

- Mass: `1000`
- Drag: `0`
- Angular Drag: `0.5`
- Use Gravity: enabled
- Interpolate: `Interpolate`
- Collision Detection: `Continuous Dynamic`

## Add Aircraft Scripts

Add these components to `PlayerAircraft`:

1. `AircraftInput`
2. `AircraftPhysics`
3. `AircraftController`

On `AircraftPhysics`, assign the `AircraftInput` component and the aircraft settings asset after creating it. On `AircraftController`, assign the `AircraftInput` and `AircraftPhysics` components if Unity does not auto-fill them.

## Create Aircraft Settings

1. Right click `Assets/FlightX/Settings`.
2. Select `Create > FlightX > Aircraft Settings`.
3. Name the asset `DefaultAircraftSettings`.
4. Assign it to `AircraftPhysics`.
5. Start with the default values, then tune after confirming the aircraft can roll, lift off, and land.

## Create Ground and Runway

1. Create a large plane for the ground.
2. Add a material with a neutral color so altitude and horizon are easy to read.
3. Create a long, flat runway from a scaled cube or plane.
4. Make sure the runway has a Collider and is on a layer included by the aircraft settings ground layer mask.
5. Place the aircraft near the start of the runway, pointing down it.

## Create a Runway Trigger Zone

1. Create an empty GameObject named `RunwayZone`.
2. Add a Box Collider.
3. Enable `Is Trigger`.
4. Scale it to cover the runway touchdown area.
5. Add the `RunwayZone` component.
6. Leave `Aircraft Tag` set to `Player`, or clear it to accept any collider.

## Add Landing Evaluator

1. Create an empty GameObject named `FlightSystems`.
2. Add the `LandingEvaluator` component.
3. Assign `AircraftPhysics`, `AircraftController`, and `RunwayZone`.

## Add Chase Camera

1. Create or select the scene camera.
2. Add the `ChaseCamera` component.
3. Assign `PlayerAircraft` as the target.
4. Start with the default offset of `(0, 5, -12)`.
5. Press Play and adjust follow smoothing, rotation speed, and look-ahead distance as needed.

## Create TextMeshPro HUD

1. Create a Canvas using `GameObject > UI > Canvas`.
2. If Unity prompts to import TextMeshPro essentials, import them.
3. Add TextMeshPro UI text objects for:
   - Speed
   - Altitude
   - Vertical Speed
   - Throttle
   - Status
   - Warning
4. Add the `FlightHUD` component to the Canvas or a HUD container object.
5. Assign `AircraftPhysics`, `AircraftController`, and each `TextMeshProUGUI` field.

## Create Input Actions

Create a new Input Actions asset at:

`Assets/FlightX/Input/FlightXInputActions.inputactions`

Add an action map named `Flight` with these actions:

- `Pitch`
- `Roll`
- `Yaw`
- `Throttle`
- `Brake`
- `Reset`
- `CameraSwitch`
- `Pause`

Example bindings:

- Pitch: `W/S` or `Up/Down Arrow` as a 1D axis
- Roll: `A/D` or `Left/Right Arrow` as a 1D axis
- Yaw: `Q/E` as a 1D axis
- Throttle: `Left Shift/Left Ctrl` as a 1D axis
- Brake: `Space`
- Reset: `R`
- CameraSwitch: `C`
- Pause: `Escape`

For each field on `AircraftInput`, assign the matching action as an `InputActionReference` in the Inspector. The script handles missing references safely, so you can wire actions incrementally.

## Test Checklist

1. Press Play.
2. Hold throttle increase until the throttle HUD rises.
3. Roll forward down the runway.
4. Pitch up gently and take off.
5. Test roll, pitch, and yaw response.
6. Reduce throttle, line up with the runway, and land.
7. Confirm status changes between grounded, airborne, stalling, and crashed.
8. Press reset to return to the starting position.

## Common Troubleshooting

### Input System namespace missing

Confirm `com.unity.inputsystem` is installed in `Packages/manifest.json`. If Unity still reports missing types, let the editor finish importing packages and regenerate project files.

### TextMeshPro references missing

Import TextMeshPro essentials when Unity prompts. Ensure HUD text objects use `TextMeshProUGUI`, not legacy `UnityEngine.UI.Text`.

### Aircraft does not move

Check that `AircraftSettings` is assigned, throttle action references are connected, the Rigidbody is not kinematic, and the aircraft is not blocked by colliders at spawn.

### Aircraft flies too aggressively

Lower `pitchPower`, `rollPower`, `yawPower`, or `maxThrust` on the settings asset. Increase Rigidbody mass only after trying tuning values first.

### Camera not following

Assign `PlayerAircraft` to the `ChaseCamera` target field. Confirm the camera component is enabled and there is only one active Audio Listener if you create extra cameras.

### Unity compile errors after Codex changes

Let Unity finish importing scripts, then check the Console for the first error in the list. Fix compile errors from top to bottom because later errors are often follow-on symptoms.
