using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LeoECS/Dialog", fileName = "Dialog")]
public class SO_Dialog : ScriptableObject {
    public bool isStopGame;
    public List<DialogElement> list;
}
