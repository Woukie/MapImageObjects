using MapImageObjects.Core;
using MapsExt.Editor.MapObjects;
using UnityEngine;

namespace MapImageObjects.Editor.Objects;

[EditorMapObject(typeof(ImageBackgroundObjectData), "Image (Background)")]
public class EditorImageBackgroundObject : ImageBackgroundObject
{
    public override void OnInstantiate(GameObject instance)
    {
        instance.AddComponent<BoxCollider2D>(); // Just so the user can actually select it.

        base.OnInstantiate(instance);
    }
}