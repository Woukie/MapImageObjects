using MapImageObjects.Core.Properties;
using MapsExt.MapObjects;
using MapsExt.Properties;

namespace MapImageObjects.Core;

public class ImageObjectData : MapObjectData
{
    public PositionProperty position = new PositionProperty();
    public ScaleProperty scale = new ScaleProperty();
    public RotationProperty rotation = new RotationProperty();
    public AnimationProperty animation = new AnimationProperty();
    public ColorProperty color = new ColorProperty();
    public URIProperty uri = new URIProperty();
}