using UnityEngine;

namespace Extras.InputSystem
{
// TODO: It is just a silly example, should be transferred to the Ministry of silly examples
    public class KeyboardInputController : BaseInputController
    {
        private void Update()
        {
            if (Input.anyKey)
            {
                var axisValue = Input.GetAxis("Horizontal");
                if (axisValue != 0f)
                {
                    var direction = axisValue * Vector2.right;
                    _bindings.Movement?.Invoke(direction);
                }

                axisValue = Input.GetAxis("Jump");
                if (axisValue != 0f)
                {
                    _bindings.Action?.Invoke();
                }
            }
        }
    }
}
