using BepInEx;
using HarmonyLib;

namespace MapImageObjects.Core;

[BepInDependency("com.willis.rounds.unbound")]
// TODO:
// extra map shit
// unhook
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInProcess("Rounds.exe")]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
        harmony.PatchAll();
    }
}
