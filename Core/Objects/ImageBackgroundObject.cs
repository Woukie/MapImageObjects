using MapsExt.MapObjects;
using UnityEngine;

namespace MapImageObjects.Core;

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