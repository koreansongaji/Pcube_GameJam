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

    private bool isMoving = false; // �̵� ������ Ȯ���ϴ� �÷���
    private int currentSeason = 0; // ���� ����

    private void Update()
    {
        if (isMoving) return; // �̵� ���̸� ������Ʈ ����
        
        Season curSeason = GetCurSeason();
        if (currentSeason == (int)curSeason) return; // ���� �����̶� ������ ������Ʈ ����
        
        Change((int)curSeason); // ���� ����
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
    /// �� ���� ����
    /// </summary>
    /// <param name="idx">0 : Fall, 1 : Winter, 2 : Spring, 3 : Summer</param>
    private void Change(int idx)
    {
        switch (idx)
        {
            case 0:
                SoundManager.Instance.PlayBGM(SoundManager.Ebgm.FALL);
                break;
            case 1:
                SoundManager.Instance.PlayBGM(SoundManager.Ebgm.WINTER);
                break;
            case 2:
                SoundManager.Instance.PlayBGM(SoundManager.Ebgm.SPRING);
                break;
            case 3:
                SoundManager.Instance.PlayBGM(SoundManager.Ebgm.SUMMER);
                break;
        }
        
        StartCoroutine(MoveCloud(idx));
    }

    private IEnumerator MoveCloud(int idx)
    {
        isMoving = true; // �̵� �� �÷��� Ȱ��ȭ
        yield return ToMid();
        yield return new WaitForSeconds(1f); // 1�� ���
        
        // ���� ����
        seasons[currentSeason].SetActive(false);
        seasons[idx].SetActive(true);
        
        yield return ToEnd();
        yield return new WaitForSecondsRealtime(1f);
        cloud.transform.position = start.transform.position; // �ʱ�ȭ
        isMoving = false; // �̵� �Ϸ� �� �÷��� ��Ȱ��ȭ
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

            // ��ġ ���
            target.position = Vector3.Lerp(from, to, smoothT);

            yield return null; // ���� �����ӱ��� ���
        }

        // �̵� �Ϸ� �� ��Ȯ�� ��ǥ ��ġ�� ����
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
