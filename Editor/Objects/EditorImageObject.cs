using MapImageObjects.Core;
using MapsExt.Editor.MapObjects;
using UnityEngine;

namespace MapImageObjects.Editor.Objects;

[EditorMapObject(typeof(ImageObjectData), "Image")]
public class EditorImageObject : ImageObject
{
    public override void OnInstantiate(GameObject instance)
    {
        base.OnInstantiate(instance);
    }
}