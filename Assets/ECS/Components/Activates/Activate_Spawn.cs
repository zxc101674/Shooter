using UnityEngine;
using System;

[Serializable]
public struct Activate_Spawn : IActivate {
    public bool isInfinity;
    public Transform prefab;
    public float interval;
    public int maxCount;
    public Vector2 position;
    public Space space;

    [HideInInspector] public int count;
    [HideInInspector] public int countDead;
    [HideInInspector] public float timer;
    
    public SO_Dialog Dialog { get; set; }
    public Action OnActivate { get; set; }
}
public class Mono_Activate_Spawn : BaseMonoComponent<Activate_Spawn> { }