using System.Collections;
using UnityEngine;

public class BreakPlatform : PlatformBase
{
    [SerializeField] private GameObject breakPlatform;
    [SerializeField] private GameObject leftPiecePrefab;
    [SerializeField] private GameObject rightPiecePrefab;

    [Header("Ƣ������� ��")]
    [SerializeField] private float breakForce = 5f;
    [SerializeField] private float pieceLifetime = 2f;
    [SerializeField] private float disableDelay = 0.2f;

    public override void OnPlayerLanding()
    {
        // 1) ������ �ٷ� ���� ����Ʈ ����
        StartCoroutine(BreakRoutine());
    }

    private IEnumerator BreakRoutine()
    {
        // (a) ���� ����
        Vector3 pos = transform.position;
        leftPiecePrefab.SetActive(true);
        rightPiecePrefab.SetActive(true);

        // (b) ������ �߰�
        var leftRb = leftPiecePrefab.GetComponent<Rigidbody2D>();
        var rightRb = rightPiecePrefab.GetComponent<Rigidbody2D>();
        leftRb.AddForce(new Vector2(-1, 1) * breakForce, ForceMode2D.Impulse);
        rightRb.AddForce(new Vector2(1, 1) * breakForce, ForceMode2D.Impulse);

        // (c) ���� �÷��� �����
        yield return new WaitForSeconds(disableDelay);
        breakPlatform.SetActive(false);

        // (d) Ǯ��/��ȯ �Ǵ� Destroy
        if (poolReference != null)
            poolReference.ReturnPlatform(gameObject);
        else
            Destroy(gameObject);
    }
}
