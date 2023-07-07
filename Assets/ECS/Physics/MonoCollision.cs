using Leopotam.Ecs;
using Voody.UniLeo;
using UnityEngine;
using System;

public class MonoCollision : MonoBehaviour {
    [SerializeField] private LayerMask layers;

    public Action<EcsEntity> OnEnter = delegate { };
    public Action<EcsEntity> OnExit = delegate { };

    private void OnCollisionEnter2D(Collision2D collision) {
        var component = collision?.gameObject.GetComponent<ConvertToEntity>();
        if (component != null && (1 << collision.gameObject.layer & layers) != 0)
            OnEnter(component.TryGetEntity().Value);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        var component = collision?.gameObject.GetComponent<ConvertToEntity>();
        if (component != null && (1 << collision.gameObject.layer & layers) != 0)
            OnExit(component.TryGetEntity().Value);
    }
}
