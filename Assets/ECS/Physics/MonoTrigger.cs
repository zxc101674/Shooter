using Leopotam.Ecs;
using Voody.UniLeo;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider2D))]
public class MonoTrigger : MonoBehaviour {
    [SerializeField] private LayerMask layers;

    public Action<EcsEntity> OnEnter = delegate { };
    public Action<EcsEntity> OnExit = delegate { };

    private void OnTriggerEnter2D(Collider2D collision) {
        var component = collision?.GetComponent<ConvertToEntity>();
        if (component != null && (1 << collision.gameObject.layer & layers) != 0) {
            if(component.TryGetEntity() != null) {
                OnEnter(component.TryGetEntity().Value);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        var component = collision?.GetComponent<ConvertToEntity>();
        if(component != null && (1 << collision.gameObject.layer & layers) != 0)
            OnExit(component.TryGetEntity().Value);
    }
}
