using Leopotam.Ecs;
using UnityEngine;

sealed class System_Input_Move : IEcsRunSystem {
    private EcsFilter<Move, Tag_Input> _filter;
    private Data_Input input;

    void IEcsRunSystem.Run() {
        foreach(int index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var move = ref _filter.Get1(index);

            var transform = move.transform;
            var animator = move.animator;

            if (animator == null) Debug.Log(transform.name);
            
            transform.Translate(input.moveInput.normalized * move.speed.current * Time.fixedDeltaTime, Space.World);
            animator.SetInteger("MoveSpeed", (int) input.moveInput.normalized.magnitude);
        }
    }
}