using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public bool CanMove { get; private set; } = true;
    public bool IsDead { get; private set; } = false;
    public bool IsInvulnerable = false;
    public int Health { get; private set; } = 2;
    public int Exp { get; private set; } = 0;
    public bool ComingToNewLevel = false;

    public UnityEvent AmbientEvent;
    public ExpCollected ExpCollected;
    public HealthChanged HealthChanged;
    public UnityEvent MobKilled;

    public DashAbility DashAbility;
    public StrikeAbility StrikeAbility;
    public InvulnerAbility InvulnerAbility;

    public GameUI GameUI;
    public Moving Moving;

    private static Player _instance;

    private void Start()
    {
        #region Should be removed after creating game manager
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);
        #endregion

        if (PlayerPrefs.HasKey("Exp"))
            Exp = PlayerPrefs.GetInt("Exp");

        if (PlayerPrefs.HasKey("Health"))
        {
            Health = PlayerPrefs.GetInt("Health");

            if (Health <= 0)
                Health = 2;
        }

        Moving = GetComponent<Moving>();
        DashAbility = GetComponent<DashAbility>();
        StrikeAbility = GetComponent<StrikeAbility>();
        InvulnerAbility = GetComponent<InvulnerAbility>();

        MobKilled = new UnityEvent();
        AmbientEvent.AddListener(PlayPopSound);
        ExpCollected.AddListener(CollectCoin);
        HealthChanged.AddListener(UpdateHealthData);

        GameUI = GameObject.FindGameObjectWithTag(nameof(GameUI))
            .GetComponent<GameUI>();

        GameUI.ExpText.text = $"Опыт: {Exp} поинтов";
        GameUI.HealthText.text = $"Здоровье: {Health}";

        // Extract to level configuration
        var level = SceneManager.GetActiveScene().name;

        if (level == "FirstLevel")
        {
            GetComponent<DashAbility>().IsEnabled = false;
            GetComponent<StrikeAbility>().IsEnabled = false;
            GameUI?.DisableDash();
            GameUI?.DisableStrike();
        }
        if (level.Contains("Second") || level.Contains("Third") || level.Contains("Fourth"))
        {
            GetComponent<StrikeAbility>().IsEnabled = false;
            GameUI?.DisableStrike();
        }

        if (GameObject.FindGameObjectWithTag("AchievPool") == null)
            Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/AchievmentPool"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && CanMove
            && DashAbility.IsEnabled && !DashAbility.IsActiveCooldown)
        {
            //InvulnerAbility.StartAbility();
            DashAbility.StartAbility();
            Moving.Dash();
            GameUI?.StartDashCooldown(DashAbility.GuiWaitTime);
        }

        if (Input.GetKeyDown(KeyCode.F) && CanMove && StrikeAbility.IsEnabled && !StrikeAbility.IsActiveCooldown)
        {
            Moving.Strike();
            StrikeAbility.StartAbility();
            GameUI?.StartStrikeCooldown(StrikeAbility.GuiWaitTime);
        }
    }

    public void Respawn()
    {
        CanMove = true;
        Health = 2;
        GameUI.UpdateHealth(Health);
        Moving.ResetAnimation();
        Moving.ResetTransform();
        IsDead = false;
    }

    public void Die()
    {
        if (!ComingToNewLevel && !IsDead && !IsInvulnerable)
        {
            Moving.Die();
            CanMove = false;
            IsDead = true;
            PlayOoffSound();
            StartCoroutine(RespawnCooldown(3f));
        }
    }

    private IEnumerator RespawnCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Respawn();
        yield return null;
    }

    public void ComeToNewLevel()
    {
        CanMove = false;
        Moving.SwithLevel();
    }

    public void Hurt()
    {
        if (IsInvulnerable)
            return;

        Health--;
        HealthChanged?.Invoke(-1);

        if (Health <= 0f)
            Die();
        else
            Moving.Hurt();
    }

    public void CollectCoin(int amount = 1)
    {
        //Debug.Log($"Current exp: {Exp} + {amount} {PlayerPrefs.GetInt("Exp", Exp)}");
        Exp += amount;
        PlayerPrefs.SetInt("Exp", Exp);
    }

    public void UpdateHealthData(int health = 2)
        => PlayerPrefs.SetInt("Health", health);

    private void PlayPopSound()
    {
        var sound = Instantiate(Resources.Load<GameObject>("Prefabs/PopSound"),
            transform.position,
            Quaternion.identity);

        Destroy(sound, 1);
    }

    private void PlayOoffSound()
    {
        var sound = Instantiate(Resources.Load<GameObject>("Prefabs/OoffSound"),
            transform.position,
            Quaternion.identity);

        Destroy(sound, 1);
    }
}
