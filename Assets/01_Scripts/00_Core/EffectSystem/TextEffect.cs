using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class TextEffect : EffectObject
{
    [SerializeField, Min(0f)] private float riseDistance = 0.5f;

    private TextMeshPro textMesh;
    private Color originalColor;
    private Vector3 startPosition;
    private float elapsed;
    private float playDuration;

    public override void Play(float duration, object value)
    {
        textMesh.text = value?.ToString() ?? string.Empty;
        textMesh.color = originalColor;
        startPosition = transform.position;
        elapsed = 0f;
        playDuration = Mathf.Max(0.01f, duration);
        base.Play(duration, value);
    }

    // Unity 생명주기
    private void Awake()
    {
        textMesh = GetComponent<TextMeshPro>();
        originalColor = textMesh.color;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        float progress = Mathf.Clamp01(elapsed / playDuration);

        transform.position = startPosition + Vector3.up * (riseDistance * progress);
        Color color = originalColor;
        color.a *= 1f - progress;
        textMesh.color = color;
    }
}
