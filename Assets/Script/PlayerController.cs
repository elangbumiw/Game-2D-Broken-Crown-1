using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Tambahkan untuk mengelola UI

public class PlayerController : MonoBehaviour
{
    bool isJump = true;
    bool isDead = false;
    int idMove = 0;
    Animator anim;

    private Vector3 initialScale;

    public float attackRange = 0.5f;
    public int attackDamage = 10;
    public LayerMask enemyLayer;

    public int health = 5; // Nyawa awal pemain
    public int maxHealth = 5; // Nyawa maksimum pemain
    private float nextAttackTime = 0f;
    public float attackCooldown = 0.5f;

    // Referensi ke layar "You Dead"
    public GameObject gameOverScreen;

    // Referensi ke bar nyawa
    public Image healthBar; // Gambar bar nyawa yang bisa diinput

    private void Start()
    {
        anim = GetComponent<Animator>();
        initialScale = transform.localScale;

        // Pastikan layar "You Dead" tidak aktif di awal
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        // Set bar nyawa ke nilai maksimum
        if (healthBar != null)
        {
            UpdateHealthBar();
        }
    }

    void Update()
    {
        // Jika pemain mati, hentikan semua input
        if (isDead) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Idle();
        }

        Move();
        Dead();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isJump)
        {
            anim.ResetTrigger("jump");
            if (idMove == 0) anim.SetTrigger("idle");
            isJump = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        anim.SetTrigger("jump");
        anim.ResetTrigger("run");
        anim.ResetTrigger("idle");
        isJump = true;
    }

    public void MoveRight()
    {
        if (isDead) return; // Cegah pergerakan jika pemain mati
        idMove = 1;
    }

    public void MoveLeft()
    {
        if (isDead) return; // Cegah pergerakan jika pemain mati
        idMove = 2;
    }

    private void Move()
    {
        if (isDead) return; // Cegah pergerakan jika pemain mati

        if (idMove == 1)
        {
            if (!isJump) anim.SetTrigger("run");
            transform.localScale = new Vector3(-Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
            transform.Translate(1 * Time.deltaTime * 5f, 0, 0);
        }
        if (idMove == 2)
        {
            if (!isJump) anim.SetTrigger("run");
            transform.localScale = new Vector3(Mathf.Abs(initialScale.x), initialScale.y, initialScale.z);
            transform.Translate(-1 * Time.deltaTime * 5f, 0, 0);
        }
    }

    public void Jump()
    {
        if (isDead || isJump) return; // Cegah lompatan jika pemain mati atau sedang di udara
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300f);
    }

    public void Idle()
    {
        if (isDead) return; // Cegah aksi idle jika pemain mati

        anim.ResetTrigger("jump");
        anim.ResetTrigger("run");
        anim.SetTrigger("idle");
        idMove = 0;
    }

    public void Attack()
    {
        if (isDead || Time.time < nextAttackTime) return; // Cegah serangan jika pemain mati atau cooldown belum selesai

        anim.SetTrigger("attack");
        nextAttackTime = Time.time + attackCooldown;

        // Mendeteksi semua musuh di dalam jangkauan serangan
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.Die(); // Membuat musuh mati
                Destroy(enemy.gameObject, 0.5f); // Hapus musuh dari game setelah animasi selesai
                Debug.Log("Musuh mati!");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Cegah pengurangan nyawa jika pemain sudah mati

        health -= damage;
        Debug.Log("Pemain terkena serangan, sisa nyawa: " + health);

        UpdateHealthBar(); // Perbarui tampilan bar nyawa

        if (health <= 0)
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            // Ubah ukuran bar berdasarkan proporsi nyawa
            healthBar.fillAmount = (float)health / maxHealth;
        }
    }

    private void Die()
    {
        if (isDead) return; // Pastikan proses mati hanya terjadi sekali

        isDead = true;
        anim.SetTrigger("dead"); // Mainkan animasi kematian
        Debug.Log("Pemain mati!");

        // Tampilkan layar "You Dead"
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }
    }

    private void Dead()
    {
        if (!isDead && transform.position.y < -10f)
        {
            Die(); // Pemain mati jika jatuh dari platform
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
