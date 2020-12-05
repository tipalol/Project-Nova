using UnityEngine;
using UnityEngine.Events;

public class Statistics
{
    internal int MobKilledCount {
        get =>  _mobKill;
        private set { _mobKill = value; StatsUpdated?.Invoke(); } }

    private Player _player;
    private static Statistics _instance;
    private int _mobKill;

    public UnityEvent StatsUpdated;

    private Statistics()
    {
        // We initialize Stat once and attach to the player from first level
        // TODO: fix this
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _player.MobKilled.AddListener(IncreaseKillCounter);
        StatsUpdated = new UnityEvent();
        //Debug.Log($"Статистика загружена. {this} {_player} {_player.MobKilled}");
        
    }

    internal static Statistics GetInstance()
    {
        if (_instance == null)
            _instance = new Statistics();

        return _instance;
    }

    internal void Reset()
    {
        MobKilledCount = 0;
    }

    private void IncreaseKillCounter()
    {
        MobKilledCount += 1;
        Debug.Log($"Кол-во убитых мобов увеличено на 1");
    }
}
// LoadLevel FifthLevel