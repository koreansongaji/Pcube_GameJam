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
        // �� �ð��� 25%�� ������ ����
        if (GameManager.Instance.GameTime > GameManager.Instance.GetEndTime() * 0.25f && currentSeason == 0)
        {
            currentSeason = 1;
            Change(currentSeason);
        }
        
        // �� �ð��� 50%�� ������ ����
        if (GameManager.Instance.GameTime > GameManager.Instance.GetEndTime() * 0.5f && currentSeason == 1)
        {
            currentSeason = 2;
            Change(currentSeason);
        }
        
        // �� �ð��� 75%�� ������ ����
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
    /// �� ���� ����
    /// </summary>
    /// <param name="idx">0 : Fall, 1 : Winter, 2 : Spring, 3 : Summer</param>
    public void Change(int idx)
    {
        StartCoroutine(MoveCloud(idx));
    }

    public IEnumerator MoveCloud(int idx)
    {
        isMoving = true; // �̵� �� �÷��� Ȱ��ȭ
        yield return ToMid();
        yield return new WaitForSeconds(1f); // 1�� ���
        Change(idx);
        yield return ToEnd();
        yield return new WaitForSecondsRealtime(1f);
        cloud.transform.position = start.transform.position; // �ʱ�ȭ
        isMoving = false; // �̵� �Ϸ� �� �÷��� ��Ȱ��ȭ
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

            // ��ġ ���
            target.position = Vector3.Lerp(from, to, smoothT);

            yield return null; // ���� �����ӱ��� ���
        }

        // �̵� �Ϸ� �� ��Ȯ�� ��ǥ ��ġ�� ����
        target.position = to;
    }
}
