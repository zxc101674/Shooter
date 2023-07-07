using UnityEngine.InputSystem;
using Leopotam.Ecs;
using UnityEngine;

sealed class System_Input_Hero_Move : IEcsInitSystem, IEcsDestroySystem {
    private Data_Input _data;

    public void Init() {
        _data.input.Hero.Enable();
        _data.input.Hero.Move.performed += Move;
        _data.input.Hero.Move.canceled += Stop;
    }

    private void Move(InputAction.CallbackContext context) {
        _data.moveInput = _data.input.Hero.Move.ReadValue<Vector2>();
    }

    private void Stop(InputAction.CallbackContext context) {
        _data.moveInput = Vector2.zero;
    }

    public void Destroy() {
        _data.input.Hero.Disable();
    }
}