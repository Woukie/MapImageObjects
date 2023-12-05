using MapImageObjects.Core;
using MapImageObjects.Core.Properties;
using MapsExt.Editor.Properties;
using MapsExt.Properties;
using UnityEngine;

namespace MapImageObjects.Editor.Properties;

[EditorPropertySerializer(typeof(URIProperty))]
public class EditorURIPropertySerializer : URIPropertySerializer, IPropertyReader<URIProperty>
{
    public virtual URIProperty ReadProperty(GameObject instance)
    {
        URIProperty property = new URIProperty();
        property.uri = instance.GetComponent<URIComponent>().GetURI();
        return property;
    }
}