using UnityEngine;
using System;

[Serializable]
public struct Activate_Trigger : IActivate {
    public Wait waitInSeconds;
    public LayerMask mask;

    [HideInInspector] public bool isReady;
    [HideInInspector] public float timer;
    [HideInInspector] public LayerMask triggerMask;

    public Action OnActivate { get; set; }
    public SO_Dialog Dialog { get; set; }
}
public class Mono_Activate_Trigger : BaseMonoComponent<Activate_Trigger> { }