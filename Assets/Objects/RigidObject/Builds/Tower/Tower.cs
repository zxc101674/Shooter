using Leopotam.Ecs;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class Tower : Build {
    [SerializeField] private Health health;
    [SerializeField] private Rotate rotate;
    [SerializeField] private Search search;
    [SerializeField] private Attack attack;
    [SerializeField] private LiveTimer liveTimer;

    private Animator _animator;
    private AudioSource _audio;
    private Collider2D _collider;

    private protected override void InitUnityComponents() {
        _animator ??= GetComponent<Animator>();
        _audio ??= GetComponent<AudioSource>();
        _collider ??= GetComponent<Collider2D>();
    }

    private protected override void InitEntityComponents() {
        attack.animator = _animator;
        rotate.transform = transform;
    }

    private protected override void InitEntityEvents() {
        health.OnDead = Destroy;
        liveTimer.OnDestroy = Destroy;
    }

    private protected override void AddComponents() {
        gameObject.AddComponent<MonoSearch>().Value = search;
        gameObject.AddComponent<MonoHealth>().Value = health;
        gameObject.AddComponent<Mono_Rotate>().Value = rotate;
        gameObject.AddComponent<Mono_Attack>().Value = attack;
        gameObject.AddComponent<MonoLiveTimer>().Value = liveTimer;
    }

    public override void Activate() {
        TryGetEntity()?.Init(search);
        TryGetEntity()?.Init(health);
        TryGetEntity()?.Init(rotate);
        TryGetEntity()?.Init(attack);
        TryGetEntity()?.Init(liveTimer);
    }

    public override void Deactivate() {
        TryGetEntity()?.Del<Search>();
        TryGetEntity()?.Del<Health>();
        TryGetEntity()?.Del<Rotate>();
        TryGetEntity()?.Del<Attack>();
        TryGetEntity()?.Del<LiveTimer>();
    }

    private void AnimAttack() => TryGetEntity()?.Init<Event_Attack>();
    private void PlayAudio(AudioClip clip) => _audio.PlayOneShot(clip);

    private void Destroy() {
        _collider.enabled = false;
        _animator.Play("Destroy");
        OnAnimDead();
        OnDead();
    }

    private void DestroyGO() {
        Destroy(gameObject);
    }
}
