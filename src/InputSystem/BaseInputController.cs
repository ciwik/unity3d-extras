using UnityEngine;

namespace Extras.InputSystem
{
    public abstract class BaseInputController : MonoBehaviour
    {
        public void Setup(InputBindings bindings) => _bindings = bindings;

        protected InputBindings _bindings;
    }
}

// Example
//
//public class KeyboardInputController : BaseInputController
//{
//private void Update()
//{
//    if (Input.anyKey)
//    {
//        var axisValue = Input.GetAxis("Horizontal");
//        if (axisValue != 0f)
//        {
//            var direction = axisValue * Vector2.right;
//            _bindings.Movement?.Invoke(direction);
//        }
//
//        axisValue = Input.GetAxis("Jump");
//        if (axisValue != 0f)
//        {
//            _bindings.Action?.Invoke();
//        }
//    }
//}
//}
