using Leopotam.Ecs;

sealed class System_Print_Count_Entities : IEcsRunSystem {
    private Data_EntityObjects data;
    private HUD hud;

    void IEcsRunSystem.Run() {
        hud.PrintCountEntities(data.Count);
    }
}