using Leopotam.Ecs;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(AudioSource))]
public class Trigger : BaseActivator {
    [Header("Trigger")]
    [SerializeField] private Activate_Trigger _activate;

    private AudioSource _audio;
    private protected override void InitUnityComponents() {
        _audio ??= GetComponent<AudioSource>();
    }

    private protected override void AddActivatorsComponents() {
        _activate.Dialog = dialog ?? null;
        _activate.OnActivate = () => OnActivate(this);
        gameObject.AddComponent<Mono_Activate_Trigger>().Value = _activate;
    }

    public override void Activate() {
        _activate.Dialog = dialog ?? null;
        _activate.OnActivate = () => OnActivate(this);
        TryGetEntity()?.Init(_activate);
    }

    public override void Deactivate() {
        _activate.OnActivate = delegate { };
        TryGetEntity()?.Del<Activate_Trigger>();
    }

    private void TryPlayAudio(EcsEntity? entity) {
        if (entity != null && entity.Value.Has<Tag_Input>()) {
            _audio.Play();
        }
    }

    private void TryStopAudio(EcsEntity? entity) {
        if (entity != null && entity.Value.Has<Tag_Input>()) {
            _audio.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (TryGetEntity().Value.Has<Activate_Trigger>()) {
            TryGetEntity().Value.Get<Activate_Trigger>().triggerMask |= (1 << collision.gameObject.layer);
            TryPlayAudio(collision.GetComponent<BaseEntityObject>()?.TryGetEntity());
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (TryGetEntity().Value.Has<Activate_Trigger>()) {
            TryGetEntity().Value.Get<Activate_Trigger>().triggerMask &= (1 << collision.gameObject.layer);
            TryStopAudio(collision.GetComponent<BaseEntityObject>()?.TryGetEntity());
        }
    }
}
