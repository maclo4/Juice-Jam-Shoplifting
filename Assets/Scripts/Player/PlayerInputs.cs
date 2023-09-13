using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class PlayerInputs : MonoBehaviour
    {
        public Vector2 move;
        public InputStates interact = InputStates.Raised;
        public InputStates useItem = InputStates.Raised;
        private PlayerCharacterInputActions inputs;
        private PlayerCharacterController _playerCharacterController;

        private void OnEnable()
        {
            inputs.Enable();
        }
        private void OnDisable()
        {
            inputs.Disable();
        }

        private void Awake()
        {
            _playerCharacterController = GetComponent<PlayerCharacterController>();
            inputs = new PlayerCharacterInputActions();
            /*inputs.Controller.Move.performed += OnMove;
            inputs.Controller.Move.canceled += OnMove;*/
            //inputs.Controller.Interact.performed += OnInteract;
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.action.WasPerformedThisFrame())
            {
                interact = InputStates.WasPressedThisFrame;
            }
            else if (context.action.IsPressed())
            {
                interact = InputStates.Pressed;
            }
            if (context.action.WasReleasedThisFrame())
            {
                interact = InputStates.Raised;
            }
        }
        
        public void OnUseItem(InputAction.CallbackContext context)
        {
            if (context.action.phase == InputActionPhase.Performed && useItem == InputStates.Raised)
            {
                useItem = InputStates.WasPressedThisFrame;
                //so bad but no time
                if (_playerCharacterController.teleporting) return;
                
                _playerCharacterController.UseItem();
            }          
            else if (context.phase == InputActionPhase.Performed)
            {
                useItem = InputStates.Pressed;
            }/*
            else if (context.action.IsPressed())
            {
                useItem = InputStates.Pressed;
            }*/
            if (context.phase == InputActionPhase.Canceled)
            {
                useItem = InputStates.Raised;
            }
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            move = context.ReadValue<Vector2>();
        }

    }
}