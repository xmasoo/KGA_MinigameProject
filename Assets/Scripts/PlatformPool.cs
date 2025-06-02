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
        // 미리 생성
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(platformPrefab, transform);//풀의 위치정보 입력하면 자동으로 자식오브젝트로 들어감
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
