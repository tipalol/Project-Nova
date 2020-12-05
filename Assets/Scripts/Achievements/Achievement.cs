using System;
using UnityEngine;

public abstract class Achievement
{
    protected string Name = nameof(Achievement);
    protected int Count;
    protected int AchieveCount = 1;
    protected Statistics _stats;

    public virtual void UpdateProgress()
    {
        Count++;
        RecordChanges();
        Debug.Log($"We have already killed {Count} civilians, and we ok with hat");
        if (IsAchieved())
            Complete();
    }

    protected virtual bool IsAchieved() => Count == AchieveCount;
    protected virtual void RecordChanges() => PlayerPrefs.SetInt(Name, Count);
    protected abstract void Complete(); // Show GUI, Update status in database
}
