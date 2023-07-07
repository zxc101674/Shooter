using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class Mine : Build {
    [SerializeField] private MonoTrigger trigger;
    [SerializeField] private Search search;
    [SerializeField] private Attack explosion;
    [SerializeField] private LiveTimer liveTimer;

    private Animator _animator;
    private AudioSource _audio;

    private protected override void InitUnityComponents() {
        _animator ??= GetComponent<Animator>();
        _audio ??= GetComponent<AudioSource>();
    }

    private protected override void InitEntityComponents() {
        explosion.animator = _animator;
    }

    private protected override void InitEntityEvents() {
        trigger.OnEnter = Explosion;
        liveTimer.OnDestroy = PlayExplosionAnim;
    }

    private protected override void AddComponents() {
        gameObject.AddComponent<MonoSearch>().Value = search;
        gameObject.AddComponent<Mono_Attack>().Value = explosion;
        gameObject.AddComponent<MonoLiveTimer>().Value = liveTimer;
    }

    public override void Activate() {
        TryGetEntity()?.Init(search);
        TryGetEntity()?.Init(explosion);
        TryGetEntity()?.Init(liveTimer);
    }

    public override void Deactivate() {
        TryGetEntity()?.Del<Search>();
        TryGetEntity()?.Del<Attack>();
        TryGetEntity()?.Del<LiveTimer>();
    }

    private void Explosion(EcsEntity entity) {
        TryGetEntity()?.Init<Event_Attack>();
        PlayExplosionAnim();
    }

    private void PlayExplosionAnim() => _animator.Play("Explosion");
    private void PlayAudio(AudioClip clip) => _audio.PlayOneShot(clip);

    private void DestroyGO() {
        Destroy(gameObject);
    }
}
