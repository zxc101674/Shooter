using Leopotam.Ecs;
using UnityEngine;

sealed class System_Attack_Search : IEcsRunSystem {
    private EcsFilter<Attack, Search> _filter;

    void IEcsRunSystem.Run() {
        foreach (var index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var attack = ref _filter.Get1(index);
            ref var search = ref _filter.Get2(index);
            ref var target = ref search.target;

            var transform = entity.GetTransform();
            var animator = attack.animator;

            if (attack.animator == null) Debug.Log(transform.name);
            
            var power = attack.power;
            int enemyDistance = 0;
            if (search.target != default) {
                var hits = Physics2D.RaycastAll(transform.position, search.target.GetTransform().position - transform.position, search.range.max);
                for (int i = 0; i < hits.Length; ++i) {
                    if (search.target.GetTransform() == hits[i].transform) {
                        enemyDistance = (int) hits[i].distance;
                    }
                }
                attack.targets = search.curTargets;
            }
            animator.SetInteger("EnemyDistance", enemyDistance);
        }
    }
}