using System.Reflection;

using FluentAssertions;

using HarmonyLib;

using ProtoFlux.Runtimes.Execution.Nodes;

using WhileLoopTimeout.Patching;

namespace WhileLoopTimeout.Tests;

public static class While_Run_PatchTests
{
    [Fact]
    public static void Patch_Should_Have_HarmonyPatch_Attribute()
    {
        Type patchType = typeof(While_Run_Patch);
        patchType
            .GetCustomAttribute<HarmonyPatch>()
            .Should()
            .NotBeNull();
    }

    [Fact]
    public static void Patch_Should_Target_While_Type()
    {
        HarmonyPatch? attribute = typeof(While_Run_Patch).GetCustomAttribute<HarmonyPatch>();
        attribute.Should().NotBeNull();
        attribute!.info.declaringType.Should().Be<While>();
    }

    [Fact]
    public static void Patch_Should_Target_Run_Method()
    {
        HarmonyPatch? attribute = typeof(While_Run_Patch).GetCustomAttribute<HarmonyPatch>();
        attribute.Should().NotBeNull();
        attribute!.info.methodName.Should().Be("Run");
    }

    [Fact]
    public static void Patch_Should_Have_Prefix_Method()
    {
        MethodInfo? method = typeof(While_Run_Patch).GetMethod(
            "Prefix",
            BindingFlags.Static | BindingFlags.NonPublic);
        method.Should().NotBeNull();
        method!.ReturnType.Should().Be<bool>();
    }

    [Fact]
    public static void Patch_Should_Be_Internal_Class()
    {
        typeof(While_Run_Patch).IsPublic.Should().BeFalse();
    }
}
