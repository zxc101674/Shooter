using Leopotam.Ecs;
using UnityEngine;

sealed class System_Follow : IEcsRunSystem {
    private EcsFilter<Refefence_Transform, Follow> _filter;
    private Runtime_Hero _hero;
    void IEcsRunSystem.Run() {
        foreach (int index in _filter) {
            ref var transform = ref _filter.Get1(index).transform;
            ref var follow = ref _filter.Get2(index);

            Vector3 curPosition = transform.position;
            curPosition = Vector2.SmoothDamp(
                curPosition,
                (((Vector2) transform.position).Distance((Vector2) _hero.transform.position) > follow.stopDistance ?
                (Vector2) _hero.transform.position : transform.position) + follow.offset,
                ref follow.curVelocity,
                follow.smooth);
            curPosition.z = transform.position.z;
            transform.position = curPosition;
        }
    }
}