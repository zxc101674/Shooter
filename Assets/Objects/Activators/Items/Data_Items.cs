using Leopotam.Ecs;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Data_Items {
    public List<EcsEntity> activeItems = new();

    public int Count => activeItems.Count;
    public EcsEntity Get(int index) => activeItems[index];
    public bool Has(EcsEntity entity) => activeItems.Contains(entity);
    public void Add(EcsEntity entity) => activeItems.Add(entity);
    public void Remove(EcsEntity entity) => activeItems.Remove(entity);
    public void RemoveAt(int index) => activeItems.RemoveAt(index);
    public void Clear() => activeItems.Clear();
}
