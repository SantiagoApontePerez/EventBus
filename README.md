# Scritable Object Event Bus

An scriptable object event bus system with event bus station.

# Features
- AddListeners to an event.
- Set or Get the event value.
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
Setting, Getting, and raising an event.
```
public IntBusEvent intBusEvent;

private void Start()
{
	//Sets the value before raising the event.
	intBusEvent.SetValue(33);
	
	//Gets the value
	var intValue = intBusEvent.GetValue;
	
	//Raises the event to all bindings
	intBusEvent.Raise
}
```
Binding and Listening.
```
public BoolBusBinding boolBusBinding;

private void Awake()
{
    RegisterEvent();
}

//Register to event with or without params.
private void RegisterEvent()
{
    boolBusBinding.AddListener(HandleBoolEvent);
    boolBusBinding.AddListener(HandleEvent);
}

private void HandleBoolEvent(bool state)
{
    Debug.Log($"{this.name} BoolEventBusListener test: received event raised with value {state}");
}

private void HandleEvent()
{
    Debug.Log($"{this.name} BoolEventBusListener test: received event");
}
```


