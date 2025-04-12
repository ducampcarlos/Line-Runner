using UnityEngine;

public class AudioPulse : MonoBehaviour
{
    public AudioSource audioSource;
    public Material sharedMaterial;
    public int bandIndex = 1;
    public float sensitivity = 10f;

    float[] spectrum = new float[64];

    void Update()
    {
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.Blackman);
        float bassValue = Mathf.Clamp01(spectrum[bandIndex] * sensitivity);

        sharedMaterial.SetFloat("_AudioPulse", bassValue);
        sharedMaterial.SetFloat("_TimeOffset", Time.time); 
    }
}
