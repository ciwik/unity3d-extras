using Extras.Diagnostics;
using UnityEngine;

namespace Extras.InputSystem
{
    [RequireComponent(typeof(BaseInputController))]
    public abstract class BaseControlObject : MonoBehaviour
    {
        protected void Setup(InputBindings bindings)
        {
            Guard.NotNull(bindings);

            var inputController = GetComponent<BaseInputController>();
            Guard.NotNull(inputController);

            inputController.Setup(bindings);
        }
    }
}
