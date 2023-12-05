using BepInEx;
using HarmonyLib;

namespace MapImageObjects.Editor;

[BepInDependency("com.willis.rounds.unbound")]
[BepInDependency("com.woukie.rounds.MapImageObjects")]
[BepInDependency("io.olavim.rounds.mapsextended")]
[BepInDependency("io.olavim.rounds.mapsextended.editor")]
[BepInDependency("com.woukie.rounds.MapImageObjects")]
[BepInPlugin("com.woukie.rounds.MapImageObjectsEditor", "MapImageObjectsEditor", "1.0.0")]
[BepInProcess("Rounds.exe")]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        var harmony = new Harmony("com.woukie.rounds.MapImageObjectsEditor");
        harmony.PatchAll();
    }
}
