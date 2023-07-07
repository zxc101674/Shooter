using Leopotam.Ecs;
using UnityEngine;

public static class SearchHelper {
    public delegate float GetNewValue<T>(T component) where T : struct;

    public static bool Search(this ref Search search, Vector2 center, Data_EntityObjects data) {
        if (search.oldTargets == null) search.oldTargets = new();

        if (search.curTargets != null && search.curTargets.Count > 0) {
            search.oldTargets.Clear();
            for (int i = 0; i < search.curTargets.Count; ++i) {
                search.oldTargets.Add(search.curTargets[i]);
            }
        }

        search.curTargets ??= new();
        search.curTargets.Clear();

        bool res = search.type switch {
            SearchType.All => search.SearchAll(center, data),
            SearchType.ByPriority => search.SearchByPriority(center, data),
            SearchType.Nearest => search.SearchByValue(center, true, data, (Refefence_Transform transform) => center.Distance(transform)),
            SearchType.Farest => search.SearchByValue(center, false, data, (Refefence_Transform transform) => center.Distance(transform)),
            SearchType.MinHP => search.SearchByValue(center, true, data, (Health health) => health.health.current),
            SearchType.MaxHP => search.SearchByValue(center, false, data, (Health health) => health.health.current),
            _ => false
        };

        search.target = search.curTargets != null && search.curTargets.Count > 0 ? search.curTargets[0] : default;

        bool isChanged = false;
        if (search.OnEnterTarget != null) {
            for (int i = 0; i < search.curTargets.Count; ++i) {
                if (!search.oldTargets.Contains(search.curTargets[i])) {
                    isChanged = true;
                    break;
                }
            }
            if (isChanged) {
                for (int i = 0; i < search.curTargets.Count; ++i) {
                    if (!search.oldTargets.Contains(search.curTargets[i])) {
                        search.OnEnterTarget(search.curTargets[i]);
                    }
                }
            }
        }

        isChanged = false;
        if(search.OnExitTarget != null) {
            for (int i = 0; i < search.oldTargets.Count; ++i) {
                if (!search.curTargets.Contains(search.oldTargets[i])) {
                    isChanged = true;
                    break;
                }
            }
            if (isChanged) {
                for (int i = 0; i < search.oldTargets.Count; ++i) {
                    if (!search.curTargets.Contains(search.oldTargets[i])) {
                        search.OnExitTarget(search.oldTargets[i]);
                        search.oldTargets.RemoveAt(i);
                    }
                }
            }
        }

        return res;
    }

    private static bool SearchAll(this ref Search search, Vector2 center, Data_EntityObjects data) {
        data.FindInRange(center, search.range).FindAll(center, data, ref search);
        return search.curTargets.Count > 0;
    }

    private static bool SearchByPriority(this ref Search search, Vector2 center, Data_EntityObjects data) {
        data.FindInRange(center, search.range).FindByPriority(center, data, ref search);
        return search.curTargets.Count > 0;
    }

    private static bool SearchByValue<T>(this ref Search search, Vector2 center, bool isMin, Data_EntityObjects data, GetNewValue<T> getNewValue) where T : struct {
        search.SearchByPriority(center, data);
        return search.Find(isMin, getNewValue);
    }

    private static bool Find<T>(this ref Search search, bool isMin, GetNewValue<T> GetNewValue) where T : struct {
        if (search.curTargets == null) return false;
        EcsEntity res = default;
        float value = 0;
        float newValue;
        for (int i = 0; i < search.curTargets.Count; ++i) {
            if (!search.curTargets[i].Has<T>()) continue;
            newValue = GetNewValue(search.curTargets[i].Get<T>());
            if (value == 0 || (isMin && newValue < value) || (!isMin && newValue > value)) {
                value = newValue;
                res = search.curTargets[i];
            }
        }
        search.curTargets.Clear();
        if(res != default) search.curTargets.Add(res);
        return search.curTargets.Count != 0;
    }
}
