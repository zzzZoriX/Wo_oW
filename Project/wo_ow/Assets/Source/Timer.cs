using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool DoWhile = false;
    public event Action Action;
    public float Duration { get; private set; }
    public float Current { get; private set; }

    public void Set(float time) => Duration = time;
    public void Run() => Current = Duration;

    public void SetNRun(float time)
    {
        Duration = time;
        Current = Duration;
    }


    private void Update()
    {
        if (Current <= 0)
        {
            Action?.Invoke();
        }
        else
        {
            Current -= Time.deltaTime;

            if (DoWhile)
            {
                Action?.Invoke();
            }
        }
    }
}
