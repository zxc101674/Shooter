using UnityEngine;

[System.Serializable]
public struct Follow {
    public float smooth;
    [Min(0)]
    public float stopDistance;
    public Vector2 offset;

    [HideInInspector] public Vector2 curVelocity;
}
public class Mono_Follow : BaseMonoComponent<Follow> { }