using UnityEngine;

public class BreakPlatform : PlatformBase
{
    [SerializeField] private GameObject breakPrefab;

    public override void OnPlayerLanding()
    {
        gameObject.SetActive(false);

        GameObject breakInstance = Instantiate(breakPrefab, transform.position, Quaternion.identity);

        Destroy(breakInstance, 2f); // 2초 뒤 이펙트 인스턴스 삭제
    }
}
