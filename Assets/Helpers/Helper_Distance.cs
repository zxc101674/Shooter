using Leopotam.Ecs;
using UnityEngine;

public static class Helper_Distance {
    public static float Distance(this EcsEntity from, EcsEntity to) => to.Has<Refefence_Transform>() ? from.Distance(to.Get<Refefence_Transform>()) : -1;
    public static float Distance(this EcsEntity from, Refefence_Transform to) => from.Distance(to.transform);
    public static float Distance(this EcsEntity from, Transform to) => from.Distance(to.position);
    public static float Distance(this EcsEntity from, Vector3 to) => from.Has<Refefence_Transform>() ? from.Get<Refefence_Transform>().Distance(to) : -1;
    public static float Distance(this EcsEntity from, Vector2 to) => from.Has<Refefence_Transform>() ? from.Get<Refefence_Transform>().Distance(to) : -1;

    public static float Distance(this Refefence_Transform from, EcsEntity to) => to.Has<Refefence_Transform>() ? from.Distance(to.Get<Refefence_Transform>()) : -1;
    public static float Distance(this Refefence_Transform from, Refefence_Transform to) => from.Distance(to.transform);
    public static float Distance(this Refefence_Transform from, Transform to) => from.Distance(to.position);
    public static float Distance(this Refefence_Transform from, Vector3 to) => from.transform.Distance(to);
    public static float Distance(this Refefence_Transform from, Vector2 to) => from.transform.Distance(to);

    public static float Distance(this Transform from, EcsEntity to) => to.Has<Refefence_Transform>() ? from.Distance(to.Get<Refefence_Transform>()) : -1;
    public static float Distance(this Transform from, Refefence_Transform to) => from.Distance(to.transform);
    public static float Distance(this Transform from, Transform to) => from.Distance(to.position);
    public static float Distance(this Transform from, Vector3 to) => from.position.Distance(to);
    public static float Distance(this Transform from, Vector2 to) => from.position.Distance(to);

    public static float Distance(this Vector3 from, EcsEntity to) => to.Has<Refefence_Transform>() ? from.Distance(to.Get<Refefence_Transform>()) : -1;
    public static float Distance(this Vector3 from, Refefence_Transform to) => from.Distance(to.transform);
    public static float Distance(this Vector3 from, Transform to) => from.Distance(to.position);
    public static float Distance(this Vector3 from, Vector3 to) => Vector3.Distance(from, to);
    public static float Distance(this Vector3 from, Vector2 to) => Vector3.Distance(from, to);

    public static float Distance(this Vector2 from, EcsEntity to) => to.Has<Refefence_Transform>() ? from.Distance(to.Get<Refefence_Transform>()) : -1;
    public static float Distance(this Vector2 from, Refefence_Transform to) => from.Distance(to.transform);
    public static float Distance(this Vector2 from, Transform to) => from.Distance(to.position);
    public static float Distance(this Vector2 from, Vector3 to) => Vector2.Distance(from, to);
    public static float Distance(this Vector2 from, Vector2 to) => Vector2.Distance(from, to);

    public static bool CheckDistance(this float distance, Vector2 between) => between.x <= distance && between.y >= distance;
}
