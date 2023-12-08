using MapImageObjects.Core.Properties;
using MapsExt;
using MapsExt.Editor.UI;
using UnityEngine;
using UnityEngine.UI;

namespace MapImageObjects.Editor.Properties.InspectorElements;

[InspectorElement(typeof(URIProperty))]
public class URIElement : IInspectorElement
{
    private InspectorContext context;
    private InputField input;

    public string Value {
        get => context.InspectorTarget.ReadProperty<URIProperty>().uri;
        set => OnChanged(value);
    }

    // Do you like my absolutley cursed way of making a UI?
    public GameObject Instantiate(InspectorContext context)
    {
        this.context = context;

        GameObject instance = Object.Instantiate(Assets.InspectorVector2InputPrefab);

        Transform inputs = instance.transform.GetChild(1);
        Transform urlInput = inputs.GetChild(0);
        Transform label = instance.transform.GetChild(0);

        // Get rid of some logic for the input boxes
        GameObject.Destroy(instance.GetComponent<Vector2Input>()); 
        GameObject.Destroy(instance.GetComponent<InspectorVector2Input>());
        GameObject.Destroy(inputs.GetChild(1).gameObject); // Get rid of the "Y" input box entirley
        GameObject.Destroy(urlInput.GetChild(0).gameObject); // Get rid of the "X" label

        urlInput.GetChild(1).GetChild(0).GetComponent<Text>().text = "URL"; // Change placeholder text
        label.gameObject.GetComponent<Text>().text = "URL";

        input = instance.GetComponentInChildren<InputField>();
        input.onValueChanged.AddListener(OnChanged);

        return instance;
    }

    public void OnUpdate() {
        input.text = Value;
    }

    private void OnChanged(string str)
    {
        URIProperty property = new URIProperty();
        property.uri = str;
        Core.Plugin.ImageCache.Clear(); // Hacky but clear the cache so the mapper doesn't have to restart the game if they update an image. This way we still keep the optimisation on the client!
        context.InspectorTarget.WriteProperty(property);

        context.Editor.TakeSnaphot();
    }
}