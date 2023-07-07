using Voody.UniLeo;
using System;

public abstract class BaseEntityObject : ConvertToEntity {
    public Action OnDead = delegate { };

    public virtual void Activate() { }
    public virtual void Deactivate() { }
    private protected virtual void InitUnityComponents() { }
    private protected virtual void SettingsUnityComponents() { }
    private protected virtual void InitEntityComponents() { }
    private protected virtual void InitEntityEvents() { }
    private protected virtual void AddComponents() { }


    private protected void Awake() {
        InitUnityComponents();
        SettingsUnityComponents();
        InitEntityComponents();
        InitEntityEvents();
        gameObject.AddComponent<Mono_Refefence_Transform>();
        gameObject.AddComponent<Mono_Event_Object_Activate>();
        AddComponents();
    }

    private void OnEnable() {
        if (TryGetEntity() == null) return;
        TryGetEntity()?.Init<Event_Object_Activate>();
        Activate();
    }

    private void OnDisable() {
        TryGetEntity()?.Init<Event_Object_Deactivate>();
        Deactivate();
        OnDead();
    }
}
