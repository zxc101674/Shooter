using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using System;

[System.Serializable]
public struct Search {
    public Range range;
    public SearchType type;
    public LayerMask obstacles;
    public List<Priority> priorities;

    public List<EcsEntity> oldTargets;
    public List<EcsEntity> curTargets;

    public EcsEntity target;

    public Action<EcsEntity> OnEnterTarget;
    public Action<EcsEntity> OnExitTarget;

    public void AddOnEnter(Action<EcsEntity> action) => OnEnterTarget = action;
    public void AddOnExit(Action<EcsEntity> action) => OnExitTarget = action;
    public void RemoveOnEnter(Action<EcsEntity> action) => OnEnterTarget = action;
    public void RemoveOnExit(Action<EcsEntity> action) => OnExitTarget = action;
}
public class MonoSearch : BaseMonoComponent<Search> { }