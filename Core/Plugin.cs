using BepInEx;
using HarmonyLib;

namespace MapImageObjects.Core;

[BepInDependency("com.willis.rounds.unbound")]
[BepInDependency("io.olavim.rounds.mapsextended")]
[BepInPlugin("com.woukie.rounds.MapImageObjects", "MapImageObjects", "1.0.0")]
[BepInProcess("Rounds.exe")]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        var harmony = new Harmony("com.woukie.rounds.MapImageObjects");
        harmony.PatchAll();
    }
}
