using UnityEngine;

public class EntityCamera : BaseEntityObject {
    [SerializeField] private Follow follow;
    private protected override void AddComponents() {
        gameObject.AddComponent<Mono_Follow>().Value = follow;
    }
}
