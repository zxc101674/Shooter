using UnityEngine.InputSystem;
using Leopotam.Ecs;
using UnityEngine;

sealed class System_Input_Spawn_Mine : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem {
    private Data_Input _data;
    private Spawn_Mine _mine;
    private Runtime_Hero _hero;
    private bool _isPressed;
    private float _timer;

    public void Init() {
        _data.input.Skills.Enable();
        _data.input.Skills.SpawnMine.performed += SpawnMine;
    }

    private void SpawnMine(InputAction.CallbackContext context) {
        if (!_isPressed) {
            Object.Instantiate(_mine.prefab, _hero.spawnPoints.mine.position, _hero.spawnPoints.mine.rotation);
            _isPressed = true;
        }
    }

    public void Run() {
        if (_isPressed) {
            if (_timer < _mine.time) {
                _timer += Time.fixedDeltaTime;
                _mine.image.fillAmount = _timer / _mine.time;
            } else {
                _isPressed = false;
                _mine.image.fillAmount = 1;
                _timer = 0;
            }
        }
    }

    public void Destroy() {
        _data.input.Skills.Disable();
    }
}