using System.Collections;
using UnityEngine;

public class BreakPlatform : PlatformBase
{
    [SerializeField] private GameObject breakPlatform;
    [SerializeField] private GameObject leftPiecePrefab;
    [SerializeField] private GameObject rightPiecePrefab;

    [Header("튀어오르는 힘")]
    [SerializeField] private float breakForce = 5f;
    [SerializeField] private float pieceLifetime = 2f;
    [SerializeField] private float disableDelay = 0.2f;

    public override void OnPlayerLanding()
    {
        // 1) 밟히면 바로 물리 이펙트 실행
        StartCoroutine(BreakRoutine());
    }

    private IEnumerator BreakRoutine()
    {
        // (a) 조각 생성
        Vector3 pos = transform.position;
        leftPiecePrefab.SetActive(true);
        rightPiecePrefab.SetActive(true);

        // (b) 물리력 추가
        var leftRb = leftPiecePrefab.GetComponent<Rigidbody2D>();
        var rightRb = rightPiecePrefab.GetComponent<Rigidbody2D>();
        leftRb.AddForce(new Vector2(-1, 1) * breakForce, ForceMode2D.Impulse);
        rightRb.AddForce(new Vector2(1, 1) * breakForce, ForceMode2D.Impulse);

        // (c) 원본 플랫폼 숨기기
        yield return new WaitForSeconds(disableDelay);
        breakPlatform.SetActive(false);

        // (d) 풀링/반환 또는 Destroy
        if (poolReference != null)
            poolReference.ReturnPlatform(gameObject);
        else
            Destroy(gameObject);
    }
}
