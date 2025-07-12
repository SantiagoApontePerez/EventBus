# Scritable Object Event Bus

An scriptable object event bus system with event bus station.

# Features
- AddListeners to an event.
- Raise events.
- Custom editor to view and raise events manually during runtime.

# Installation

### Via Git URL

   - In Unity, open **Window > Package Manager**.
   - Click the **+** icon and choose **Add package from Git URL...**.
   - Enter: `https://github.com/SantiagoApontePerez/EventBus.git`

### Local Install

Simply download the library into your Unity project `Assets/` folder.

# Usage

### EventBus Station

1. View all currently registered events.
2. View the last time an event was raised.
3. Manually raise an event with value.

### EventBus Events & Binding
```
public IntBusEvent intBusEvent;

private void Start()
{
	//Sets the value before raising the event.
	intBusEvent.SetValue(33);
	intBusEvent.Raise
}
```
