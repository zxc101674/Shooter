using Leopotam.Ecs;
using UnityEngine;

sealed class System_Slowing_Rotate : IEcsRunSystem {
    private EcsFilter<Rotate> _filter;

    void IEcsRunSystem.Run() {
        foreach (int index in _filter) {
            ref var rotate = ref _filter.Get1(index);
            ref var slowing = ref rotate.slowing;
            ref var speed = ref rotate.speed;

            float time = slowing.curve.keys[slowing.curve.keys.Length - 1].time;

            if (slowing.curTime < time) {
                slowing.curTime = Mathf.Clamp(slowing.curTime + .1f, 0, time);
                speed.current = Mathf.Lerp(speed.startToChange, rotate.SpeedTarget, slowing.curve.Evaluate(slowing.curTime));
            }
        }
    }
}