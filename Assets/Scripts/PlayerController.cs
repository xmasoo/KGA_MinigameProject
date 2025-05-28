using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float width;


    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        width = 3f;
    }

    private void Update()
    {
        PlayerMove();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.y >= 0)
        {
            if (collision.collider.CompareTag("Platform"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            // 아래로 떨어지고 있을 때만 점프 반응
            if (rb.velocity.y <= 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
        }
    }
    public void PlayerMove()
    {
        float input = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(input * moveSpeed, rb.velocity.y);

        Vector3 pos = transform.position;
        if (pos.x > width) pos.x = -width;
        else if (pos.x < -width) pos.x = width;
        transform.position = pos;
    }
}
