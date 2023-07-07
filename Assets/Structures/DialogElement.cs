using UnityEngine;

[System.Serializable]
public struct DialogElement {
    public Sprite sprite;
    [Multiline(5)]
    public string text;
}
