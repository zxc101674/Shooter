using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class AnimActivator : BaseActivator {
    private Event_Activate _activate;

    private Animator _animator;
    private AudioSource _audio;

    private protected override void InitUnityComponents() {
        _animator ??= GetComponent<Animator>();
        _audio ??= GetComponent<AudioSource>();
    }

    public override void Activate() {
        _animator.SetTrigger("Activate");
    }

    public void PlayAudio(AudioClip clip) {
        _audio.PlayOneShot(clip);
    }

    public void EndAnimation() {
        _activate.Dialog = dialog ?? null;
        _activate.OnActivate = () => OnActivate(this);
        TryGetEntity()?.Init(_activate);
    }
}
