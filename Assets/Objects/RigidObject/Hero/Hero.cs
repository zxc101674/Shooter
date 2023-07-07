using UnityEngine.Events;
using UnityEngine.UI;
using Leopotam.Ecs;
using UnityEngine;

public class Hero : BaseEntityObject {
    [SerializeField] private MonoTrigger swordTrigger;
    [SerializeField] private HeroAnimation model;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;

    [SerializeField] private Search search;
    [SerializeField] private Health health;
    [SerializeField] private Move move;
    [SerializeField] private Rotate rotate;
    [SerializeField] private Attack attack;

    [HideInInspector] public Vector2 respawnPosition;

    [Space]
    [SerializeField] private UnityEvent OnStartDead;

    private protected override void InitEntityComponents() {
        rotate.transform = model.transform;
        move.transform = transform;
        move.animator = model.animator;
        attack.animator = model.animator;
    }

    private protected override void InitEntityEvents() {
        model.OnAttack = () => TryGetEntity()?.Init<Event_Attack>();
        model.OnDead = OnStartDead.Invoke;

        swordTrigger.OnEnter = (EcsEntity entity) => TryGetEntity()?.Init<Event_Attack>();

        health.OnDead = () => {
            model.animator.Play("Death");
            GetComponent<Collider>().enabled = false;
            Deactivate();
        };

        health.OnUpdateShield = (CorrectValue value) => shieldBar.fillAmount = value.current / value.max;
        health.OnUpdateHealth = (CorrectValue value) => healthBar.fillAmount = value.current / value.max;
    }

    private protected override void AddComponents() {
        gameObject.AddComponent<Mono_Tag_Hero>();
        gameObject.AddComponent<Mono_Tag_Input>();
        gameObject.AddComponent<MonoSearch>().Value = search;
        gameObject.AddComponent<MonoHealth>().Value = health;
        gameObject.AddComponent<Mono_Move>().Value = move;
        gameObject.AddComponent<Mono_Rotate>().Value = rotate;
        gameObject.AddComponent<Mono_Attack>().Value = attack;
    }

    public override void Activate() {
        TryGetEntity()?.Init<Tag_Input>();
        TryGetEntity()?.Init(search);
        TryGetEntity()?.Init(health);
        TryGetEntity()?.Init(move);
        TryGetEntity()?.Init(rotate);
        TryGetEntity()?.Init(attack);
    }

    public override void Deactivate() {
        if(gameObject.activeSelf == true) {
            model.animator.Play("Idle");
        }
        TryGetEntity()?.Del<Tag_Input>();
        TryGetEntity()?.Del<Search>();
        TryGetEntity()?.Del<Health>();
        TryGetEntity()?.Del<Move>();
        TryGetEntity()?.Del<Rotate>();
        TryGetEntity()?.Del<Attack>();
    }

    public void Respawn() {
        Activate();
        transform.position = respawnPosition;
        model.animator.Play("Idle");
        GetComponent<Collider>().enabled = true;
        Debug.Log(health.health.max > 0 ? 1 : 0);
        healthBar.fillAmount = health.health.max > 0 ? 1 : 0;
        shieldBar.fillAmount = health.shield.max > 0 ? 1 : 0;
    }
}
