using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DefaultNamespace
{
    public class PlayerInputs : MonoBehaviour
    {
        public Vector2 move;
        public InputStates interact = InputStates.Raised;
        private ShoplifterInputActions inputs;

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
            inputs = new ShoplifterInputActions();
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

        public void OnMove(InputAction.CallbackContext context)
        {
            move = context.ReadValue<Vector2>();
        }

        private void Update()
        {
            
        }
    }
}