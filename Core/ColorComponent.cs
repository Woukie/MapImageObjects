using UnityEngine;

namespace MapImageObjects;

public class ColorComponent : MonoBehaviour
{
    public Color color = Color.white;

    public Color GetColor()
    {
        return color;
    }

    public void SetColor(Color color)
    {
        this.color = color;
        ApplyColor();
    }

    public void ApplyColor()
    {
        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.color = color;
        }

        foreach (LineRenderer lineRenderer in GetComponentsInChildren<LineRenderer>())
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
        }
    }
}