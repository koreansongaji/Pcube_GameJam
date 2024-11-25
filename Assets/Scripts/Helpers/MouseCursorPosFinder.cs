using UnityEngine;

namespace Helpers
{
    public static class MouseCursorPosFinder
    {
        [System.Obsolete("Use TryGetMouseWorldPosition instead")]
        public static Vector3 GetMouseWorldPosition()
        {
            // 마우스가 가르키는 y 0.2f의 위치를 반환합니다.
            // RayMask를 사용해서 Mouse Detection 레이어만 검출하도록 설정
            if (Camera.main == null) throw new System.Exception("Main Camera not found");
            
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(
                    ray, 
                    out RaycastHit hit, 
                    1000, 
                    LayerMask.GetMask("Mouse Detection")
                    )
                )
            {
                return hit.point + new Vector3(0, 0, 0);
            }

            
            return Vector3.zero;
        }
        
        public static bool TryGetMouseWorldPosition(out Vector3 result)
        {
            // 마우스가 가르키는 y 0.2f의 위치를 반환합니다.
            // RayMask를 사용해서 Mouse Detection 레이어만 검출하도록 설정
            if (Camera.main == null)
            {
                Debug.LogError("Main Camera not found");
                result = Vector3.zero;
                return false;
            }
            
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(
                    ray, 
                    out RaycastHit hit, 
                    1000, 
                    LayerMask.GetMask("Mouse Detection")
                )
               )
            {
                result = hit.point + new Vector3(0, 0, 0);
                return true;
            }

            
            result = Vector3.zero;
            return false;
        }
    }
}