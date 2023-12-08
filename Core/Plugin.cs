using BepInEx;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace MapImageObjects.Core;

[BepInDependency("com.willis.rounds.unbound")]
[BepInDependency("io.olavim.rounds.mapsextended")]
[BepInPlugin("com.woukie.rounds.mapimageobjects", "MapImageObjects", "1.1.3")]
[BepInProcess("Rounds.exe")]
public class Plugin : BaseUnityPlugin
{
    // Maps URI to sprite
    public static Dictionary<string, Sprite> ImageCache = new Dictionary<string, Sprite>();

    private void Awake()
    {
        var harmony = new Harmony("com.woukie.rounds.mapimageobjects");
        harmony.PatchAll();
    }
}
