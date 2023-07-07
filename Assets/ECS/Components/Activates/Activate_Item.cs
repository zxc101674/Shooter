using UnityEngine;
using System;

[Serializable]
public struct Activate_Item : IActivate {
    public int distance;

    [HideInInspector] public Canvas ui;
    
    public Action OnDestroy;

    public SO_Dialog Dialog { get; set; }
    public Action OnActivate { get; set; }
}
public class Mono_Activate_Item : BaseMonoComponent<Activate_Item> { }