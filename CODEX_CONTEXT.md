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