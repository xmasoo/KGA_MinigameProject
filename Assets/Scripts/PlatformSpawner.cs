using System.Collections.Generic;
using UnityEngine;
public enum PlatformType
{
    Normal,
    Breakable,
    Moving,
    Spring,
    Start
}
public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private PlatformPool poolNormal;
    [SerializeField] private PlatformPool poolBreakable;
    [SerializeField] private PlatformPool poolMoving;
    [SerializeField] private PlatformPool poolSpring;
    public Transform player;
    public float levelWidth = 3f;//플랫폼이 생성되는 가로 범위
    public float platformSpacing = 4f;//플랫폼이 생성되는 세로 높이
    public int initialPlatforms = 10;//기본 생성되는 플랫폼 개수

    private float spawnY = 0f;
    private List<GameObject> activePlatforms = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < initialPlatforms; i++)
            SpawnPlatform();
    }

    void Update()
    {
        while (player.position.y + 30f > spawnY)//이 높이만큼 플랫폼 생성
            SpawnPlatform();

        for (int i = activePlatforms.Count - 1; i >= 0; i--)
        {
            GameObject platform = activePlatforms[i];
            if (platform.transform.position.y < player.position.y - 12f)//이 높이 이하면 리턴풀
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

    PlatformType GetRandomPlatformType()//플랫폼 생성 확률
    {
        float r = Random.value;
        if (r < 0.05f) return PlatformType.Normal;
        else if (r < 0.75f) return PlatformType.Breakable;
        else if (r < 0.9f) return PlatformType.Moving;
        else return PlatformType.Spring;
    }

    PlatformPool GetPoolByType(PlatformType type)
    {
        switch (type)
        {
            case PlatformType.Normal: return poolNormal;
            case PlatformType.Breakable: return poolBreakable;
            case PlatformType.Moving: return poolMoving;
            case PlatformType.Spring: return poolSpring;
            default: return poolNormal;
        }
    }
}
