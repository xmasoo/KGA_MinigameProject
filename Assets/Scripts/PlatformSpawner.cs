using System.Collections.Generic;
using UnityEngine;
public enum PlatformType
{
    Normal,
    Breakable,
    Moving
}
public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private PlatformPool poolNormal;
    [SerializeField] private PlatformPool poolBreakable;
    [SerializeField] private PlatformPool poolMoving;
    public Transform player;
    public float levelWidth = 3f;
    public float platformSpacing = 2.5f;
    public int initialPlatforms = 10;

    private float spawnY = 0f;
    private List<GameObject> activePlatforms = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < initialPlatforms; i++)
            SpawnPlatform();
    }

    void Update()
    {
        while (player.position.y + 10f > spawnY)
            SpawnPlatform();

        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            GameObject platform = activePlatforms[i];
            if (platform.transform.position.y < player.position.y - 15f)
            {
                PlatformBase baseScript = platform.GetComponent<PlatformBase>();
                if (baseScript != null && baseScript.poolReference != null)
                    baseScript.poolReference.ReturnPlatform(platform);

                activePlatforms.RemoveAt(i);
            }
        }
    }

    void SpawnPlatform()
    {
        float x = Random.Range(-levelWidth, levelWidth);
        Vector3 pos = new Vector3(x, spawnY, 0f);

        PlatformType type = GetRandomPlatformType();
        PlatformPool usedPool = GetPoolByType(type);

        GameObject platform = usedPool.GetPlatform(pos);

        PlatformBase baseScript = platform.GetComponent<PlatformBase>();
        if (baseScript != null)
            baseScript.poolReference = usedPool;

        activePlatforms.Add(platform);
        spawnY += platformSpacing;
    }

    PlatformType GetRandomPlatformType()
    {
        float r = Random.value;
        if (r < 0.6f) return PlatformType.Normal;
        else if (r < 0.85f) return PlatformType.Breakable;
        else return PlatformType.Moving;
    }

    PlatformPool GetPoolByType(PlatformType type)
    {
        switch (type)
        {
            case PlatformType.Normal: return poolNormal;
            case PlatformType.Breakable: return poolBreakable;
            case PlatformType.Moving: return poolMoving;
            default: return poolNormal;
        }
    }
}
