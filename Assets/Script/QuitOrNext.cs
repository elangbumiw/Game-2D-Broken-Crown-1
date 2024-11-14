using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitOrNext : MonoBehaviour
{
    // Nama adegan yang akan dimuat
    [SerializeField]
    private string NameScene = "";

    private void Update()
    {
        // Keluar dari aplikasi jika tombol Escape ditekan
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Exiting application...");
            Application.Quit();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Periksa apakah objek yang bertabrakan memiliki tag "Player"
        if (collision.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(NameScene))
            {
                Debug.Log($"Loading scene: {NameScene}");
                SceneManager.LoadScene(NameScene);
            }
            else
            {
                Debug.LogError("NameScene is empty or not assigned!");
            }
        }
    }
}
