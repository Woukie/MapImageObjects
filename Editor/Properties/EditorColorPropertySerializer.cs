using MapImageObjects.Core.Properties;
using MapsExt.Editor.Properties;
using MapsExt.Properties;
using UnityEngine;

namespace MapImageObjects.Editor.Properties;

[EditorPropertySerializer(typeof(ColorProperty))]
public class EditorColorPropertySerializer : ColorPropertySerializer, IPropertyReader<ColorProperty>
{
    public ColorProperty ReadProperty(GameObject instance)
    {
        var spriteRenderer = instance.GetComponentInChildren<SpriteRenderer>();

        Color color = new Color32(200, 200, 200, 255);

        if (spriteRenderer != null)
        {
            color = spriteRenderer.color;
        }

        if (spriteRenderer == null && instance.GetComponentInChildren<LineRenderer>())
        {
            LineRenderer lineRenderer = instance.GetComponentInChildren<LineRenderer>();
            color = lineRenderer.endColor;
        }

        ColorProperty colorProperty = new ColorProperty(color);

        ColorProperty.defaultColor = colorProperty;

        return colorProperty;
    }
}
