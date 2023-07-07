using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

public static class TransformHelper {
    public static void FindAll(this List<Transform> transforms, Vector2 center, Data_EntityObjects data, ref Search search) {
        search.curTargets = new List<EcsEntity>();
        for (int i = 0; i < transforms.Count; ++i) {
            if (Physics2D.Linecast(center, transforms[i].position, search.obstacles)) continue;

            for (int j = 0; j < search.priorities.Count; ++j) {
                if ((1 << transforms[i].gameObject.layer & search.priorities[j].layer) != 0) {
                    search.curTargets.Add(data.Get(transforms[i]));
                    break;
                }
            }
        }
    }

    public static void FindByPriority(this List<Transform> transforms, Vector2 center, Data_EntityObjects data, ref Search search) {
        int maxPriority = 0;
        search.curTargets = new List<EcsEntity>();
        for (int i = 0; i < transforms.Count; ++i) {
            if (Physics2D.Linecast(center, transforms[i].position, search.obstacles)) continue;

            for (int j = maxPriority; j < search.priorities.Count; ++j) {
                if ((1 << transforms[i].gameObject.layer & search.priorities[j].layer) == 0) continue;
                if (maxPriority < search.priorities[j].priority) {
                    maxPriority = j;
                    search.curTargets.Clear();
                    search.curTargets.Add(data.Get(transforms[i]));
                } else if (maxPriority == search.priorities[j].priority) {
                    search.curTargets.Add(data.Get(transforms[i]));
                }
            }
        }
    }
}
