using Leopotam.Ecs;

sealed class System_Attack : IEcsRunSystem {
    private EcsFilter<Attack, Event_Attack> _filter;
    void IEcsRunSystem.Run() {
        foreach (int index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var attack = ref _filter.Get1(index);
            
            entity.Del<Event_Attack>();

            if (attack.targets == null) continue;
            //Debug.Log(search.curTargets.Count);
            for (int i = 0; i < attack.targets.Count; ++i) {
                attack.targets[i].Init(new Event_GetDamage { damage = attack.power });
            }
        }
    }
}