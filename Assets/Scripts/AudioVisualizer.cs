using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    public static AudioVisualizer Instance { get; private set; }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private FFTWindow fftWindow = FFTWindow.Blackman;
    [SerializeField] private int sampleIndex = 1; // más bajo = más graves
    private float[] spectrum = new float[64];

    [SerializeField] private float smoothSpeed = 5f; // Entre 2 y 10 es un buen rango
    private float targetValue;

    public float AudioPulseValue { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        audioSource.GetSpectrumData(spectrum, 0, fftWindow);
        float rawValue = spectrum[sampleIndex] * 150f;
        Debug.Log($"Raw Value: {rawValue}"); // Debugging line

        targetValue = Mathf.Clamp01(rawValue);

        AudioPulseValue = Mathf.Lerp(AudioPulseValue, targetValue, Time.deltaTime * smoothSpeed);
    }
}
