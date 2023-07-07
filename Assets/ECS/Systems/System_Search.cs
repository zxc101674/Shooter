using Leopotam.Ecs;
using UnityEngine;

sealed class System_Search : IEcsRunSystem {
    private EcsFilter<Search> _filter;
    private Data_EntityObjects _data;

    void IEcsRunSystem.Run() {
        foreach (int index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var search = ref _filter.Get1(index);
            search.Search(entity.GetTransform().position, _data);
        }
    }
}