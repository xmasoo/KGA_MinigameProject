using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float highestY;
    public float lockedX = 0f;  // ���ϴ� X ��ġ�� ����
    public float lockedZ = -10f; // �Ϲ����� 2D ī�޶� Z ��

    void Start()
    {
        highestY = transform.position.y;
    }

    void LateUpdate()
    {
        float currentY = transform.position.y;

        // �ְ� ���� ����
        if (currentY > highestY)
            highestY = currentY;

        // ī�޶� ��ġ ����: X, Z�� ���� / Y�� �ְ� ���̱����� �ö�
        transform.position = new Vector3(lockedX, highestY, lockedZ);
    }
}
