using Leopotam.Ecs;
using UnityEngine;

sealed class System_Patrol : IEcsRunSystem {
    private EcsFilter<Search, Patrol, Attack, Move, Rotate> _filter;

    void IEcsRunSystem.Run() {
        foreach (int index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var search = ref _filter.Get1(index);
            ref var patrol = ref _filter.Get2(index);
            ref var attack = ref _filter.Get3(index);
            ref var move = ref _filter.Get4(index);
            ref var rotate = ref _filter.Get5(index);

            var modelTransform = rotate.transform;

            var transform = move.transform;
            var animator = move.animator;

            Vector2 direction;
            if (search.target != default) {
                if(patrol.stopDistanceToTarget < modelTransform.Distance(search.target)) {
                    // Move to target
                    animator.SetBool("IsMove", true);
                    direction = search.target.GetTransform().position - modelTransform.position;
                } else {
                    // Attack
                    animator.SetBool("IsMove", false);
                    continue;
                }
            } else {
                // Move by path
                animator.SetBool("IsMove", true);
                direction = patrol.PathPoint - (Vector2) modelTransform.position;
                if (direction.magnitude <= patrol.minDistanceToNextPath) patrol.NextPathPoint();
            }

            if (direction == Vector2.zero) continue;

            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
            modelTransform.rotation = Quaternion.RotateTowards(modelTransform.rotation, rotation, rotate.speed.current * Time.fixedDeltaTime);

            transform.Translate(direction.magnitude > patrol.minDistanceToNextPath ?
                             direction.normalized * move.speed.current * Time.fixedDeltaTime : Vector2.zero, Space.World);
        }
    }
}