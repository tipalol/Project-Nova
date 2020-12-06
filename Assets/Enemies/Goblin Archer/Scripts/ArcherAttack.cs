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
            if (IsRightDirection() == false)
                transform.localScale = new Vector3(-transform.localScale.x,
                                            transform.localScale.y,
                                            transform.localScale.z);

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
        var arrowX = transform.localScale.x < 0 ? transform.position.x-1 : transform.position.x+1;
        var arrowPosition = new Vector3(arrowX, transform.position.y, transform.position.z);
        var arrow = Instantiate(arrowPrefab, arrowPosition, Quaternion.identity);
        ((GameObject)arrow).GetComponent<ShootingArrow>().Rotate(-(int)GetXDirection().x);
        ((GameObject)arrow).GetComponent<ShootingArrow>().Shoot(target);
    }

    
    private Vector2 GetXDirection()
    {
        Vector2 direction;
        if (transform.position.x - _player.position.x < 0)
            direction = Vector2.right; // {1, 0, 0}
        else
            direction = Vector2.left; // {-1, 0, 0}

        return direction;
    }

    private bool IsRightDirection()
    {
        var xDirection = GetXDirection();
        if (transform.localScale.x < 0)
            return xDirection.x < 0;
        else
            return xDirection.x > 0;
    }

}
