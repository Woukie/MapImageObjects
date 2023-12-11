using MapImageObjects.Objects.Data;
using MapsExt.MapObjects;
using UnityEngine;

namespace MapImageObjects.Objects;

[MapObject(typeof(ImageBackgroundObjectData))]
public class ImageBackgroundObject : ImageObject
{
    public override void OnInstantiate(GameObject instance)
    {
        Object.Destroy(instance.GetComponent<Collider2D>());
        Object.Destroy(instance.GetComponent<SFPolygon>());

        LateLoad<SpriteMask>(instance);
    }
}