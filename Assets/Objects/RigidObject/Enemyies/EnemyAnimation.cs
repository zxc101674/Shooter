using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class EnemyAnimation : MonoBehaviour {
    public Action OnDead = delegate { };
    private void Dead() => OnDead();
}
