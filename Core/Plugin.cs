using BepInEx;
using HarmonyLib;

namespace MapImageObjects.Core;

[BepInDependency("com.willis.rounds.unbound")]
[BepInDependency("io.olavim.rounds.mapsextended")]
[BepInPlugin("com.woukie.rounds.mapimageobjects", "MapImageObjects", "1.1.1")]
[BepInProcess("Rounds.exe")]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        var harmony = new Harmony("com.woukie.rounds.mapimageobjects");
        harmony.PatchAll();
    }
}
