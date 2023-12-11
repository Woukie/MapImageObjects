using MapsExt.MapObjects;
using MapsExt;
using System.Threading.Tasks;
using UnityEngine;
using UnboundLib;
using MapImageObjects.Core;
using MapImageObjects.Objects.Data;

namespace MapImageObjects.Objects;

[MapObject(typeof(ImageObjectData))]
public class ImageObject : IMapObject
{
    // IDK how custom assets work with bepinex so we just use this and set them up in OnInstantiate()
    public GameObject Prefab => MapObjectManager.LoadCustomAsset<GameObject>("Ground");

    public virtual void OnInstantiate(GameObject instance)
    {
        // spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

        UnityEngine.Object.Destroy(instance.GetComponent<Collider2D>());
        instance.AddComponent<PolygonCollider2D>();

        LateLoad<SpriteMask>(instance);
    }

    // Materials need a bit to load so I put them in here
    public static async void LateLoad<ComponentType>(GameObject instance) where ComponentType : Component
    {
        while (instance.GetComponent<ComponentType>() == null) await Task.Delay(1000 / 30);

        UnityEngine.Object.Destroy(instance.GetComponent<ComponentType>());
        SpriteRenderer spriteRenderer = instance.GetComponent<SpriteRenderer>();
        spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Move to background if it has no collision
        if (!instance.GetComponent<PolygonCollider2D>())
        {
            spriteRenderer.sortingLayerName = "Background";
        }

        instance.GetOrAddComponent<ColorComponent>().ApplyColor();

        return;
    }
}