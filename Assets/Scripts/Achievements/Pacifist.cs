using UnityEngine;
using UnityEngine.SceneManagement;

public class Pacifist : Achievement
{
    public Pacifist()
    {
        Name = nameof(Pacifist);
        AchieveCount = 0;

        _stats = Statistics.GetInstance();
        _stats.StatsUpdated.AddListener(UpdateProgress);

    }

    public override void UpdateProgress()
    {
        Count = _stats.MobKilledCount;

        Debug.Log($"Прогресс достижения Пацифист обновлен. Вы убили: {Count} раз");

        if (IsAchieved())
            Complete();
    }

    protected override void Complete()
    {
        UnityEngine.Debug.Log("You are rewarded by ass developers");
    }

    protected override bool IsAchieved()
        => SceneManager.GetActiveScene().name == "Final" && base.IsAchieved();
}
