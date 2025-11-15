using System.Threading.Tasks;

using FrooxEngine.ProtoFlux;

using Moq;

using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using ProtoFlux.Runtimes.Execution.Nodes;

using WhileLoopTimeout.Patching;

using ExecutionContext = ProtoFlux.Runtimes.Execution.ExecutionContext;

namespace WhileLoopTimeout.Tests;

public sealed class AsyncWhile_RunAsync_PatchTests
{
    [Fact]
    public void Prefix_WithNonFrooxEngineContext_ShouldReturnTrue()
    {
        Mock<ExecutionContext> mockExecutionContext = new();
        Mock<AsyncWhile> mockAsyncWhile = new();
        Task<IOperation>? result = null;

        bool shouldContinue = AsyncWhile_RunAsync_Patch.Prefix(
            mockAsyncWhile.Object,
            mockExecutionContext.Object,
            ref result!);

        Assert.True(shouldContinue);
        Assert.Null(result);
    }

    [Fact]
    public void Prefix_WithFrooxEngineContext_ShouldReturnFalse()
    {
        Mock<FrooxEngineContext> mockFrooxEngineContext = new();
        Mock<AsyncWhile> mockAsyncWhile = new();
        Task<IOperation>? result = null;

        bool shouldContinue = AsyncWhile_RunAsync_Patch.Prefix(
            mockAsyncWhile.Object,
            mockFrooxEngineContext.Object,
            ref result!);

        Assert.False(shouldContinue);
        Assert.NotNull(result);
    }
}
