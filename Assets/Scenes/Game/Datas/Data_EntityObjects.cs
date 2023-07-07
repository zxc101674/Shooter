using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public class Data_EntityObjects {
    private Dictionary<Transform, EcsEntity> mapEntities = new();
    public int Count => mapEntities.Count;
    public EcsEntity Get(Transform transform) => mapEntities[transform];
    public void Add(EcsEntity entity, Transform transform) {
        if (mapEntities.ContainsKey(transform)) return;
        mapEntities[transform] = entity;
    }
    public void Remove(Transform transform) {
        if (!mapEntities.ContainsKey(transform)) return;
        mapEntities.Remove(transform);
    }
    public List<Transform> FindInRange(Vector2 center, Range range) {
        List<Transform> res = new();
        foreach (Transform transform in mapEntities.Keys) {
            if((Vector2) transform.position != center &&
               Vector2.Distance(center, transform.position) >= range.min &&
               Vector2.Distance(center, transform.position) <= range.max) {
                    res.Add(transform);
            }
        }
        return res;
    }
    public void Destroy() {
        mapEntities.Clear();
        mapEntities = null;
    }
}
