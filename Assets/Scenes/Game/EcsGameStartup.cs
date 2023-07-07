using LeoEcsPhysics;
using Leopotam.Ecs;
using System.Collections;
using UnityEngine;
using Voody.UniLeo;

sealed class EcsGameStartup : MonoBehaviour {
    [SerializeField] private int maxFPS = 60;
    [SerializeField] private float coroutineDeltaTime = .1f;
    [SerializeField] private HUD _hud;
    [SerializeField] private Runtime_Hero _hero;
    
    [Space]
    [Header("Skills")]
    [SerializeField] private Activate_Dron _activateDron;
    [SerializeField] private Spawn_Mine _spawnMine;
    [SerializeField] private Spawn_Tower _spawnTower;

    private EcsWorld _world;
    private EcsSystems _systems;
    private EcsSystems _fixedSystems;
    private EcsSystems _coroutineSystems;
    private Data_EntityObjects _entityObjectsData;
    private Data_Input _input;
    private Data_Items _items;

    private void Start() {
        Application.targetFrameRate = maxFPS;

        _world = new();
        _systems = new(_world);
        _fixedSystems = new(_world);
        _coroutineSystems = new(_world);
        _entityObjectsData = new();
        _input = new();
        _items = new();
        
        EcsPhysicsEvents.ecsWorld = _world;

#if UNITY_EDITOR
        Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedSystems);
        Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_coroutineSystems);
#endif

        _systems
            .ConvertScene()
            .Inject(_input)
            .Inject(_hud)
            .Inject(_entityObjectsData)
            .Inject(_activateDron)
            .Inject(_spawnMine)
            .Inject(_spawnTower)
            .Inject(_hero)
            .Inject(_items)
            .Add(new System_Object_Activate())
            .Add(new System_Object_Deactivate())
            .Add(new System_Input_Hero_Move())
            .Add(new System_Input_Spawn_Mine())
            .Add(new System_Input_Spawn_Tower())
            .Add(new System_Input_Activate_Dron())
            .Add(new System_Activate_Item())
            .Add(new System_Search())
            .Add(new System_Attack_Search())
            .Add(new System_Attack())
            .Add(new System_Acceleration_Move())
            .Add(new System_Acceleration_Rotate())
            .Add(new System_Slowing_Move())
            .Add(new System_Slowing_Rotate())
            .Add(new System_GetDamage())
            .Add(new System_Follow())
            .Init();

        _fixedSystems
            .Inject(_input)
            .Inject(_hud)
            .Inject(_hero)
            .Add(new System_Input_Move())
            .Add(new System_Rotate_ToTarget())
            .Add(new System_Input_Rotate_ToTarget())
            .Add(new System_Activate_ByEvent())
            .Add(new System_Activate_Trigger())
            .Add(new System_Activate_Spawn())
            .Add(new System_Fix())
            .Add(new System_Patrol())
            .Add(new System_LiveTimer())
            //.Add(new SearchItemSystem())
            //.Add(new ShowDialogSystem())
            //.Add(new HideSystem())
            //.Add(new FollowSystem())
            //.OneFramePhysics2D()
            .Init();

        _coroutineSystems
            .Inject(_hud)
            .Inject(_entityObjectsData)
            .Add(new System_Print_FPS())
            .Add(new System_Print_Count_Entities())
            .Init();

        StartCoroutine(Enumerator());
    }

    private void Update() {
        _systems?.Run();
    }

    private void FixedUpdate() {
        _fixedSystems?.Run();
    }

    private IEnumerator Enumerator() {
        while (true) {
            _coroutineSystems.Run();
            yield return new WaitForSeconds(coroutineDeltaTime);
        }
    }

    private void OnDestroy() {
        if (_systems != null) {
            _systems.Destroy();
            _systems = null;
        }
        if (_fixedSystems != null) {
            _fixedSystems.Destroy();
            _fixedSystems = null;
        }
        if (_coroutineSystems != null) {
            _coroutineSystems.Destroy();
            _coroutineSystems = null;
        }
        if (_world != null) {
            EcsPhysicsEvents.ecsWorld = null;
            _world.Destroy();
            _world = null;
        }
        if(_entityObjectsData != null) {
            _entityObjectsData.Destroy();
            _entityObjectsData = null;
        }
    }
}