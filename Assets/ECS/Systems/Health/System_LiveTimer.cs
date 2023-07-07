using Leopotam.Ecs;
using UnityEngine;

sealed class System_LiveTimer : IEcsRunSystem {
    private EcsFilter<LiveTimer> _filter;
    void IEcsRunSystem.Run() {
        foreach (int index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var destroy = ref _filter.Get1(index);

            if (destroy.timer < destroy.time) {
                destroy.timer += Time.fixedDeltaTime;
            } else {
                destroy.OnDestroy();
            }
        }
    }
}