using MapsExt.MapObjects;
using UnityEngine;

namespace MapImageObjects.Core;

[MapObject(typeof(ImageDestructibleObjectData))]
public class ImageDestructibleObject : ImageObject
{
    public override void OnInstantiate(GameObject instance)
    {
        base.OnInstantiate(instance);
    }
}