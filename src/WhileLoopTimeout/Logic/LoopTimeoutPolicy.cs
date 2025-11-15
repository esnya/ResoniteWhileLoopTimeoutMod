namespace WhileLoopTimeout.Logic;

/// <summary>
/// Provides pure timeout policy helpers for loop execution.
/// </summary>
internal static class LoopTimeoutPolicy
{
    /// <summary>
    /// Determines whether the loop should abort due to timeout or explicit abort request.
    /// </summary>
    /// <param name="previousTick">Previous engine update tick.</param>
    /// <param name="currentTick">Current engine update tick.</param>
    /// <param name="elapsedMs">Elapsed milliseconds since the last different tick.</param>
    /// <param name="timeoutMs">Timeout threshold in milliseconds.</param>
    /// <param name="abortRequested">Whether an external abort has been requested.</param>
    /// <returns>True when the loop should abort.</returns>
    public static bool ShouldAbort(long previousTick, long currentTick, long elapsedMs, int timeoutMs, bool abortRequested)
    {
        return abortRequested || (currentTick == previousTick && elapsedMs > timeoutMs);
    }
}
