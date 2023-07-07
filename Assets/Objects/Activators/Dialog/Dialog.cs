using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class Dialog : MonoBehaviour, IPointerClickHandler {
    [Header("Dialog")]
    [SerializeField] private Image background;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI text;

    public Action OnActivate = delegate { };

    private List<DialogElement> dialog;

    public void Show(SO_Dialog so_dialog) {
        if (so_dialog.list.IsEmpty()) {
            OnActivate();
            return;
        }
        dialog ??= new();
        so_dialog.list.Copy(out dialog);

        background.enabled = true;
        text.enabled = true;

        Time.timeScale = so_dialog.isStopGame ? 0 : 1;

        NextStep();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(background.enabled == false) return;
        NextStep();
    }

    public void NextStep() {
        if (dialog.Count != 0) {
            Print();
        } else {
            Hide();
        }
    }

    private void Print() {
        icon.enabled = dialog[0].sprite;
        if (dialog[0].sprite) icon.sprite = dialog[0].sprite;
        text.text = dialog[0].text;
        dialog.RemoveAt(0);
    }

    private void Hide() {
        background.enabled = false;
        icon.enabled = false;
        text.enabled = false;
        OnActivate?.Invoke();
        OnActivate = delegate { };
        Time.timeScale = 1;
    }
}
