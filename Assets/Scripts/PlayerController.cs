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
        //Debug.Log("트리거 감지됨: " + collision.name);

        if (collision.CompareTag("Platform"))
        {
            //Debug.Log("Platform 태그 확인됨");

            if (rb.velocity.y <= 0f)
            {
                //Debug.Log("낙하 중");

                PlatformBase platform = collision.GetComponent<PlatformBase>();
                if (platform is SpringPlatform spring)
                {
                    //Debug.Log("spring");
                    rb.velocity = new Vector2(rb.velocity.x, spring.boostJumpForce);
                }
                else
                {
                    //Debug.Log("기본 점프");
                    platform.OnPlayerLanding();
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                }
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
