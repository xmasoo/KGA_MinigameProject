using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float width;

    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip jumpLongSound;
    [SerializeField] AudioClip jumpBreakSound;


    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "JumpGameScene")
            PlayerMove();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Ʈ���� ������: " + collision.name);

        if (collision.CompareTag("Platform"))
        {
            //Debug.Log("Platform �±� Ȯ�ε�");

            if (rb.velocity.y <= 0f)
            {
                //Debug.Log("���� ��");

                PlatformBase platform = collision.GetComponent<PlatformBase>();
                if (platform is SpringPlatform spring)
                {
                    //Debug.Log("spring");
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed + 10);
                    SoundManager.Instance.PlaySFX(jumpLongSound);
                }
                else
                {
                    //Debug.Log("�⺻ ����");
                    platform.OnPlayerLanding();
                    rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
                    if (platform is BreakPlatform)
                        SoundManager.Instance.PlaySFX(jumpBreakSound);
                    else
                        SoundManager.Instance.PlaySFX(jumpSound);
                }
            }
        }
    }
    public void PlayerMove()
    {
        float input = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(input * moveSpeed, rb.velocity.y);

        if (input > 0 && transform.localScale.x < 0)
            Flip();   // ������ ����
        else if (input < 0 && transform.localScale.x > 0)
            Flip();   // ���� ����

        Vector3 pos = transform.position;
        if (pos.x > width) pos.x = -width;
        else if (pos.x < -width) pos.x = width;
        transform.position = pos;

    }

    private void Flip()
    {
        Vector3 s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }
}
