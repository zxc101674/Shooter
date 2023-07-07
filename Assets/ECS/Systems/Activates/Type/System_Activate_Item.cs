using UnityEngine.InputSystem;
using Leopotam.Ecs;
using System;

sealed class System_Activate_Item : System_BaseActivate, IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem {
    private EcsFilter<Activate_Item> _filter;
    private Data_Items _data;
    private Data_Input _input;
    private Runtime_Hero _hero;

    public void Init() {
        _input.input.Hero.Enable();
        _input.input.Hero.Activate.performed += Activate;
    }

    void IEcsRunSystem.Run() {
        foreach(int index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var item = ref _filter.Get1(index);
            
            var transform = entity.GetTransform();

            if (_data.Has(entity)) {
                if (transform.Distance(_hero.transform) > item.distance) {
                    _data.Remove(entity);
                    item.ui.enabled = false;
                }
            } else if (transform.Distance(_hero.transform) <= item.distance) {
                item.ui.enabled = true;
                _data.Add(entity);
            }
        }
    }

    private void Activate(InputAction.CallbackContext context) {
        Action OnActivate = delegate { };
        SO_Dialog dialog = null;
        while (_data.Count > 0) {
            if (_data.Get(0) == default) continue;
            if (!_data.Get(0).Has<Activate_Item>()) continue;
            Activate_Item item = _data.Get(0).Get<Activate_Item>();
            OnActivate += item.OnActivate;
            dialog ??= item.Dialog;
            item.Dialog = null;
            item.OnDestroy();
            _data.RemoveAt(0);
        }
        if (dialog != null) _hud.ShowDialog(dialog, OnActivate);
        else                OnActivate();
    }

    public void Destroy() {
        _input.input.Hero.Disable();
    }
}