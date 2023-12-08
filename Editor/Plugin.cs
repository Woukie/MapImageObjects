using BepInEx;
using HarmonyLib;

namespace MapImageObjects.Editor;

[BepInDependency("com.willis.rounds.unbound")]
[BepInDependency("com.woukie.rounds.mapimageobjects")]
[BepInDependency("io.olavim.rounds.mapsextended")]
[BepInDependency("io.olavim.rounds.mapsextended.editor")]
[BepInPlugin("com.woukie.rounds.mapimageobjectseditor", "MapImageObjectsEditor", "1.1.1")]
[BepInProcess("Rounds.exe")]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        var harmony = new Harmony("com.woukie.rounds.mapimageobjectseditor");
        harmony.PatchAll();
    }
}
