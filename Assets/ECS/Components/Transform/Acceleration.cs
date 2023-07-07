using UnityEngine;

[System.Serializable]
public struct Acceleration {
    public bool isCanMulty;
    public AnimationCurve curve;
    [HideInInspector] public float curTime;
    [HideInInspector, Min(1)] public float value;

    public void AddValue(float val) {
        if (isCanMulty || value == 0) {
            value += val;
            curTime = 0;
        }
    }

    public void ReduceValue(float val) {
        value = isCanMulty ? value - val : 0;
        curTime = 0;
    }
}