using System;
using System.Collections;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] private float _abilityDelay;
    [SerializeField] private float _cooldownTime;
    internal bool IsActiveCooldown { get; set; }
    internal float GuiWaitTime => _abilityDelay + _cooldownTime;
    internal bool IsEnabled { get; set; } = true;

    public void StartAbility()
    {
        if (!IsActiveCooldown || IsEnabled)
            StartCoroutine(Delay(_abilityDelay));
    }

    private IEnumerator Delay(float seconds = 0f)
    {
        IsActiveCooldown = true;
        yield return new WaitForSeconds(seconds);
        ActivateAbility();
        StartCoroutine(StartCooldown(_cooldownTime));
        yield return null;
    }

    private IEnumerator StartCooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        IsActiveCooldown = false;
        yield return null;
    }

    public abstract void ActivateAbility();
}
