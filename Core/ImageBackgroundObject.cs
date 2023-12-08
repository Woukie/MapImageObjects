using MapsExt.MapObjects;
using UnityEngine;

namespace MapImageObjects.Core;

[MapObject(typeof(ImageBackgroundObjectData))]
public class ImageBackgroundObject : ImageObject
{
    public override void OnInstantiate(GameObject instance)
    {
        GameObject.Destroy(instance.GetComponent<Collider2D>());
        GameObject.Destroy(instance.GetComponent<SFPolygon>());

        LateLoad<SpriteMask>(instance);
    }
}