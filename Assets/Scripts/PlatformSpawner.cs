using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public PlatformPool platformPool;
    public Transform player;
    public float levelWidth = 3f;
    public float platformSpacing = 2.5f;
    public int initialPlatforms = 10;

    private float spawnY = 0f;
    private List<GameObject> activePlatforms = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < initialPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        // ���� �ö󰡸� �� ����
        while (player.position.y + 10f > spawnY)
        {
            SpawnPlatform();
        }

        // �Ʒ��� �ִ� �� ��ȯ
        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            if (activePlatforms[i].transform.position.y < player.position.y - 15f)
            {
                platformPool.ReturnPlatform(activePlatforms[i]);
                activePlatforms.RemoveAt(i);
            }
        }
    }

    void SpawnPlatform()
    {
        float x = Random.Range(-levelWidth, levelWidth);
        Vector3 pos = new Vector3(x, spawnY, 0f);
        GameObject platform = platformPool.GetPlatform(pos);
        activePlatforms.Add(platform);
        spawnY += platformSpacing;
    }
}
