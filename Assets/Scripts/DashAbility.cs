using System;
using UnityEngine;

public class DashAbility : Ability
{
    public override void ActivateAbility()
    {
        Debug.Log($"This is Dash. {name}");
    }
}
