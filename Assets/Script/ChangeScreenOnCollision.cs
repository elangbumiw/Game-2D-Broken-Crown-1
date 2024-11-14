using UnityEngine;
using UnityEngine.SceneManagement; // Untuk manajemen scene

public class ChangeScreenOnCollision : MonoBehaviour
{
    // Nama scene yang ingin dituju
    [SerializeField] private string targetSceneName;

    // Fungsi ini terpanggil saat ada tabrakan
    private void OnTriggerEnter(Collider other)
    {
        // Periksa apakah objek yang bertabrakan adalah player
        if (other.CompareTag("Player"))
        {
            // Ganti ke scene yang ditentukan
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
