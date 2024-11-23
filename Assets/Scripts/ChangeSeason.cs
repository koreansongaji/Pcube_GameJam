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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isMoving)
        {
            StartCoroutine(MoveCloud(0));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && !isMoving)
        {
            StartCoroutine(MoveCloud(1));
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && !isMoving)
        {
            StartCoroutine(MoveCloud(2));
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4) && !isMoving)
        {
            StartCoroutine(MoveCloud(3));
        }
    }

    /// <summary>
    /// �� ���� ����
    /// </summary>
    /// <param name="idx">0 : Fall, 1 : Winter, 2 : Spring, 3 : Summer</param>
    public void Change(int idx)
    {
        for (int i = 0; i < seasons.Count; i++)
        {
            seasons[i].SetActive(false);
        }
        seasons[idx].SetActive(true);
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
