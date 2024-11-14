using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject gameOverPanel;  // Referensi ke panel Game Over
    [SerializeField] float remainingTime;
    private bool isTimerRunning = true;

    void Start()
    {
        gameOverPanel.SetActive(false);  // Menyembunyikan panel Game Over di awal
    }

    void Update()
    {
        if (isTimerRunning && remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime <= 0)
            {
                remainingTime = 0;
                timerText.color = Color.red;
                isTimerRunning = false;
                GameOver();
            }
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    public void ResumeTimer()
    {
        isTimerRunning = true;
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);  // Menampilkan panel Game Over
    }
}
