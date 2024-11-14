using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private int health = 10; // Nyawa musuh
    public float speed = 2f; // Kecepatan musuh
    public float attackRange = 1.5f; // Jarak serangan musuh
    public int attackDamage = 1; // Damage serangan musuh
    public float attackCooldown = 1f;

    private float nextAttackTime = 0f;
    private Transform player;
    private PlayerController playerController; // Referensi ke PlayerController
    private bool isAttacking = false; // Apakah musuh sedang menyerang
    private bool facingRight = true; // Apakah musuh menghadap ke kanan

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Panggil fungsi Flip untuk membalik arah jika diperlukan
        Flip();

        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown;
            }
        }
        else if (distanceToPlayer <= 5f && !isAttacking)
        {
            ChasePlayer();
        }
        else
        {
            Idle();
        }
    }

    private void Idle()
    {
        animator.Play("Idle");
        rb.velocity = Vector2.zero;
    }

    private void ChasePlayer()
    {
        animator.Play("Walk");
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * speed;
    }

    private void Attack()
    {
        isAttacking = true;
        rb.velocity = Vector2.zero; // Berhenti mendorong player
        animator.Play("Attack");

        // Berikan damage ke player setelah jeda sinkron dengan animasi
        Invoke(nameof(DealDamageToPlayer), 0.5f);
    }

    private void DealDamageToPlayer()
    {
        if (playerController != null)
        {
            playerController.TakeDamage(attackDamage);
            Debug.Log("Musuh menyerang pemain!");
        }
        isAttacking = false; // Reset status serangan
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Musuh terkena damage, sisa nyawa: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        // Cegah metode dipanggil lebih dari sekali
        if (health <= 0) return;

        health = 0; // Set nyawa ke 0
        animator.Play("Death"); // Mainkan animasi kematian
        Debug.Log("Musuh mati!");
        this.enabled = false; // Nonaktifkan skrip Enemy
    }

    private void Flip()
    {
        // Cek apakah posisi pemain ada di kiri atau kanan musuh
        if ((player.position.x < transform.position.x && facingRight) ||
            (player.position.x > transform.position.x && !facingRight))
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1; // Balikkan sumbu X
            transform.localScale = scale;
        }
    }
}
