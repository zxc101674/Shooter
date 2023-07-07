using UnityEngine;

[System.Serializable]
public struct Speed {
    public float value;
    [HideInInspector] public float current;
    [HideInInspector] public float startToChange;

    public void SaveSpeed() {
        startToChange = current;
    }
}