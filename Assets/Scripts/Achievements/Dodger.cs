using System;

public class Dodger : Achievement
{
    public Dodger()
    {
        Name = nameof(Dodger);
        AchieveCount = 30;
    }

    protected override void Complete()
    {
        UnityEngine.Debug.Log("You are rewarded by ass developers");
    }
}

