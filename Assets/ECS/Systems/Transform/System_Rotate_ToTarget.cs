using Leopotam.Ecs;
using UnityEngine;

sealed class System_Rotate_ToTarget : IEcsRunSystem {
    EcsFilter<Search, Rotate> _filter;
    void IEcsRunSystem.Run() {
        foreach(var index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var search = ref _filter.Get1(index);
            ref var rotate = ref _filter.Get2(index);

            var transform = rotate.transform;

            if (search.target != default) {
                Vector2 direction = search.target.GetTransform().position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction.normalized);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotate.speed.current * Time.fixedDeltaTime);
            }
        }
    }
}