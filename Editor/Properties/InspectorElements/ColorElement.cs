using MapImageObjects.Core.Properties;
using MapsExt;
using MapsExt.Editor.UI;
using UnityEngine;

namespace MapImageObjects.Editor.Properties.InspectorElements;

[InspectorElement(typeof(ColorProperty))]
public class ColorElement : InspectorElement
{
    private TextSliderInput _r;
    private TextSliderInput _g;
    private TextSliderInput _b;
    private TextSliderInput _a;

    public Color Value
    {
        get => Context.InspectorTarget.ReadProperty<ColorProperty>();
        set => OnChange(value, ChangeType.ChangeEnd);
    }

    protected override GameObject GetInstance()
    {
        var instance = Object.Instantiate(Assets.FoldoutPrefab);
        var foldout = instance.GetComponent<Foldout>();
        foldout.Label.text = "Color";

        var r = Object.Instantiate(Assets.InspectorSliderInputPrefab, foldout.Content.transform);
        var quaternionInput = r.GetComponent<InspectorSliderInput>();
        quaternionInput.Label.text = "R";
        _r = quaternionInput.Input;
        _r.Slider.minValue = 0f;
        _r.Slider.maxValue = 255f;
        _r.Slider.wholeNumbers = true;
        _r.OnChanged += OnChange;

        var g = Object.Instantiate(Assets.InspectorSliderInputPrefab, foldout.Content.transform);
        quaternionInput = g.GetComponent<InspectorSliderInput>();
        quaternionInput.Label.text = "G";
        _g = quaternionInput.Input;
        _g.Slider.minValue = 0f;
        _g.Slider.maxValue = 255f;
        _g.Slider.wholeNumbers = true;
        _g.OnChanged += OnChange;

        var b = Object.Instantiate(Assets.InspectorSliderInputPrefab, foldout.Content.transform);
        quaternionInput = b.GetComponent<InspectorSliderInput>();
        quaternionInput.Label.text = "B";
        _b = quaternionInput.Input;
        _b.Slider.minValue = 0f;
        _b.Slider.maxValue = 255f;
        _b.Slider.wholeNumbers = true;
        _b.OnChanged += OnChange;

        var a = Object.Instantiate(Assets.InspectorSliderInputPrefab, foldout.Content.transform);
        quaternionInput = a.GetComponent<InspectorSliderInput>();
        quaternionInput.Label.text = "A";
        _a = quaternionInput.Input;
        _a.Slider.minValue = 0f;
        _a.Slider.maxValue = 255f;
        _a.Slider.wholeNumbers = true;
        _a.OnChanged += OnChange;

        return instance;
    }

    public override void OnUpdate()
    {
        _r.SetWithoutEvent(Mathf.RoundToInt(Value.r * 255));
        _g.SetWithoutEvent(Mathf.RoundToInt(Value.g * 255));
        _b.SetWithoutEvent(Mathf.RoundToInt(Value.b * 255));
        _a.SetWithoutEvent(Mathf.RoundToInt(Value.a * 255));
    }

    public void OnChange(Color color, ChangeType changeType)
    {
        if (changeType == ChangeType.Change || changeType == ChangeType.ChangeEnd)
        {
            Context.InspectorTarget.WriteProperty<ColorProperty>((Color32)color);
        }

        if (changeType == ChangeType.ChangeEnd)
        {
            Context.Editor.TakeSnaphot();
        }

        ColorProperty.defaultColor = color;
    }

    public void OnChange(float value, ChangeType changeType)
    {
        if (changeType == ChangeType.Change || changeType == ChangeType.ChangeEnd)
        {
            Context.InspectorTarget.WriteProperty<ColorProperty>((Color32)new Color(_r.Value / 255, _g.Value / 255, _b.Value / 255, _a.Value / 255));
        }

        if (changeType == ChangeType.ChangeEnd)
        {
            Context.Editor.TakeSnaphot();
        }

        ColorProperty.defaultColor = (Color32)new Color(_r.Value / 255, _g.Value / 255, _b.Value / 255, _a.Value / 255);
    }
}