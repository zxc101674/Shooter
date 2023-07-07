using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class MonoClick : MonoBehaviour, IPointerClickHandler {
    public Action OnClick = delegate { };

    public void OnPointerClick(PointerEventData eventData) {
        OnClick();
    }
}
