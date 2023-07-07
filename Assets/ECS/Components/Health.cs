using UnityEngine;
using System;

[System.Serializable]
public struct Health {
    public CorrectValue health;
    public CorrectValue shield;
    public float timeGetDamage;
    public Color colorToGetDamage;
    [HideInInspector] public float timerGetDamage;

    public Action<CorrectValue> OnUpdateHealth;
    public Action<CorrectValue> OnUpdateShield;
    public Action OnDead;
}
public class MonoHealth : BaseMonoComponent<Health> { }