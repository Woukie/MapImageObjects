using MapImageObjects.Core;
using MapsExt.Editor.MapObjects;
using UnityEngine;

namespace MapImageObjects.Editor.Objects;

[EditorMapObject(typeof(ImageDestructibleObjectData), "Image (Destructible)", Category = "Dynamic")]
public class EditorImageDestructibleObject : ImageDestructibleObject
{
    public override void OnInstantiate(GameObject instance)
    {
        instance.GetComponent<Rigidbody2D>().isKinematic = true;
        base.OnInstantiate(instance);
    }
}