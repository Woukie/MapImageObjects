using MapImageObjects.Core.Components;
using MapsExt.Properties;
using UnboundLib;
using UnityEngine;

namespace MapImageObjects.Core.Properties;

[PropertySerializer(typeof(URIProperty))]
public class URIPropertySerializer : IPropertyWriter<URIProperty>
{
    public virtual void WriteProperty(URIProperty property, GameObject target)
    {
        target.GetOrAddComponent<URIComponent>().SetURI(property.uri);
    }
}