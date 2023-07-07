using UnityEngine.InputSystem;
using Leopotam.Ecs;
using UnityEngine;

sealed class System_Input_Spawn_Tower : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem {
    private Data_Input _data;
    private Spawn_Tower _tower;
    private Runtime_Hero _hero;
    private bool _isPressed;
    private float _timer;

    public void Init() {
        _data.input.Skills.Enable();
        _data.input.Skills.SpawnTower.performed += SpawnTower;
    }

    private void SpawnTower(InputAction.CallbackContext context) {
        if (!_isPressed) {
            Object.Instantiate(_tower.prefab, _hero.spawnPoints.tower.position, _hero.spawnPoints.tower.rotation);
            _isPressed = true;
        }
    }

    public void Run() {
        if (_isPressed) {
            if (_timer < _tower.time) {
                _timer += Time.fixedDeltaTime;
                _tower.image.fillAmount = _timer / _tower.time;
            } else {
                _isPressed = false;
                _tower.image.fillAmount = 1;
                _timer = 0;
            }
        }
    }

    public void Destroy() {
        _data.input.Skills.Disable();
    }
}