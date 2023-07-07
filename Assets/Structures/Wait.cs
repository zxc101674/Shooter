using UnityEngine;

[System.Serializable]
public struct Wait {
    public float start;
    public float delay;
    [HideInInspector] public bool isStarted;
}
