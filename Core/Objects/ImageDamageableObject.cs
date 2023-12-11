using MapsExt.MapObjects;
using UnityEngine;

namespace MapImageObjects.Core;

[MapObject(typeof(ImageDamageableObjectData))]
public class ImageDamageableObject : ImageObject
{
    public override void OnInstantiate(GameObject instance)
    {
        base.OnInstantiate(instance);
    }
}