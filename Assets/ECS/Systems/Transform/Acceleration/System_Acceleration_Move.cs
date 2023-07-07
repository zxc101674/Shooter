using Leopotam.Ecs;
using UnityEngine;

sealed class System_Acceleration_Move : IEcsRunSystem {
    private EcsFilter<Move> _filter;

    void IEcsRunSystem.Run() {
        foreach(int index in _filter) {
            ref var move = ref _filter.Get1(index);
            ref var acceleration = ref move.acceleration;
            ref var speed = ref move.speed;

            float time = acceleration.curve.keys[acceleration.curve.keys.Length - 1].time;
            
            if (acceleration.curTime < time) {
                acceleration.curTime = Mathf.Clamp(acceleration.curTime + .1f, 0, time);
                speed.current = Mathf.Lerp(speed.startToChange, move.SpeedTarget, acceleration.curve.Evaluate(acceleration.curTime));
            }
        }
    }
}