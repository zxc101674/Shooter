using Leopotam.Ecs;
using UnityEngine;

sealed class System_Acceleration_Rotate : IEcsRunSystem {
    private EcsFilter<Rotate> _filter;

    void IEcsRunSystem.Run() {
        foreach (int index in _filter) {
            ref var rotate = ref _filter.Get1(index);
            ref var acceleration = ref rotate.acceleration;
            ref var speed = ref rotate.speed;

            float time = acceleration.curve.keys[acceleration.curve.keys.Length - 1].time;

            if (acceleration.curTime < time) {
                acceleration.curTime = Mathf.Clamp(acceleration.curTime + .1f, 0, time);
                speed.current = Mathf.Lerp(speed.startToChange, rotate.SpeedTarget, acceleration.curve.Evaluate(acceleration.curTime));
            }
        }
    }
}