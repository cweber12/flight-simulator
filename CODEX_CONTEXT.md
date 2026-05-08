# FlightX Unity Project Context

Project name: FlightX_Unity  
Game name: FlightX  
Engine: Unity  
Editor workflow: Unity Editor + VS Code  
Primary language: C#  
Game type: 3D flight simulator / arcade-sim flight game  

## Development Goal

Build a playable flight simulator prototype where the player can:
- throttle up
- take off from a runway
- control pitch, roll, and yaw
- fly around a test environment
- land back on the runway
- crash on hard impact
- view speed, altitude, vertical speed, throttle, stall, and crash status on a HUD

## Implementation Style

Start with an arcade/sim hybrid, not a full real-world physics simulator.

Use:
- Rigidbody physics
- simplified thrust, lift, drag, and torque
- ScriptableObjects for aircraft tuning
- Unity new Input System
- TextMeshPro for HUD
- clean modular scripts

Avoid:
- multiplayer
- real-world terrain
- advanced avionics
- complex cockpit systems
- full aerodynamic simulation
- unnecessary dependencies

## Folder Rules

Place all project-specific files under:

Assets/FlightX/

Scripts should be organized into:

Assets/FlightX/Scripts/Aircraft
Assets/FlightX/Scripts/Camera
Assets/FlightX/Scripts/Core
Assets/FlightX/Scripts/Environment
Assets/FlightX/Scripts/Input
Assets/FlightX/Scripts/UI

## Coding Rules

- Use clear C# naming.
- Keep scripts small and focused.
- Prefer serialized fields over public mutable fields.
- Use [SerializeField] private fields.
- Use read-only public properties where outside scripts need data.
- Add tooltips for tuning values.
- Make code easy to tune in the Unity Inspector.
- Do not use old Input.GetKey unless specifically requested.
- Use the new Input System with InputActionReference.
- Keep prototype code simple and readable.

## First Prototype Features

1. AircraftSettings ScriptableObject
2. AircraftInput
3. AircraftPhysics
4. AircraftController
5. ChaseCamera
6. FlightHUD
7. RunwayZone
8. LandingEvaluator
9. TestFlight scene setup guide

---

## System Map

| File | Namespace | What it does |
|------|-----------|-------------|
| `AircraftInput.cs` | `FlightX.Input` | Reads Unity Input System actions; exposes normalized floats and button states |
| `AircraftSettings.cs` | `FlightX.Aircraft` | ScriptableObject — all tuning values (thrust, lift, drag, limits) |
| `AircraftPhysics.cs` | `FlightX.Aircraft` | Applies forces to Rigidbody each FixedUpdate; tracks speed, altitude, stall, grounded state |
| `AircraftController.cs` | `FlightX.Aircraft` | Owns crash detection via OnCollisionEnter; handles reset; polls AircraftInput for reset button |
| `ChaseCamera.cs` | `FlightX.Camera` | Smoothly follows aircraft Transform; SmoothDamp position, Slerp rotation |
| `RunwayZone.cs` | `FlightX.Environment` | Trigger collider; tracks whether the aircraft (by tag) is inside the runway zone |
| `LandingResult.cs` | `FlightX.Core` | Immutable struct — touchdown data snapshot (no UnityEngine dependency) |
| `LandingEvaluator.cs` | `FlightX.Core` | Detects ground transition each Update; calls EvaluateTouchdown; stores LandingResult |
| `FlightHUD.cs` | `FlightX.UI` | Reads AircraftPhysics + AircraftController each Update; writes to TextMeshPro labels |

## Dependency Graph

```
FlightXInputActions (Input Asset)
        │
        ▼
  AircraftInput          AircraftSettings (ScriptableObject)
        │                        │
        ├────────────────────────┤
        ▼                        ▼
  AircraftPhysics ◄──── AircraftController
        │                        │
        ├────────────────────────┤
        ▼                        ▼
   FlightHUD              LandingEvaluator ◄── RunwayZone
                                 │
                                 ▼
                           LandingResult

  ChaseCamera ──follows──► Aircraft Transform (no script dep)
```

Key rules:
- `AircraftInput` has no dependency on any other FlightX script
- `AircraftSettings` has no dependency on any other FlightX script
- `LandingResult` has no UnityEngine dependency — safe to test without Unity runner
- `ChaseCamera` only holds a `Transform` reference, not a script reference

## Data Flow

**Control loop (per frame):**
```
Player input
  → AircraftInput (reads InputActionReferences)
  → AircraftPhysics.FixedUpdate (applies thrust/lift/drag/torque to Rigidbody)
  → AircraftController.Update (checks reset button, handles crash state)
  → FlightHUD.Update (displays speed/altitude/VS/throttle/status)
```

**Landing detection:**
```
Rigidbody touches ground
  → AircraftPhysics.IsGrounded flips true
  → LandingEvaluator.Update detects grounded transition
  → RunwayZone.IsAircraftInRunwayZone queried
  → LandingResult struct created and stored on LandingEvaluator
```

## Assembly Definitions

| Asmdef | Contains |
|--------|----------|
| `FlightX.Runtime` | All runtime scripts under `Assets/FlightX/Scripts/` |
| `FlightX.EditModeTests` | Tests in `Assets/FlightX/Tests/EditMode/` |
| `FlightX.PlayModeTests` | Tests in `Assets/FlightX/Tests/PlayMode/` |

## Test Coverage

| Test file | What it covers |
|-----------|---------------|
| `AircraftSettingsTests.cs` | Default value validation on AircraftSettings ScriptableObject |
| `LandingResultTests.cs` | LandingResult struct construction and field correctness |
| `LandingEvaluatorScoringTests.cs` | Scoring logic for touchdown conditions (speed, attitude, runway) |
| `FlightXSmokeTests.cs` | PlayMode: runtime smoke coverage |

Run tests via Unity Test Runner: `Window > General > Test Runner`.

## What Does Not Exist Yet

Do not search for or assume these exist:
- No aircraft **prefab** — aircraft is manually placed in `TestFlight.unity`
- No **pause menu** or pause logic (PausePressed is read by AircraftInput but nothing acts on it)
- No **camera switching** logic (CameraSwitchPressed is exposed but unused)
- No **multiplayer**, networking, or save system
- No **audio** system
- No **particle effects** or visual feedback on crash/landing
- Cinemachine is a future recommendation but **not yet installed**

## Project Layout

```
Assets/FlightX/
├── Scripts/
│   ├── Aircraft/         → AircraftController, AircraftPhysics, AircraftSettings
│   ├── Camera/           → ChaseCamera
│   ├── Core/             → LandingEvaluator, LandingResult
│   ├── Environment/      → RunwayZone
│   ├── Input/            → AircraftInput
│   └── UI/               → FlightHUD
├── Tests/
│   ├── EditMode/         → Deterministic unit tests (no scene required)
│   └── PlayMode/         → Runtime smoke tests
├── ScriptableObjects/    → DefaultAircraftSettings.asset
├── Scenes/               → TestFlight.unity (the only scene)
├── Input/                → FlightXInputActions.inputactions
└── Prefabs/              → (empty — no prefab yet)
```

## Scene: TestFlight

Single scene at `Assets/FlightX/Scenes/TestFlight.unity`. Contains:
- Aircraft GameObject with `AircraftInput`, `AircraftPhysics`, `AircraftController` (all on the same object)
- `ChaseCamera` as a separate GameObject
- `FlightHUD` on a Canvas
- `RunwayZone` trigger on the runway mesh
- `LandingEvaluator` on a manager GameObject