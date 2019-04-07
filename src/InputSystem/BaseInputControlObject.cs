using System;
using UnityEngine;

namespace Extras.InputSystem
{
    [RequireComponent(typeof(BaseInputController))]
    public abstract class BaseControlObject : MonoBehaviour
    {
        protected void Setup(InputBindings bindings)
        {
            var inputController = GetComponent<BaseInputController>();
            if (inputController == null)
            {
                throw new NullReferenceException();
            }
            inputController.Setup(bindings);
        }
    }
}
