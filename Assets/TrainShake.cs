using UnityEngine;

public class TrainShake : MonoBehaviour
{
    public float shakeAmount = 0.1f; // How much the train shakes
    public float shakeFrequency = 1f; // How fast the train shakes

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float noiseSeed;

    void Start()
    {
        originalPosition = transform.localPosition;
        originalRotation = transform.localRotation;
        noiseSeed = Random.Range(0f, 100f); // Initialize a random seed for Perlin noise
    }

    void Update()
    {
        ShakeTrain();
    }

    void ShakeTrain()
    {
        // Using Perlin Noise for smooth, pseudo-random shake
        float shakeX = Mathf.PerlinNoise(noiseSeed, Time.time * shakeFrequency) * 2 - 1; // Generates a value between -1 and 1
        float shakeY = Mathf.PerlinNoise(noiseSeed + 1, Time.time * shakeFrequency) * 2 - 1;
        float shakeZ = Mathf.PerlinNoise(noiseSeed + 2, Time.time * shakeFrequency) * 2 - 1;

        Vector3 shakePosition = originalPosition + new Vector3(shakeX, shakeY, shakeZ) * shakeAmount;
        Quaternion shakeRotation = new Quaternion(originalRotation.x + shakeX * shakeAmount, 
            originalRotation.y + shakeY * shakeAmount, 
            originalRotation.z + shakeZ * shakeAmount, 
            originalRotation.w);

        transform.localPosition = shakePosition;
        transform.localRotation = shakeRotation;
    }
}