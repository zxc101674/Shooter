using UnityEngine.InputSystem;
using Leopotam.Ecs;
using UnityEngine;

sealed class System_Input_Activate_Dron : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem {
    private Data_Input _data;
    private Activate_Dron _dron;
    private Runtime_Hero _hero;
    private bool _isPressed;
    private float _timer;

    public void Init() {
        _data.input.Skills.Enable();
        _data.input.Skills.ActivateDrons.performed += ActivateDrons;
    }

    private void ActivateDrons(InputAction.CallbackContext context) {
        if (!_isPressed) {
            Object.Instantiate(_dron.prefab, _hero.spawnPoints.dron.position, _hero.spawnPoints.dron.rotation);
            _isPressed = true;
        }
    }

    public void Run() {
        if (_isPressed) {
            if (_timer < _dron.time) {
                _timer += Time.fixedDeltaTime;
                _dron.image.fillAmount = _timer / _dron.time;
            } else {
                _isPressed = false;
                _dron.image.fillAmount = 1;
                _timer = 0;
            }
        }
    }

    public void Destroy() {
        _data.input.Skills.Disable();
    }
}