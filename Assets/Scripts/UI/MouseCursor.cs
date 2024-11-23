using UnityEngine;

namespace UI
{
    public class MouseCursor : MonoBehaviour
    {
        private void Update()
        {
            transform.position = Helpers.MouseCursorPosFinder.GetMouseWorldPosition();
        }
    }
}