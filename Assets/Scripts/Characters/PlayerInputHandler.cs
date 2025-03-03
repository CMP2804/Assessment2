using System.Collections.Generic;
using System.Net.Http.Headers;
using cmp2804.Characters.Movement;
using cmp2804.Math;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

namespace cmp2804.Characters
{
    [RequireComponent(typeof(PositionMovement), typeof(RotationMovement), typeof(Clap))]
    [HideMonoScript]
    public class PlayerInputHandler : SerializedMonoBehaviour
    {
        private PositionMovement _positionMovement;
        private RotationMovement _rotationMovement;
        private Clap _clap;

        [Title("Movement States", "The possible states the player can move in.")]
        [OdinSerialize] private Dictionary<string, MovementState> _movementStates;

        private void Awake()
        {
            _positionMovement = GetComponent<PositionMovement>();
            _rotationMovement = GetComponent<RotationMovement>();
            _clap = GetComponent<Clap>();
        }

        public void SetMoveDirection(InputAction.CallbackContext context)
        {
            var direction = (Vector3)context.ReadValue<Vector2>();
            var vector3 = new Vector3(direction.x, 0, direction.y);
            var target = new Target(vector3);
            _positionMovement.Target = target;
            _rotationMovement.Target = target;
        }

        public void Crawl(InputAction.CallbackContext context)
        {
            SetMovementState(context, "Crawl");
        }
        
        public void Crouch(InputAction.CallbackContext context)
        {
            SetMovementState(context, "Crouch");
        }

        public void Jog(InputAction.CallbackContext context)
        {
            SetMovementState(context, "Jog");
        }
        public void Clap(InputAction.CallbackContext context)
        {
            _clap.Process(context.phase, context.duration);
        }

        private void SetMovementState(InputAction.CallbackContext context, string newStateName)
        {
            if (context.canceled)
                newStateName = "Walk"; 
            _movementStates.TryGetValue(newStateName, out var newState);
            _positionMovement.MovementState = newState;
        }
    }
}