using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{
    public GameObject platformPrefab;
    public int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        // �̸� ����
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(platformPrefab, transform);//Ǯ�� ��ġ���� �Է��ϸ� �ڵ����� �ڽĿ�����Ʈ�� ��
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject GetPlatform(Vector3 position)
    {
        GameObject obj = pool.Count > 0 ? pool.Dequeue() : Instantiate(platformPrefab);
        obj.transform.position = position;
        obj.SetActive(true);
        return obj;
    }

    public void ReturnPlatform(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
