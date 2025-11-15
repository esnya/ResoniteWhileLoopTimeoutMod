using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using AutoFixture;

using FluentAssertions;

using WhileLoopTimeout;

namespace WhileLoopTimeout.Tests;

public static class WhileLoopTimeoutModTests
{
    [Fact]
    public static void Mod_Should_Have_Valid_Name()
    {
        WhileLoopTimeout.WhileLoopTimeoutMod mod = new();

        mod.Name.Should().Be("WhileLoopTimeout");
    }

    [Fact]
    public static void Mod_Should_Have_Valid_Author()
    {
        WhileLoopTimeout.WhileLoopTimeoutMod mod = new();

        mod.Author.Should().Be("esnya");
    }

    [Fact]
    public static void Mod_Should_Expose_Static_Version_Tag()
    {
        WhileLoopTimeout.WhileLoopTimeoutMod mod = new();

        mod.Version.Should().Be(WhileLoopTimeoutMod.VersionTag);
        mod.Version.Should().MatchRegex(@"^\d+\.\d+\.\d+");
    }

    [Theory]
    [InlineData(30_000)]
    public static void TimeoutMs_Should_Return_Default_When_Config_Not_Loaded(int expectedTimeout)
    {
        WhileLoopTimeout.WhileLoopTimeoutMod.TimeoutMs.Should().Be(expectedTimeout);
    }

    [Fact]
    public static void Mod_Should_Implement_ResoniteMod()
    {
        WhileLoopTimeout.WhileLoopTimeoutMod mod = new();

        mod.Should().BeAssignableTo<ResoniteModLoader.ResoniteMod>();
    }

    [Fact]
    public static void Mod_Properties_Should_Not_Change_Between_Instances()
    {
        WhileLoopTimeout.WhileLoopTimeoutMod first = new();
        WhileLoopTimeout.WhileLoopTimeoutMod second = new();

        first.Name.Should().Be(second.Name);
        first.Author.Should().Be(second.Author);
        first.Version.Should().Be(second.Version);
    }

    [Fact]
    public static void Fixture_Should_Create_Mock_Objects()
    {
        Fixture fixture = new();

        string randomString = fixture.Create<string>();
        int randomInt = fixture.Create<int>();

        randomString.Should().NotBeNullOrEmpty();
        randomInt.Should().NotBe(0);
    }

    [Fact]
    public static void Assembly_Should_Expose_Internals_To_Tests()
    {
        Assembly assembly = typeof(WhileLoopTimeout.WhileLoopTimeoutMod).Assembly;

        InternalsVisibleToAttribute? attribute = assembly
            .GetCustomAttributes<InternalsVisibleToAttribute>()
            .FirstOrDefault(attr => attr.AssemblyName == "WhileLoopTimeout.Tests");

        attribute.Should().NotBeNull();
    }
}
