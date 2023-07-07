using UnityEngine;
using System;

[RequireComponent(typeof(Animator))]
public class Foundation : BaseEntityObject {
    [SerializeField] private Build buildPref;
    
    private Action OnAnimDead = delegate { };

    private Build build;
    private Animator animator;

    private protected override void AddComponents() { }

    public override void Activate() {
        build.enabled = true;
    }

    public override void Deactivate() {
        if(build != null) {
            build.enabled = false;
        }
    }

    public void AnimDead() {
        animator.Play("Destroy");
        OnAnimDead();
    }

    private void DestroyGO() {
        Destroy(gameObject);
    }

    private new void Awake() {
        animator ??= GetComponent<Animator>();
        if (buildPref == null) return;
        build = GetComponentInChildren<Build>();
        build ??= Instantiate(buildPref, transform);
        build.OnAnimDead = AnimDead;

        base.Awake();
    }
}
