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
    }

    private void Update()
    {
        PlayerMove();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform") && rb.velocity.y <= 0f)
        {
            // Á¡ÇÁ
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

            // ÇÃ·§Æû µ¿ÀÛ È£Ãâ (Breakable µî)
            PlatformBase platform = collision.GetComponent<PlatformBase>();
            if (platform != null)
            {
                platform.OnPlayerLanding();
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
