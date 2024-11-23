using UnityEngine;

namespace Helpers
{
    public static class MouseCursorPosFinder
    {
        public static Vector3 GetMouseWorldPosition()
        {
            // 마우스가 가르키는 y 0.2f의 위치를 반환합니다.
            // RayMask를 사용해서 Mouse Detection 레이어만 검출하도록 설정
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000, 
                    LayerMask.GetMask("Mouse Detection")))
            {
                return hit.point + new Vector3(0, 0, 0);
            }
            else
            {
                Debug.LogError("Mouse Detection 레이어가 없습니다.");
                return Vector3.zero;
            }
        }
    }
}