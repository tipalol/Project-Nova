using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image DashIcon;
    public Image StrikeIcon;
    public TMPro.TextMeshProUGUI ExpText;
    public TMPro.TextMeshProUGUI HealthText;
    public Player Player;

    private const float _disabledAbilityAlpha = 0.5f;
    private const float _enabledAbilityAlpha = 1f;

    public void UpdateExp(int exp = 1)
    {
        ExpText.text = $"Опыт: {Player.Exp} поинтов";
    }

    public void UpdateHealth(int health)
    {
        HealthText.text = $"Здоровье: {Player.Health}";
    }

    public void DisableStrike() => ChangeStrikeIconAlpha(_disabledAbilityAlpha);
    public void EnableStrike() => ChangeStrikeIconAlpha(_enabledAbilityAlpha);
    public void DisableDash() => ChangeDashIconAlpha(_disabledAbilityAlpha);
    public void EnableDash() => ChangeDashIconAlpha(_enabledAbilityAlpha);

    public void StartDashCooldown(float seconds)
    {
        StartCoroutine(DashCooldown(seconds));
        DisableDash();
    }

    public void StartStrikeCooldown(float seconds)
    {
        StartCoroutine(StrikeCooldown(seconds));
        DisableStrike();
    }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Player.ExpCollected.AddListener(UpdateExp);
        Player.HealthChanged.AddListener(UpdateHealth);
    }

    private IEnumerator StrikeCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        EnableStrike();
        yield return null;
    }

    private IEnumerator DashCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        EnableDash();
        yield return null;
    }

    private void ChangeStrikeIconAlpha(float alpha)
    {
        var oldColor = StrikeIcon.color;
        StrikeIcon.color = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
    }

    private void ChangeDashIconAlpha(float alpha)
    {
        var oldColor = DashIcon.color;
        DashIcon.color = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
    }
}
