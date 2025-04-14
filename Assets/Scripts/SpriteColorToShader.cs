using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteToColorShader: MonoBehaviour
{
    private MaterialPropertyBlock block;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        block = new MaterialPropertyBlock();
    }

    private void OnEnable()
    {
        UpdateVisual();
        SetBaseColor();
    }

    private void Update()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        sr.GetPropertyBlock(block);
        block.SetFloat("_AudioPulse", Mathf.Clamp(AudioVisualizer.Instance.AudioPulseValue, 0.4f, 1));
        sr.SetPropertyBlock(block);
    }

    public void SetBaseColor()
    {
        sr.GetPropertyBlock(block);
        block.SetColor("_BaseColor", sr.color);
        sr.SetPropertyBlock(block);
    }
}
