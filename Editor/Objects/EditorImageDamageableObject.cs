using MapImageObjects.Core;
using MapsExt.Editor.MapObjects;
using UnityEngine;

namespace MapImageObjects.Editor.Objects;

[EditorMapObject(typeof(ImageDamageableObjectData), "Image (Damageable)")]
public class EditorImageDamageableObject : ImageDamageableObject
{
    public override void OnInstantiate(GameObject instance)
    {
        base.OnInstantiate(instance);
    }
}