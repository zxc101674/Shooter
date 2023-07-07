using LeoEcsPhysics;
using Leopotam.Ecs;
using UnityEngine;

public static class ECSCoreHelper {
    public static EcsEntity Init<T>(this EcsEntity entity) where T : struct {
        entity.Get<T>();
        return entity;
    }

    public static EcsEntity Init<T>(this EcsEntity entity, T data) where T : struct {
        entity.Get<T>() = data;
        return entity;
    }

    public static Transform GetTransform(this EcsEntity entity) =>
        entity.Has<Refefence_Transform>() ? entity.Get<Refefence_Transform>().transform : null;

    public static EcsSystems OneFramePhysics2D(this EcsSystems ecsSystems) =>
        ecsSystems.OneFrame<OnTriggerEnter2DEvent>()
                  .OneFrame<OnTriggerStay2DEvent>()
                  .OneFrame<OnTriggerExit2DEvent>()
                  .OneFrame<OnCollisionEnter2DEvent>()
                  .OneFrame<OnCollisionStay2DEvent>()
                  .OneFrame<OnCollisionExit2DEvent>()
                  .OneFrame<OnControllerColliderHitEvent>();
}
