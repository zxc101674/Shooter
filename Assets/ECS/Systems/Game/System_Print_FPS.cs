using Leopotam.Ecs;
using UnityEngine;

sealed class System_Print_FPS : IEcsRunSystem {
    private HUD hud;

    void IEcsRunSystem.Run() {
        float fps = 1f / Time.unscaledDeltaTime;
        hud.PrintFPS(Mathf.Round(fps));
    }
}