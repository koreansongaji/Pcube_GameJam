using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSeason : MonoBehaviour
{
    [SerializeField] private List<GameObject> seasons = new List<GameObject>();

    [SerializeField] private GameObject cloud;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject mid;
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject apple;

    private bool isMoving = false; // 이동 중인지 확인하는 플래그
    private int currentSeason = 0; // 현재 계절

    private void Update()
    {
        if (isMoving) return; // 이동 중이면 업데이트 중지
        
        Season curSeason = GetCurSeason();
        if (currentSeason == (int)curSeason) return; // 현재 계절이랑 같으면 업데이트 중지
        
        Change((int)curSeason); // 계절 변경
    }
    
    private float GetCurTimePercentage()
    {
        return GameManager.Instance.GameTime / GameManager.Instance.GetEndTime() * 100.0f;
    }
    
    private Season GetCurSeason()
    {
        float curPercentage = GetCurTimePercentage();

        return curPercentage switch
        {
            < 30.0f => Season.WINTER,
            < 60.0f => Season.SPRING,
            < 90.0f => Season.SUMMER,
            _       => Season.FALL
        };
    }   

    /// <summary>
    /// 맵 계절 변경
    /// </summary>
    /// <param name="idx">0 : Fall, 1 : Winter, 2 : Spring, 3 : Summer</param>
    private void Change(int idx)
    {
        StartCoroutine(MoveCloud(idx));
    }

    private IEnumerator MoveCloud(int idx)
    {
        isMoving = true; // 이동 중 플래그 활성화
        yield return ToMid();
        yield return new WaitForSeconds(1f); // 1초 대기
        
        // 계절 변경
        seasons[currentSeason].SetActive(false);
        seasons[idx].SetActive(true);
        
        yield return ToEnd();
        yield return new WaitForSecondsRealtime(1f);
        cloud.transform.position = start.transform.position; // 초기화
        isMoving = false; // 이동 완료 후 플래그 비활성화
        currentSeason = idx;
        
        if(apple != null) apple.SetActive(idx == (int)Season.FALL);
    }

    private IEnumerator ToMid()
    {
        yield return SmoothMove(cloud.transform, start.transform.position, mid.transform.position, 1f);
    }

    private IEnumerator ToEnd()
    {
        yield return SmoothMove(cloud.transform, mid.transform.position, end.transform.position, 1f);
    }

    private static IEnumerator SmoothMove(Transform target, Vector3 from, Vector3 to, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            float smoothT = Mathf.SmoothStep(0, 1, t);

            // 위치 계산
            target.position = Vector3.Lerp(from, to, smoothT);

            yield return null; // 다음 프레임까지 대기
        }

        // 이동 완료 후 정확히 목표 위치로 설정
        target.position = to;
    }

    private enum Season
    {
        FALL = 0,
        WINTER = 1,
        SPRING = 2,
        SUMMER = 3
    }
}
