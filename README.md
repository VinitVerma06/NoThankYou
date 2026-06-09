# NoThankYou

A third-person action game where the player defends a goal post from physics-driven projectiles fired by coordinated cannons. Block every shot, deflect what you can, and protect the goal before time runs out.

![Gameplay Screenshot](https://github.com/VinitVerma06/NoThankYou/blob/ba796e2f78f734c7d596774b5e74cab96a04dfac/Screenshots/NoThankYou_Main_Menu.png)

Available for:
- 🖥️ Windows PC (controller support)

## 📖 About

NoThankYou is a third-person defense game where the player controls a character standing in front of a goal post. Cannons fire projectiles in a coordinated sequence and the player must physically reposition their character to intercept and deflect each shot using the game's physics-based deflection system.

**Project Purpose:** Built as an original project to explore Unity 6.3 physics systems, Cinemachine, and the Unity New Input System. Every system was independently designed and implemented without tutorial reference, with a focus on modular, single-responsibility architecture.

## ✨ Key Features

### Core Gameplay
- **Deflection System**: Physics-based projectile deflection using PhysicsMaterial bounce — no scripted responses
- **Cannon Coordination**: Multiple cannons fire in a defined sequence with configurable intervals and rest periods
- **Win Conditions**: Survive the timer or deflect all projectiles
- **Spray System**: Randomised projectile spray patterns keep each volley unpredictable

### Technical Features
- **Character Controller**: Rigidbody-based movement with SphereCast ground detection, single jump, and full air control
- **Camera System**: Cinemachine OrbitalFollow with mouse-look, scroll-wheel zoom, and custom ground-clamp collision extension
- **Input System**: Unity New Input System supporting keyboard, mouse, and gamepad with camera-relative directional movement
- **Boundary System**: Invisible player boundaries using Layer Collision Matrix — players blocked, projectiles pass through freely
- **Goal Detection**: Trigger-based detection using component identifier pattern instead of string tags

### Polish & UX
- **Ground Clamp**: Custom CinemachineExtension prevents camera from clipping below terrain at any zoom level or camera angle
- **Projectile Lifecycle**: Automatic despawn on goal hit, lifetime expiry, or post-deflection velocity threshold
- **Game State Management**: Full UI flow with menu, gameplay, and end states

## 🛠️ Technologies & Tools

**Engine & Framework:**
- Unity 6 (6000.3.13f1 LTS)
- C# (.NET Standard 8)

**Key Unity Packages:**
- Unity New Input System (cross-device input)
- Cinemachine 3.1.5 (camera management)
- TextMeshPro (UI text rendering)

**Architecture Patterns:**
- Component-based architecture
- Single-responsibility scripts
- Component identifier pattern (over string tags)
- Event-driven projectile lifecycle (C# Actions)
- Layer Collision Matrix for physics filtering

**Version Control:**
- Git
- GitHub

## 🎯 Game Mechanics

### Player
The player controls a third-person character with full freedom of movement within the play boundary. Jumping allows the player intercept high shots. Air control is intentionally unrestricted so mid-air repositioning always feels responsive.

### Cannons
- Static cannons positioned around the play area
- Fire in a defined sequence coordinated by GameHandler
- Configurable fire interval and rest period between volleys
- Spray angle adds variance to each shot's impact point

### Projectiles
Spherical projectiles fired with physics impulse force. When they contact the player, PhysicsMaterial bounce handles deflection naturally Deflected projectiles despawn once their velocity drops below a threshold, keeping the play area clean.

### Goal
A trigger volume covers the goal opening. Any projectile entering the trigger is detected as a goal scored. The visual frame and trigger are separated — detection is purely logical with no physics interference.

### Boundaries
Four invisible walls surround the play area keeping the player within bounds. Projectiles are excluded from boundary collision via Unity's Layer Collision Matrix, allowing deflected shots to travel freely beyond the play area.

## 📱 Controls

### PC (Keyboard & Mouse)
- **WASD**: Movement (camera-relative)
- **Space**: Jump
- **Mouse**: Camera look
- **Scroll Wheel**: Zoom in / out
- **ESC**: Pause menu

### Controller
- **Left Stick**: Movement
- **South Button**: Jump
- **Right Stick**: Camera look
- **Left Bumper**: Zoom in 
- **Right Bumper**: Zoom out
- **Start Button**: Pause menu

## 🎓 What I Learned

### Unity Skills
- Rigidbody-based character controller with SphereCast ground detection.
- Cinemachine OrbitalFollow and writing custom CinemachineExtensions.
- Unity New Input System for multi-device input handling.
- Layer Collision Matrix for physics-level collision filtering.
- Physics materials for bounce and deflection behaviour.
- Scene management and UI state transitions.

### Programming Concepts
- Component identifier pattern as a type-safe alternative to string tags.
- C# Actions and events for decoupled projectile lifecycle management.
- Single-responsibility principle applied across the game systems.
- Camera-relative movement using transform direction mapping.
- Physics filtering without runtime code using Unity's layer system.

### Game Design
- Coordinated enemy timing to create fair but escalating pressure.
- Air control tuning for responsive mid-air player feel.
- Separation of visual and logical game objects (triggers vs colliders).
- Iterative playtesting to balance cannon fire rate and rest periods.

## 🚀 Future Enhancements

- [ ] Addition game mode where you have to save goals for as long as you can.
- [ ] Multiple levels with different cannon layouts and positions.
- [ ] Visual polish (projectile trails, impact particles, SFX).
- [ ] Audio system (cannon fire, deflection, goal sounds).
- [ ] Object pooling for projectiles.

## 🐛 Known Issues

- Player character's jump can be affected by the projectiles.
- Player camera can get behind the wall.

## 📄 License

This project was created for portfolio purposes.  
All Rights Reserved — not for redistribution.

## 📧 Contact

**Vinit Verma**
- GitHub: [@VinitVerma06](https://github.com/VinitVerma06)
- LinkedIn: [Vinit Verma](https://www.linkedin.com/in/vinitverma06/)
- Email: vinit07verma06@gmail.com
