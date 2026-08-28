using System;
using System.Reflection;

using HarmonyLib;

using ResoniteModLoader;
#if USE_RESONITE_HOT_RELOAD_LIB
using ResoniteHotReloadLib;
#endif

namespace WhileLoopTimeout;

public sealed class WhileLoopTimeoutMod : ResoniteMod
{
    private const string HarmonyIdentifier = "com.nekometer.esnya.while-loop-timeout";
    private const int DefaultTimeoutMs = 30_000;
    private static readonly Assembly ModAssembly = typeof(WhileLoopTimeoutMod).Assembly;
    private static readonly Harmony HarmonyInstance = new(HarmonyIdentifier);

    [AutoRegisterConfigKey]
    private static readonly ModConfigurationKey<int> TimeoutKey = new(
        "Timeout",
        "Timeout in milliseconds before loop execution aborts.",
        computeDefault: () => DefaultTimeoutMs);

    private static ModConfiguration? configuration;

    public override string Name => "WhileLoopTimeout";
    public override string Author => "esnya";
    public override string Version => ModAssembly.GetName().Version?.ToString(3) ?? "0.0.0";
    public override string Link => "https://github.com/esnya/ResoniteWhileLoopTimeoutMod";

    internal static int TimeoutMs => configuration?.GetValue(TimeoutKey) ?? DefaultTimeoutMs;

    public override void OnEngineInit()
    {
        InitializeMod(this);
    }

#if USE_RESONITE_HOT_RELOAD_LIB
    public static void BeforeHotReload()
    {
        HarmonyInstance.UnpatchAll(HarmonyIdentifier);
    }

    public static void OnHotReload(ResoniteMod modInstance)
    {
        if (modInstance is WhileLoopTimeoutMod whileLoopTimeoutMod)
        {
            InitializeMod(whileLoopTimeoutMod);
        }
    }
#endif

    private static void InitializeMod(WhileLoopTimeoutMod modInstance)
    {
        ArgumentNullException.ThrowIfNull(modInstance);
#if USE_RESONITE_HOT_RELOAD_LIB
        HotReloader.RegisterForHotReload(modInstance);
#endif
        configuration = modInstance.GetConfiguration();
        HarmonyInstance.PatchAll(typeof(WhileLoopTimeoutMod).Assembly);
    }
}
