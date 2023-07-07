using UnityEngine;
using System;

[System.Serializable]
public struct LiveTimer {
    public float time;

    [HideInInspector] public float timer;

    public Action OnDestroy;
}
public class MonoLiveTimer : BaseMonoComponent<LiveTimer> { }