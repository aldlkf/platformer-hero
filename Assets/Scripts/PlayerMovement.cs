using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private PlayerLife life;

    [SerializeField] private LayerMask jumpableGround;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    // Coyote time: можно прыгнуть сразу после схода с края платформы
    [SerializeField] private float coyoteTime = 0.1f;
    // Jump buffer: нажатие прыжка чуть раньше приземления не «теряется»
    [SerializeField] private float jumpBufferTime = 0.1f;
    // Во сколько раз гасим скорость, если кнопку прыжка отпустили раньше (прыжок переменной высоты)
    [SerializeField, Range(0.1f, 1f)] private float jumpCutMultiplier = 0.5f;

    private float coyoteCounter;
    private float jumpBufferCounter;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        life = GetComponent<PlayerLife>();
    }

    private void Update()
    {
        // Не реагируем на ввод после смерти и во время паузы
        if (Time.timeScale == 0f || (life != null && life.IsDead))
        {
            return;
        }

        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        bool grounded = IsGrounded() && rb.velocity.y < 0.1f;
        coyoteCounter = grounded ? coyoteTime : coyoteCounter - Time.deltaTime;
        jumpBufferCounter = Input.GetButtonDown("Jump") ? jumpBufferTime : jumpBufferCounter - Time.deltaTime;

        if (jumpBufferCounter > 0f && coyoteCounter > 0f)
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0f;
            coyoteCounter = 0f;
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}