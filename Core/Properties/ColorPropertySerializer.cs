using MapsExt.Properties;
using UnboundLib;
using UnityEngine;

namespace MapImageObjects.Properties;

[PropertySerializer(typeof(ColorProperty))]
public class ColorPropertySerializer : IPropertyWriter<ColorProperty> {
    public void WriteProperty(ColorProperty property, GameObject target) {
        target.GetOrAddComponent<ColorComponent>().SetColor(property);
    }
}