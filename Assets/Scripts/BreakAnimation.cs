using UnityEngine;

public class BreakAnimation : MonoBehaviour
{
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    [SerializeField] float power;
    private void OnEnable()
    {
        Rigidbody2D leftRb = left.GetComponent<Rigidbody2D>();
        Rigidbody2D rightRb = right.GetComponent<Rigidbody2D>();

        leftRb.AddForce(new Vector2(-1, 1) * power, ForceMode2D.Impulse);
        rightRb.AddForce(new Vector2(1, 1) * power, ForceMode2D.Impulse);
    }
}
