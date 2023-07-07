using Leopotam.Ecs;

sealed class System_Object_Activate : System_BaseActivate, IEcsRunSystem {
    private EcsFilter<Refefence_Transform, Event_Object_Activate> _filter;
    private Data_EntityObjects _data;

    void IEcsRunSystem.Run() {
        foreach (int index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var transform = ref _filter.Get1(index).transform;
            _data.Add(entity, transform);
            entity.Del<Event_Object_Activate>();
        }
    }
}