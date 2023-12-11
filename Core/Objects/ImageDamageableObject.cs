using MapsExt;
using MapsExt.MapObjects;
using System;
using UnityEngine;

namespace MapImageObjects.Core;

[MapObject(typeof(ImageDamageableObjectData))]
public class ImageDamageableObject : ImageObject
{
    public override GameObject Prefab => MapObjectManager.LoadCustomAsset<GameObject>("Box Destructible");

    public override void OnInstantiate(GameObject instance)
    {
        Console.WriteLine("EARLY LOAD");
        instance.GetComponent<Rigidbody2D>().isKinematic = true;
        // Remove the 'color' child, responsible for putting the 'box' sprite over the object and effects like blinking when shot.
        // Can't really get around this, effects controll interfere with too much, eg blinking also controls colour.
        GameObject.DestroyImmediate(instance.transform.GetChild(0).gameObject);
        instance.GetComponent<SpriteRenderer>().enabled = true;

        base.OnInstantiate(instance);
    }
}