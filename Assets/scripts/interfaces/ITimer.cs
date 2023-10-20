

public interface ITimer 
{
    /// <summary>
    /// indicates weather the timer has finished or not. Note stopping the timer before is 
    /// is finished should not change this value to true.
    /// </summary>
    bool IsCompleted { get; }


    /// <summary>
    /// indicates weather the timer is running or not.
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    /// Starts the timer.
    /// </summary>
    void Run();

    /// <summary>
    /// Stops the timer can be used as a pause.
    /// </summary>
    void Stop();

    /// <summary>
    /// Sets the time
    /// sets IsComplete to false
    /// </summary>
    /// <param name="time">the time for timer</param>
    void Set(float time);

    /// <summary>
    /// Resets the timer. Stops the timer as well if it's running.
    /// Sets IsComplete to false.
    /// </summary>
    void Reset();

}
