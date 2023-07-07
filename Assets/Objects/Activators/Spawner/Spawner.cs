using Leopotam.Ecs;
using UnityEngine;

public class Spawner : BaseActivator {
    [Header("Spawner")]
    [SerializeField] private Activate_Spawn _activate;

    public override void Activate() {
        _activate.Dialog = dialog ?? null;
        _activate.OnActivate = () => OnActivate(this);
        TryGetEntity()?.Init(_activate);
    }

    public override void Deactivate() {
        _activate.OnActivate = delegate { };
        TryGetEntity()?.Del<Activate_Spawn>();
    }

    private protected override void AddActivatorsComponents() {
        _activate.Dialog = dialog ?? null;
        _activate.OnActivate = () => OnActivate(this);
        gameObject.AddComponent<Mono_Activate_Spawn>().Value = _activate;
    }
}
