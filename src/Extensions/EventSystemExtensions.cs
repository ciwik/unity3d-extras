using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Extras.Extensions
{
    public static class EventSystemExtensions
    {
        // Returns all intersections at a screen point
        public static IReadOnlyCollection<RaycastResult> Raycast(
            this EventSystem eventSystem,
            Vector2 screenPosition)
        {
            var eventData = new PointerEventData(eventSystem)
            {
                position = screenPosition
            };

            var results = new List<RaycastResult>();
            eventSystem.RaycastAll(eventData, results);

            return results.AsReadOnly();
        }
    }
}
