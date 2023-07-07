using Leopotam.Ecs;

sealed class System_Activate_ByEvent : System_BaseActivate, IEcsRunSystem {
    private EcsFilter<Event_Activate> _filter;

    void IEcsRunSystem.Run() {
        foreach (int index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var group = ref _filter.Get1(index);

            Activate(group);
            
            entity.Del<Event_Activate>();
        }
    }
}