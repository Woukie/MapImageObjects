using MapsExt.MapObjects;
using MapsExt;
using System.Threading.Tasks;
using UnityEngine;
using UnboundLib;
using MapImageObjects.Core.Components;
using System.Collections;

namespace MapImageObjects.Core;

[MapObject(typeof(ImageObjectData))]
public class ImageObject : IMapObject
{
    // IDK how custom assets work with bepinex so we just use this and set them up in OnInstantiate()
    public virtual GameObject Prefab => MapObjectManager.LoadCustomAsset<GameObject>("Ground");

    public virtual void OnInstantiate(GameObject instance)
    {
        // spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

        UnityEngine.Object.Destroy(instance.GetComponent<Collider2D>());
        instance.AddComponent<PolygonCollider2D>();

        DelayResetNetworkPhysics(instance);

        LateLoad<SpriteMask>(instance);
    }

    // Materials need a bit to load so I put them in here
    public static async void LateLoad<ComponentType>(GameObject instance) where ComponentType : Component
    {
        while (instance.GetComponent<ComponentType>() == null) await Task.Delay(1000 / 30);

        UnityEngine.Object.Destroy(instance.GetComponent<ComponentType>());
        SpriteRenderer spriteRenderer = instance.GetOrAddComponent<SpriteRenderer>();
        spriteRenderer.material = new Material(Shader.Find("Sprites/Default"));

        // Move to background if it has no collision
        if (!instance.GetComponent<PolygonCollider2D>())
        {
            spriteRenderer.sortingLayerName = "Background";
        }

        instance.GetOrAddComponent<ColorComponent>().ApplyColor();

        URIComponent uriComponent = instance.GetComponent<URIComponent>();
        if (uriComponent != null) { 
            uriComponent.SetURI(uriComponent.GetURI()); 
        }

        return;
    }

    // Super jank but I need a quick solution for this, NetworkPhysicsObject keeps resetting during loading for some reason
    public async void DelayResetNetworkPhysics(GameObject instance)
    {
        await Task.Delay(1000);
        instance.GetComponent<NetworkPhysicsObject>().Awake();
    }
}