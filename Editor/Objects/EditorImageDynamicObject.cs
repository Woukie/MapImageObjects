using MapImageObjects.Core;
using MapsExt.Editor.MapObjects;
using UnityEngine;

namespace MapImageObjects.Editor.Objects;

[EditorMapObject(typeof(ImageDynamicObjectData), "Image", Category = "Dynamic")]
public class EditorImageDynamicObject : ImageDynamicObject
{
    public override void OnInstantiate(GameObject instance)
    {
        instance.GetComponent<Rigidbody2D>().isKinematic = true;
        base.OnInstantiate(instance);
    }
}