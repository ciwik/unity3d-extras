using UnityEngine;

namespace Extras.InputSystem
{
    public abstract class BaseInputController : MonoBehaviour
    {
        public void Setup(InputBindings bindings) => _bindings = bindings;

        protected InputBindings _bindings;
    }
}
