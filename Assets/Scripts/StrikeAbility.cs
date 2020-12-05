using System.Collections;
using UnityEngine;

public class StrikeAbility : Ability
{
    [SerializeField] private LayerMask _collideLayer;
    [SerializeField] private float _abilityDistance;

    public override void ActivateAbility()
    {
        if (!GetComponent<Player>().IsDead)
        {
            var ray = Physics2D.Raycast(CalculateCenterPosition(),
                Vector2.right * GetComponent<Moving>().Direction, _abilityDistance, _collideLayer);

            if (ray.collider == null)
                return;

            Destroy(ray.collider.gameObject);

            Debug.Log($"Senior debug: {GetComponent<Player>().MobKilled}");
            //Debug.Log(GetComponent<Player>() + " _ " + GetComponent<Player>().MobKilled.GetPersistentEventCount());
            //GetComponent<Player>().MobKilled?.AddListener(() => { Debug.Log("Dimon is 2 smart ass"); });
            //GetComponent<Player>().MobKilled?.Invoke();
            //var player1rdy = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
            //player1rdy.MobKilled?.Invoke();
        }
    }

    private Vector3 CalculateCenterPosition()
        => new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
}
