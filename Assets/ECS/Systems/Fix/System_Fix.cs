using Leopotam.Ecs;
using UnityEngine;

sealed class System_Fix : IEcsRunSystem {
    private EcsFilter<Search, Fix, Event_Fix> _filter;

    void IEcsRunSystem.Run() {
        foreach (var index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var search = ref _filter.Get1(index);
            ref var fix = ref _filter.Get2(index);

            for (int i = 0; i < search.curTargets.Count; ++i)
                    if (search.curTargets[i].Has<Health>())
                        Fix(ref search.curTargets[i].Get<Health>(), fix.power);

            entity.Del<Event_Fix>();

            //if (fix.interval <= 0) continue;

            //if (fix.timer < fix.interval) {
            //    fix.timer += Time.fixedDeltaTime;
            //} else {
            //    fix.timer = 0;
            //    for (int i = 0; i < search.curTargets.Count; ++i)
            //        if (search.curTargets[i].Has<Health>())
            //            Fix(ref search.curTargets[i].Get<Health>(), fix.power);
            //}
        }
    }

    private void Fix(ref Health component, float regen) {
        if (regen == 0 || component.shield.current == component.shield.max) return;
        
        if (component.shield.max - component.shield.current < regen)
            component.shield.current = component.shield.max;
        else
            component.shield.current += regen;

        component.OnUpdateShield(component.shield);
    }
}