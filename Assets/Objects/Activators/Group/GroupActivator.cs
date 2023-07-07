public class GroupActivator : BaseActivator {
    private Event_Activate _activate;

    public override void Activate() {
        _activate.Dialog = dialog ?? null;
        _activate.OnActivate = () => OnActivate(this);
        TryGetEntity()?.Init(_activate);
    }

    public override void Deactivate() {
        _activate.OnActivate = delegate { };
    }

    private protected override void AddActivatorsComponents() {
        _activate.Dialog = dialog ?? null;
        _activate.OnActivate = () => OnActivate(this);
        gameObject.AddComponent<Mono_Event_Activate>().Value = _activate;
    }
}
