using UnityEngine;

[System.Serializable]
public struct Fix {
    public float power;
    public float interval;
    [HideInInspector] public float timer;
}
public class MonoFix : BaseMonoComponent<Fix> { }