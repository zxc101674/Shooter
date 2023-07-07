using UnityEngine;

public abstract class System_BaseActivate {
    private protected HUD _hud;

    private protected void Activate(IActivate activator) {
        if (activator.Dialog != null) {
            _hud.ShowDialog(activator.Dialog, activator.OnActivate);
            activator.Dialog = null;
        } else {
            activator.OnActivate?.Invoke();
        }
    }
}