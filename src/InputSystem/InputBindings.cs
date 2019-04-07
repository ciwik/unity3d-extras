using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extras.InputSystem
{
    public class InputBindings
    {
        public InputBindings(
            Action<Vector2> movement = null,
            Action<Vector2> direction = null,
            Action action = null,
            Dictionary<string, Action> otherActions = null)
        {
            this.Movement = movement;
            this.Direction = direction;
            this.Action = action;

            _otherActions = otherActions;
        }

        // TODO: Add more properties
        // Depends of direction of movement (commonly conrolled by WASD or joystick)
        public Action<Vector2> Movement { get; }
        // Depends of direction of sight (commonly conrolled by mouse, joystick or touchscreen)
        public Action<Vector2> Direction { get; }
        // Non-parametric action (commonly controlled by Space/Enter key or A gamepad button) 
        public Action Action { get; }
        // Custom non-parametric actions
        public Action this[string key] => _otherActions[key];

        private Dictionary<string, Action> _otherActions;
    }
}
