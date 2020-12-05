using System.Collections;
using UnityEngine;

public class ArcherAttack : MonoBehaviour
{
    private Animator _animator;
    private Transform _player;
    public bool IsAlive;
    public float AttackCooldown;
    public float AttackDistance = 10f;

    private bool PlayerIsNear => Vector3.Distance(_player.position, transform.position) <= AttackDistance;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (AttackDistance <= 1f)
            AttackDistance = 10f;
        StartCoroutine(AttackEverySeconds(AttackCooldown));
    }

    private IEnumerator AttackEverySeconds(float seconds)
    {
        while (IsAlive)
        {
            yield return new WaitForSeconds(seconds);
            Debug.Log(Vector3.Distance(_player.position, transform.position));
            Debug.Log(Vector3.Distance(_player.position, transform.position) < AttackDistance);
            if (PlayerIsNear)
            {
                _animator.SetTrigger("attack");
                ShootAt(_player.position);
            }
        }
    }

    private void ShootAt(Vector3 target)
    {
        var arrowPrefab = Resources.Load("Prefabs/Arrow");
        var arrowPosition = new Vector3(transform.position.x-1, transform.position.y, transform.position.z);
        var arrow = Instantiate(arrowPrefab, arrowPosition, Quaternion.identity);
        ((GameObject)arrow).GetComponent<ShootingArrow>().Shoot(target);
    }

    

}
