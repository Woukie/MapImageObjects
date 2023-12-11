using MapImageObjects.Core.Components;
using MapsExt;
using MapsExt.MapObjects;
using System;
using UnboundLib;
using UnityEngine;

namespace MapImageObjects.Core;

[MapObject(typeof(ImageDestructibleObjectData))]
public class ImageDestructibleObject : ImageObject
{
    public override GameObject Prefab => MapObjectManager.LoadCustomAsset<GameObject>("Box Destructible");

    public override void OnInstantiate(GameObject instance)
    {
        // Remove the 'color' child, responsible for putting the 'box' sprite over the object and effects like blinking when shot.
        // Can't really get around this, effects interfere with too much, eg blinking also sets colour.
        GameObject.Destroy(instance.transform.GetChild(0).gameObject);

        instance.GetComponent<Collider2D>().enabled = false;
        instance.AddComponent<PolygonCollider2D>();

        SpriteRenderer spriteRenderer = instance.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));

        instance.GetOrAddComponent<ColorComponent>().ApplyColor();
    }
}
