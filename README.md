# Scritable Object Event Bus

A lightweight, decoupled event system for Unity leveraging ScriptableObjects and inspector-friendly listeners.

# Features
- **Generic Events**: Supports any type `T` (int, float, bool, string, custom structs, etc.).
- **ScriptableObject Assets**: Create events via Unity’s `Create Asset` menu for drag-and-drop usage.
- **Inspector Listeners**: Attach `EventListener` components to GameObjects and wire up UnityEvents in the Inspector—no code references required.
- **Runtime Debugger Window**: Inspect all events, listener counts, registration times, and last invoke times.

# Installation

### Via Git URL

   - In Unity, open **Window > Package Manager**.
   - Click the **+** icon and choose **Add package from Git URL...**.
   - Enter: `https://github.com/SantiagoApontePerez/EventBus.git`

### Local Install

Simply download the library into your Unity project `Assets/` folder.

# Usage

### 1. Create an Event Asset
1. In the Project window, right-click:  
   **Create → EventBus → Events**  
2. Name your event asset (e.g. `ScoreUpdated_IntEvent`).  
3. Inspect the asset to see type information.

<img width="259" height="43" alt="image" src="https://github.com/user-attachments/assets/905ea447-e98f-4ce5-b3d2-3a0b21c40e1b" />
