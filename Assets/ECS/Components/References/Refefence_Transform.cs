using UnityEngine;
using Voody.UniLeo;

public struct Refefence_Transform {
    public Transform transform;
}

public class Mono_Refefence_Transform : MonoProvider<Refefence_Transform> {
    private void Awake() {
        value.transform = transform;
    }
}