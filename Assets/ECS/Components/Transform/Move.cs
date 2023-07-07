using UnityEngine;

[System.Serializable]
public struct Move {
    public Acceleration acceleration;
    public Acceleration slowing;
    public Speed speed;

    [HideInInspector] public Animator animator;
    [HideInInspector] public Transform transform;

    public float SpeedTarget => speed.value * (1 + acceleration.value - slowing.value);

    public void AddSlowing(float value) {
        slowing.AddValue(value);
        speed.SaveSpeed();
    }

    public void ReduceSlowing(float value) {
        slowing.ReduceValue(value);
        speed.SaveSpeed();
    }

    public void AddAcceleration(float value) {
        acceleration.AddValue(value);
        speed.SaveSpeed();
    }

    public void ReduceAcceleration(float value) {
        acceleration.ReduceValue(value);
        speed.SaveSpeed();
    }
}
public class Mono_Move : BaseMonoComponent<Move> { }