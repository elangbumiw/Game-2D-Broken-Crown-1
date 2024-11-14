using UnityEngine;

public class CameraFollowWithParallax : MonoBehaviour
{
    public Transform player;       // Referensi ke posisi player
    public Vector3 offset;         // Offset untuk jarak kamera dari player
    public float smoothSpeed = 0.125f; // Kecepatan smoothing kamera

    // Referensi untuk background yang akan mengikuti kamera dengan parallax effect
    public Transform Bg1;
    public Transform Bg2;
    public Transform Bg3;

    // Faktor parallax untuk setiap background
    public float parallaxFactorBg1 = 1.0f;
    public float parallaxFactorBg2 = 0.8f;
    public float parallaxFactorBg3 = 0.6f;

    void LateUpdate()
    {
        // Mengikuti player hanya pada sumbu x
        Vector3 desiredPosition = new Vector3(player.position.x + offset.x, transform.position.y, transform.position.z);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Mengatur posisi background berdasarkan parallax factor masing-masing
        Bg1.transform.position = new Vector2(transform.position.x * parallaxFactorBg1, Bg1.transform.position.y);
        Bg2.transform.position = new Vector2(transform.position.x * parallaxFactorBg2, Bg2.transform.position.y);
        Bg3.transform.position = new Vector2(transform.position.x * parallaxFactorBg3, Bg3.transform.position.y);
    }
}
