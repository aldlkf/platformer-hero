using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioSource deathSoundEffect;
    // Если игрок упал ниже этой высоты (провалился за карту) — он погибает
    [SerializeField] private float fallLimitY = -20f;

    public bool IsDead { get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!IsDead && transform.position.y < fallLimitY)
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    private void Die()
    {
        // Защита от повторного вызова (несколько столкновений подряд)
        if (IsDead)
        {
            return;
        }

        IsDead = true;
        deathSoundEffect.Play();
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    // Вызывается Animation Event в конце анимации Player_Death
    private void RestartLevel()
    {
        GameSession.RollbackLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}