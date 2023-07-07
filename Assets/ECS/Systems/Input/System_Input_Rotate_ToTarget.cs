using Leopotam.Ecs;
using UnityEngine;

sealed class System_Input_Rotate_ToTarget : IEcsRunSystem {
    private EcsFilter<Search, Rotate, Tag_Input> _filter;
    private Data_Input _input;

    void IEcsRunSystem.Run() {
        foreach (var index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var search = ref _filter.Get1(index);
            ref var rotate = ref _filter.Get2(index);

            var transform = rotate.transform;

            Vector2 direction;

            if (search.target != default) {
                var targetTransform = search.target.GetTransform();
                direction = targetTransform.position - transform.position;
            } else {
                direction = _input.moveInput * Vector2.one;
            }

            if (direction == Vector2.zero) continue;

            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotate.speed.current * Time.fixedDeltaTime);
        }
    }
}