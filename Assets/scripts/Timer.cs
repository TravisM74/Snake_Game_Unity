using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour, ITimer {
    // Start is called before the first frame update

    /// <summary>
    /// the time left in the timer.
    /// </summary>
    private float time;

    /// <summary>
    /// the start time of the timer
    /// </summary>
    private float startTime;
    
    
    public bool IsCompleted {
        get {
            if (this.time <= 0) {
                this.time = 0;
                return true;
            }
            return false;
        }
    }

    public bool IsRunning {
        get ;
        private set;
    }

    private void Update() {
        if (IsRunning) {
            this.time -= Time.deltaTime;
            if (IsCompleted) {
                Stop();
            }
        }
    }

    public void Reset() {
        Stop();
        this.time = this.startTime;
    }

    public void Run() {
        IsRunning = true;
    }

    public void Set(float time) {
        
        this.startTime = time;
        Reset();
        
    }

    public void Stop() {
        IsRunning = false;
    }
}
