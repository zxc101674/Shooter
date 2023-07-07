using Leopotam.Ecs;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class Enemy : BaseEntityObject {
    [SerializeField] private MonoTrigger clawTrigger;

    [SerializeField] private Search search;
    [SerializeField] private Health health;
    [SerializeField] private Move move;
    [SerializeField] private Rotate rotate;
    [SerializeField] private Attack attack;
    [SerializeField] private Patrol patrol;

    private Animator _animator;
    private AudioSource _audio;
    private Collider2D _collider;

    private protected override void InitUnityComponents() {
        _animator ??= GetComponent<Animator>();
        _audio ??= GetComponent<AudioSource>();
        _collider ??= GetComponent<Collider2D>();
    }

    private protected override void InitEntityComponents() {
        rotate.transform = transform;
        move.transform = transform;
        move.animator = _animator;
        attack.animator = _animator;
    }

    private protected override void InitEntityEvents() {
        clawTrigger.OnEnter = (EcsEntity entity) => AnimAttack();
        health.OnDead = AnimDead;
    }

    private protected override void AddComponents() {
        gameObject.AddComponent<Mono_Tag_Enemy>();
        gameObject.AddComponent<MonoSearch>().Value = search;
        gameObject.AddComponent<MonoHealth>().Value = health;
        gameObject.AddComponent<Mono_Move>().Value = move;
        gameObject.AddComponent<Mono_Rotate>().Value = rotate;
        gameObject.AddComponent<Mono_Attack>().Value = attack;
        gameObject.AddComponent<Mono_Patrol>().Value = patrol;
    }

    public override void Activate() {
        TryGetEntity()?.Init(search);
        TryGetEntity()?.Init(health);
        TryGetEntity()?.Init(move);
        TryGetEntity()?.Init(rotate);
        TryGetEntity()?.Init(attack);
        TryGetEntity()?.Init(patrol);
    }

    public override void Deactivate() {
        TryGetEntity()?.Del<Search>();
        TryGetEntity()?.Del<Health>();
        TryGetEntity()?.Del<Move>();
        TryGetEntity()?.Del<Rotate>();
        TryGetEntity()?.Del<Attack>();
        TryGetEntity()?.Del<Patrol>();
    }

    private void AnimDead() {
        _animator.Play("Death");
        _collider.enabled = false;
        enabled = false;
    }

    private void PlayAudio(AudioClip clip) => _audio.PlayOneShot(clip);

    private void DestroyGO() {
        Destroy(gameObject);
    }

    private void AnimAttack() => TryGetEntity()?.Init<Event_Attack>();
}
