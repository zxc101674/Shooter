using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Attack {
    public float power;
    public float slowing;
    public List<EcsEntity> targets;

    [HideInInspector] public Animator animator;
}
public class Mono_Attack : BaseMonoComponent<Attack> { }