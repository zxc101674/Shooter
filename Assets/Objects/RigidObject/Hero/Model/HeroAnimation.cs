using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]
public class HeroAnimation : MonoBehaviour {
    public Action OnAttack = delegate { };
    public Action OnDead = delegate { };
    
    [HideInInspector] public Animator animator;
    [HideInInspector] public new AudioSource audio;
    private void Attack() => OnAttack();
    private void Dead() => OnDead();
    private void PlayAudio(AudioClip clip) {
        // audio.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        audio.clip = clip;
        if (!audio.isPlaying) {
            audio.Play();
        }
        // audio.PlayOneShot(clip);
    }
    private void StopAudio() {
        audio.clip = null;
        audio.Stop();
    }

    private void Awake() {
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }
}
