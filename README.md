# UnityEventSystem
Decoupled Event System for Unity

# Usage

Add the WGEventSystem.cs to any gameobject and use the functions below to trigger events, listen for events or to stop listening to events

WGEventSystem.Instance.TriggerEvent(BaseEvent);

WGEventSystem.Instance.RegisterListener<BaseEvent>(Callback);

WGEventSystem.Instance.UnregisterListener<BaseEvent>(Callback);
