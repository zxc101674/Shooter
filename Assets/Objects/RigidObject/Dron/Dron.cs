using Leopotam.Ecs;
using UnityEngine;

public class Dron : BaseEntityObject {
    [SerializeField] private Search search;
    [SerializeField] private Follow follow;
    [SerializeField] private Rotate rotate;
    [SerializeField] private Fix fix;
    [SerializeField] private LiveTimer liveTime;

    private Animator _animator;
    private AudioSource _audio;

    private protected override void InitUnityComponents() {
        _animator ??= GetComponent<Animator>();
        _audio ??= GetComponent<AudioSource>();
    }

    private protected override void InitEntityComponents() {
        rotate.transform = transform;
    }

    private protected override void InitEntityEvents() {
        liveTime.OnDestroy = () => _animator.SetTrigger("Destroy");
    }

    private protected override void AddComponents() {
        gameObject.AddComponent<MonoSearch>().Value = search;
        gameObject.AddComponent<Mono_Follow>().Value = follow;
        gameObject.AddComponent<Mono_Rotate>().Value = rotate;
        gameObject.AddComponent<MonoFix>().Value = fix;
        gameObject.AddComponent<MonoLiveTimer>().Value = liveTime;
    }

    public override void Activate() {
        TryGetEntity()?.Init(search);
        TryGetEntity()?.Init(follow);
        TryGetEntity()?.Init(rotate);
        TryGetEntity()?.Init(fix);
        TryGetEntity()?.Init(liveTime);
    }
    
    public override void Deactivate() {
        TryGetEntity()?.Del<Search>();
        TryGetEntity()?.Del<Follow>();
        TryGetEntity()?.Del<Rotate>();
        TryGetEntity()?.Del<Fix>();
        TryGetEntity()?.Del<LiveTimer>();
    }

    private void PlayAudio(AudioClip clip) => _audio.PlayOneShot(clip);


    private void Fix() {
        TryGetEntity()?.Init<Event_Fix>();
    }

    private void DestroyGO() {
        Destroy(gameObject);
    }
}
