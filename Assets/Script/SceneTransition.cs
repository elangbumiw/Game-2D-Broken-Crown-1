using UnityEngine;
using UnityEngine.SceneManagement;  // Ini untuk mengatur scene

public class SceneTransition : MonoBehaviour
{
    public string nextSceneName; // Nama scene yang ingin dimuat

    // Ketika player memasuki area collider (trigger)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Periksa apakah objek yang memasuki trigger adalah player
        {
            SceneManager.LoadScene(nextSceneName);  // Muat scene baru
        }
    }
}
