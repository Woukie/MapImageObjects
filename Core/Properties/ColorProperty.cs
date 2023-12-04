using MapsExt.Properties;
using UnityEngine;

namespace MapImageObjects.Properties;
public class ColorProperty : ValueProperty<Color>, ILinearProperty<ColorProperty> {
    public static Color32 defaultColor = new Color32(255, 255, 255, 255);

    [SerializeField] private readonly int _r;
    [SerializeField] private readonly int _g;
    [SerializeField] private readonly int _b;
    [SerializeField] private readonly int _a;

    public override Color Value => new Color32((byte)_r, (byte)_g, (byte)_b, (byte)_a);

    public ColorProperty() : this(defaultColor) { }

    public ColorProperty(Color color) : this(Mathf.RoundToInt(color.r * 255), Mathf.RoundToInt(color.g * 255), Mathf.RoundToInt(color.b * 255), Mathf.RoundToInt(color.a * 255)) { }

    public ColorProperty(Color32 color) : this(color.r, color.g, color.b, color.a) { }

    public ColorProperty(int r, int g, int b, int a) {
        _r = r;
        _g = g;
        _b = b;
        _a = a;
    }

    public static implicit operator Color(ColorProperty prop) => prop.Value;
    public static implicit operator Color32(ColorProperty prop) => prop.Value;
    public static implicit operator ColorProperty(Color color) => new ColorProperty(color);
    public static implicit operator ColorProperty(Color32 color32) => new ColorProperty(color32);

    public ColorProperty Lerp(ColorProperty end, float t) => Color.Lerp(this, end, t);
    public IProperty Lerp(IProperty end, float t) => Lerp((ColorProperty)end, t);
}
