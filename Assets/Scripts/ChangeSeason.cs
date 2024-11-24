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
        // 총 시간의 25%가 지나면 변경
        if (GameManager.Instance.GameTime > GameManager.Instance.GetEndTime() * 0.25f && currentSeason == 0)
        {
            currentSeason = 1;
            Change(currentSeason);
        }
        
        // 총 시간의 50%가 지나면 변경
        if (GameManager.Instance.GameTime > GameManager.Instance.GetEndTime() * 0.5f && currentSeason == 1)
        {
            currentSeason = 2;
            Change(currentSeason);
        }
        
        // 총 시간의 75%가 지나면 변경
        if (GameManager.Instance.GameTime > GameManager.Instance.GetEndTime() * 0.75f && currentSeason == 2)
        {
            currentSeason = 3;
            Change(currentSeason);
        }

        if (GameManager.Instance.GameTime > GameManager.Instance.GetEndTime() * 0.90f && currentSeason == 3)
        {
            currentSeason = 0;
            Change(currentSeason);
            apple.SetActive(true);
        }
    }

    /// <summary>
    /// 맵 계절 변경
    /// </summary>
    /// <param name="idx">0 : Fall, 1 : Winter, 2 : Spring, 3 : Summer</param>
    public void Change(int idx)
    {
        StartCoroutine(MoveCloud(idx));
    }

    public IEnumerator MoveCloud(int idx)
    {
        isMoving = true; // 이동 중 플래그 활성화
        yield return ToMid();
        yield return new WaitForSeconds(1f); // 1초 대기
        Change(idx);
        yield return ToEnd();
        yield return new WaitForSecondsRealtime(1f);
        cloud.transform.position = start.transform.position; // 초기화
        isMoving = false; // 이동 완료 후 플래그 비활성화
    }

    IEnumerator ToMid()
    {
        yield return SmoothMove(cloud.transform, start.transform.position, mid.transform.position, 1f);
    }

    IEnumerator ToEnd()
    {
        yield return SmoothMove(cloud.transform, mid.transform.position, end.transform.position, 1f);
    }

    IEnumerator SmoothMove(Transform target, Vector3 from, Vector3 to, float duration)
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
}
