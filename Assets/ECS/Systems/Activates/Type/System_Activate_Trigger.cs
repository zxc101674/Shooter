using Leopotam.Ecs;
using UnityEngine;

sealed class System_Activate_Trigger : System_BaseActivate, IEcsRunSystem {
    private EcsFilter<Activate_Trigger> _filter;

    void IEcsRunSystem.Run() {
        foreach (int index in _filter) {
            ref var trigger = ref _filter.Get1(index);
            
            if (trigger.mask.value == 0) continue;

            if (trigger.waitInSeconds.isStarted &&
                trigger.timer > trigger.waitInSeconds.delay) {
                if((trigger.triggerMask & trigger.mask) != 0) {
                    Activate(trigger);
                    trigger.triggerMask.value = 0;
                    trigger.timer = 0;
                }
            } else if (trigger.timer > trigger.waitInSeconds.start) {
                trigger.waitInSeconds.isStarted = true;
                Activate(trigger);
            }

            trigger.timer += Time.fixedDeltaTime;
        }
    }
}