using Leopotam.Ecs;
using UnityEngine;

sealed class System_GetDamage : IEcsRunSystem {
    private EcsFilter<Health, Event_GetDamage> _filter;
    private HUD hud;

    void IEcsRunSystem.Run() {
        foreach(int index in _filter) {
            ref var entity = ref _filter.GetEntity(index);
            ref var health = ref _filter.Get1(index);
            ref var damage = ref _filter.Get2(index).damage;
            
            if (health.timerGetDamage < health.timeGetDamage) {
                if (health.timerGetDamage == 0) {
                    GetShieldDamage(ref health, damage);
                    if (health.health.current == 0) {
                        if(entity.Has<Tag_Enemy>()) hud.RemoveEnemy();
                        continue;
                    }
                    SpriteRenderer[] arr = entity.GetTransform().GetComponentsInChildren<SpriteRenderer>();
                    for (int i = 0; i < arr.Length; ++i) {
                        arr[i].color = health.colorToGetDamage;
                    }
                }
                health.timerGetDamage += .1f;
            } else if (health.timerGetDamage >= health.timeGetDamage) {
                SpriteRenderer[] arr = entity.GetTransform().GetComponentsInChildren<SpriteRenderer>();
                for (int i = 0; i < arr.Length; ++i) {
                    arr[i].color = Color.white;
                }
                health.timerGetDamage = 0;
                entity.Del<Event_GetDamage>();
            }
        }
    }

    private void GetShieldDamage(ref Health health, float damage) {
        if (damage == 0) return;
        if (health.shield.current == 0) {
            GetHealthDamage(ref health, damage);
            return;
        }

        if (health.shield.current < damage) {
            damage -= health.shield.current;
            health.shield.current = 0;
            GetHealthDamage(ref health, damage);
        } else {
            health.shield.current -= damage;
        }

        health.OnUpdateShield(health.shield);
    }

    private void GetHealthDamage(ref Health health, float damage) {
        if (health.health.current == 0) return;

        if (health.health.current < damage) {
            health.health.current = 0;
        } else {
            health.health.current -= damage;
        }

        if (health.health.current == 0) {
            health.OnDead();
        } else {
            health.OnUpdateHealth?.Invoke(health.health);
        }
    }
}