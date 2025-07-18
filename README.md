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
2. Name your event asset (e.g. `PlayerDied_BoolEvent`).  
3. Inspect the asset to see type information.

<img width="259" height="43" alt="image" src="https://github.com/user-attachments/assets/905ea447-e98f-4ce5-b3d2-3a0b21c40e1b" />

### 2. Add a Listener Component

1. On any GameObject, click **Add Component**:  
   **Int Event Listener**  
2. Assign your `PlayerDied_BoolEvent` asset to the field.  
3. Click **+** on the UnityEvent to assign a receiver:  
   - Drag the same GameObject (or another) into the slot.  
   - Select a public method that takes an `bool`.

<img width="414" height="169" alt="image" src="https://github.com/user-attachments/assets/b4c8cd97-9327-4a89-ab26-7f74c8a23873" />

### 3. Raise Events in Code

```csharp
using UnityEngine;
using Systems.EventBus.Runtime;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private IntEvent scoreEvent;

    void AddPoints(int points)
    {
        scoreEvent.Raise(points);
    }
}
```
# Debugging at Runtime

**Window → EventBus → Event Debugger**

<img width="403" height="285" alt="image" src="https://github.com/user-attachments/assets/6b1176e5-ffdd-4ff2-a82e-8344dfcba9fe" />

The window lists each event asset with:

- Last Raised By: Object name that last triggered the event.
- Last Raised At: Timestamp (HH:mm:ss).
- Raise Value: Input field and Raise button to manually trigger.
- Listeners: Active listener count.
- Listener Details:
- Name: GameObject or custom listener description.
- Registered: When the listener first subscribed.
- Last Invoked: When it last received an event (or “Never”).
- Select button: Jump to the listener in the Inspector.
