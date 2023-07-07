using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseActivator : BaseEntityObject {
    [Header("Activator")]
    public SO_Dialog dialog;

    [SerializeField] private protected bool isInfinity;
    [SerializeField] private List<BaseActivator> locks;

    public Action<BaseActivator> OnActivate = delegate { };
    private protected virtual void AddActivatorsComponents() { }

    private List<BaseActivator> unlocks = new();

    private void InitActiveEvent() {
        for (int i = 0; i < locks.Count; ++i) {
            locks[i].OnActivate += (BaseActivator val) => {
                int index = locks.IndexOf(val);
                if (index == -1) return;
                if (val.isInfinity) unlocks.Add(locks[index]);
                locks.RemoveAt(index);
                if (locks.Count == 0) {
                    Deactivate();
                    Activate();
                    if (val.isInfinity) {
                        for (int i = 0; i < unlocks.Count; ++i) {
                            locks.Add(unlocks[i]);
                        }
                        unlocks.Clear();
                    }
                }
            };
        }
    }

    private protected sealed override void AddComponents() {
        InitActiveEvent();
        OnActivate += (BaseActivator val) => dialog = null;
        if (locks.Count == 0) {
            AddActivatorsComponents();
        }
    }
}
