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

    private bool isMoving = false; // �̵� ������ Ȯ���ϴ� �÷���

    private void Update()
    {
        // GameTime�� �� 4�б⸶�� ���� ��ȭ
        if (GameManager.Instance.GameTime > 60 * 2.5f && isMoving)
        {
            Change(1);
        }
        else if (GameManager.Instance.GameTime > 60 * 5f && isMoving)
        {
            Change(2);
        }
        else if (GameManager.Instance.GameTime > 60 * 7.5f && isMoving)
        {
            Change(3);
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
