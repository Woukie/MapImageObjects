using MapImageObjects.Core.Components;
using MapsExt;
using MapsExt.MapObjects;
using UnboundLib;
using UnityEngine;

namespace MapImageObjects.Core;

[MapObject(typeof(ImageDynamicObjectData))]
public class ImageDynamicObject : ImageObject
{
    public override GameObject Prefab => MapObjectManager.LoadCustomAsset<GameObject>("Box");

    public override void OnInstantiate(GameObject instance)
    {
        // GameObject.Destroy(instance.transform.GetChild(0).gameObject);

        instance.GetComponent<Collider2D>().enabled = false;
        instance.AddComponent<PolygonCollider2D>();

        SpriteRenderer spriteRenderer = instance.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = true;
        spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));

        GameObject.Destroy(instance.transform.GetChild(0).gameObject); // Background
        GameObject.Destroy(instance.transform.GetChild(1).gameObject); // Lines

        instance.GetOrAddComponent<ColorComponent>().ApplyColor();
    }
}
