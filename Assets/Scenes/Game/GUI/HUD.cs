using UnityEngine.Events;
using UnityEngine;
using System;
using TMPro;

public class HUD : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI countEnemiesText;
    [SerializeField] private TextMeshProUGUI FPSText;
    [SerializeField] private TextMeshProUGUI countEntitiesText;
    [SerializeField] private Dialog dialog;
    [SerializeField] private Animator animator;
    
    [Space]
    [SerializeField] private UnityEvent OnRespawnHero;

    private int countEnemies;

    public void AnimRespawnHero() {
        animator.Play("RespawnHero");
    }

    public void RespawnHero() {
        OnRespawnHero.Invoke();
    }

    public void ShowDialog(SO_Dialog so_dialog, Action OnActivate) {
        dialog.OnActivate += OnActivate;
        dialog.Show(so_dialog);
    }

    public void AddEnemy() {
        ++countEnemies;
        countEnemiesText.text = countEnemies.ToString();
    }

    public void RemoveEnemy() {
        --countEnemies;
        countEnemiesText.text = countEnemies.ToString();
    }

    public void PrintFPS(float fps) {
        FPSText.text = fps.ToString();
    }

    public void PrintCountEntities(int count) {
        countEntitiesText.text = count.ToString();
    }
}
