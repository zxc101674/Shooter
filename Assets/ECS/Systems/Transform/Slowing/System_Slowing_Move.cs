using Leopotam.Ecs;
using UnityEngine;

sealed class System_Slowing_Move : IEcsRunSystem {
    private EcsFilter<Move> _filter;

    void IEcsRunSystem.Run() {
        foreach (int index in _filter) {
            ref var move = ref _filter.Get1(index);
            ref var slowing = ref move.slowing;
            ref var speed = ref move.speed;

            float time = slowing.curve.keys[slowing.curve.keys.Length - 1].time;

            if (slowing.curTime < time) {
                slowing.curTime = Mathf.Clamp(slowing.curTime + .1f, 0, time);
                speed.current = Mathf.Lerp(speed.startToChange, move.SpeedTarget, slowing.curve.Evaluate(slowing.curTime));
            }
        }
    }
}