using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private float highestY;
    public float lockedX = 0f;  // 원하는 X 위치로 고정
    public float lockedZ = -10f; // 일반적인 2D 카메라 Z 값

    void Start()
    {
        highestY = transform.position.y;
    }

    void LateUpdate()
    {
        float currentY = transform.position.y;

        // 최고 높이 갱신
        if (currentY > highestY)
            highestY = currentY;

        // 카메라 위치 고정: X, Z는 고정 / Y는 최고 높이까지만 올라감
        transform.position = new Vector3(lockedX, highestY, lockedZ);
    }
}
