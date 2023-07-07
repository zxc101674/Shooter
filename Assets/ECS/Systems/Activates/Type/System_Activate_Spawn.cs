using Leopotam.Ecs;
using UnityEngine;

sealed class System_Activate_Spawn : System_BaseActivate, IEcsRunSystem {
    private EcsFilter<Activate_Spawn> _filter;

    void IEcsRunSystem.Run() {
        foreach(int index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var spawn = ref _filter.Get1(index);
            
            var transform = entity.GetTransform();

            if (spawn.count == spawn.maxCount) {
                continue;
            }

            if (spawn.timer <= 0) {
                spawn.timer = spawn.interval;
                ++spawn.count;

                Vector2 position = spawn.position +
                                        (spawn.space == Space.Self ?
                                            transform.position : Vector2.zero);
                BaseEntityObject obj = Object.Instantiate(spawn.prefab, position, Quaternion.identity).GetComponent<BaseEntityObject>();
                obj.OnDead = () => {
                    if (transform && transform.GetComponent<BaseEntityObject>() && transform.GetComponent<BaseEntityObject>()?.TryGetEntity() != null) {
                        EcsEntity e = (transform.GetComponent<BaseEntityObject>()?.TryGetEntity()).Value;
                        if (e.Has<Activate_Spawn>()) {
                            ref Activate_Spawn spawn = ref e.Get<Activate_Spawn>();
                            ++spawn.countDead;
                            if (spawn.isInfinity) {
                                --spawn.count;
                            }
                            if(!spawn.isInfinity && spawn.countDead == spawn.maxCount) {
                                Activate(spawn);
                                e.Del<Activate_Spawn>();
                            }
                        }
                    }
                };
                if (spawn.prefab.GetComponent<Enemy>()) _hud.AddEnemy();
            } else spawn.timer -= Time.fixedDeltaTime;
        }
    }
}