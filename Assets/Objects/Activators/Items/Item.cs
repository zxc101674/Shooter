using UnityEngine;
using Leopotam.Ecs;

public class Item : BaseActivator {
    [Header("Item")]
    [SerializeField] private Canvas ui;
    [SerializeField] private Activate_Item _activator;

    public override void Activate() {
        _activator.ui = ui;
        _activator.Dialog = dialog ?? null;
        _activator.OnActivate = () => OnActivate(this);
        if(!isInfinity) _activator.OnDestroy = () => Destroy(gameObject);
        TryGetEntity()?.Init(_activator);
    }

    private protected override void AddActivatorsComponents() {
        _activator.ui = ui;
        _activator.Dialog = dialog ?? null;
        _activator.OnActivate = () => OnActivate(this);
        if (!isInfinity) _activator.OnDestroy = () => Destroy(gameObject);
        gameObject.AddComponent<Mono_Activate_Item>().Value = _activator;
    }
}
