using MapImageObjects;
using MapImageObjects.Elements;
using MapImageObjects.Properties;
using MapsExt;
using MapsExt.Editor.Properties;
using MapsExt.Editor.UI;
using MapsExt.Properties;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace MapImageObjects.Properties
{
    public class TextureURIProperty : IProperty
    {
        public string text = "https://raw.githubusercontent.com/Woukie/Image/main/cover%20transparent.png";
    }

    [PropertySerializer(typeof(TextureURIProperty))]
    public class TextureURIPropertySerializer : IPropertyWriter<TextureURIProperty>
    {
        public async void WriteProperty(TextureURIProperty property, GameObject target)
        {
            Texture2D texture = await GetRemoteTexture("https://raw.githubusercontent.com/Woukie/Image/main/cover%20transparent.png");

            SpriteRenderer spriteRenderer = target.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), texture.height);
        }

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
}

[InspectorElement(typeof(TextureURIProperty))]
public class TextureURIElement : IInspectorElement
{
    private InspectorContext context;
    private InputField input;

    public GameObject Instantiate(InspectorContext context)
    {
        this.context = context;

        // TODO: Instantiate a whole ass text box in the inspector without prefabs

        // Then set up inputs from this text box

        GameObject parent = new GameObject("TextBox");
        parent.AddComponent<RectTransform>();
        parent.AddComponent<LayoutElement>();
        parent.AddComponent<HorizontalLayoutGroup>();
        parent.AddComponent<HorizontalLayoutGroup>();

        input = instance.GetComponent<InputField>();
        input.onValueChanged.AddListener(OnInputChanged);

        return instance;
    }

    public void OnUpdate()
    {
        input.onValueChanged.RemoveListener(OnInputChanged);

        input.text = context.InspectorTarget.ReadProperty<TextureURIProperty>().text;

        input.onValueChanged.AddListener(OnInputChanged);
    }

    private void OnInputChanged(string str)
    {
        var property = new TextureURIProperty();
        property.text = str;
        context.InspectorTarget.WriteProperty(property);
        context.Editor.TakeSnaphot();
    }
}