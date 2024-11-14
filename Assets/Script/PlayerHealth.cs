using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 5; // Nyawa awal pemain, bisa disesuaikan

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Nyawa pemain berkurang, sisa nyawa: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Pemain mati!");
        // Tambahkan logika kematian pemain di sini
    }
}
