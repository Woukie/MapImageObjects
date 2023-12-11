using MapImageObjects.Objects.Data;
using MapsExt.MapObjects;
using UnityEngine;

namespace MapImageObjects.Objects;

[MapObject(typeof(ImageDestructibleObjectData))]
public class ImageDestructibleObject : ImageObject
{
    public override void OnInstantiate(GameObject instance)
    {
        base.OnInstantiate(instance);
    }
}