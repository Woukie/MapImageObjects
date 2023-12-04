using BepInEx;
using HarmonyLib;
using MapImageObjects.Properties;
using MapsExt;
using MapsExt.Editor.MapObjects;
using MapsExt.MapObjects;
using MapsExt.Properties;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace MapImageObjects;

[BepInDependency("com.willis.rounds.unbound")]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("Rounds.exe")]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();

        Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
    }

    // Destroy needs to be run on a MonoBehaviour for some reason
    public static void DestroyCrap(Object crap)
    {
        Destroy(crap);
    }
}

public class ImageObjectData : MapObjectData
{
    public PositionProperty position = new PositionProperty();
    public ScaleProperty scale = new ScaleProperty();
    public RotationProperty rotation = new RotationProperty();
}

[MapObject(typeof(ImageObjectData))]
public class ImageObject : IMapObject
{
    public static Material defaultMaterial = new Material(Shader.Find("Sprites/Default"));

    public GameObject Prefab => MapObjectManager.LoadCustomAsset<GameObject>("Ground");

    public virtual void OnInstantiate(GameObject instance)
    {
        // Texture2D texture = await GetRemoteTexture("https://raw.githubusercontent.com/Woukie/Image/main/cover%20transparent.png");

        SpriteRenderer spriteRenderer = instance.GetComponent<SpriteRenderer>();
        // spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), texture.height);
        spriteRenderer.material = defaultMaterial;
        spriteRenderer.color = Color.white;

        Plugin.DestroyCrap(instance.GetComponent<BoxCollider2D>());
        instance.AddComponent<PolygonCollider2D>();

        instance.GetComponent<SFPolygon>().enabled = false; // TODO: Transfer paths from polygon collision for shaddows

        DisableComponentLoading<SpriteMask>(instance);
    }

    // IDK whats putting a sprite mask here but I dont want it
    public static async void DisableComponentLoading<ComponentType>(GameObject instance) where ComponentType : Component
    {
        while (instance.GetComponent<ComponentType>() == null) await Task.Delay(1000 / 30);

        Plugin.DestroyCrap(instance.GetComponent<ComponentType>());

        return;
    }

    // Yoink
    // https://stackoverflow.com/a/53770838
    public static async Task<Texture2D> GetRemoteTexture(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            var asyncOp = www.SendWebRequest();

            while (asyncOp.isDone == false)
                await Task.Delay(1000 / 30);

            return www.isNetworkError || www.isHttpError ? null : DownloadHandlerTexture.GetContent(www);
        }
    }
}

[EditorMapObject(typeof(ImageObjectData), "Image")]
public class EditorMyObject : ImageObject
{
    public override void OnInstantiate(GameObject instance)
    {
        base.OnInstantiate(instance);

        MapsExt.Utils.GameObjectUtils.DisableRigidbody(instance);
    }
}