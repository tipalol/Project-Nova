using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Moving : MonoBehaviour
{
    private Transform _transform;
    private Rigidbody2D _rigidbody2d;
    private BoxCollider2D _collider;
    [SerializeField] public float _speed;
    private Animator _animator;
    public Transform RespawnPoint;
    private Player _player;
    
    internal int Direction => transform.localScale.x > 0 ? 1 : -1;

    void Start()
    {
        _transform = GetComponent<Transform>();
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _player = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.D) && _player.CanMove)
        {
            var velocity = _rigidbody2d.velocity;
            _rigidbody2d.velocity = new Vector2() { x = _speed, y = velocity.y};

            _animator.SetBool("isRunning", true);

            if (transform.localScale.x < 0)
            {
                var scale = transform.localScale;
                transform.localScale = new Vector2() { x = scale.x * (-1), y = scale.y};
            }
            
        }
        if (Input.GetKey(KeyCode.A) && _player.CanMove)
        {
            var velocity = _rigidbody2d.velocity;
            _rigidbody2d.velocity = new Vector2() { x = -_speed, y = velocity.y};
            _animator.SetBool("isRunning", true);
            if (transform.localScale.x > 0)
            {
                var scale = transform.localScale;
                transform.localScale = new Vector2() { x = scale.x * (-1), y = scale.y};
            }
        }
        
        if (Mathf.Abs(_rigidbody2d.velocity.x) < .5f)
            _animator.SetBool("isRunning", false);
    }
    
    public void ResetAnimation()
    {
        _animator.ResetTrigger("death");
        _animator.Play("Default_Idle");

    }

    public void ResetTransform()
    {
        _rigidbody2d.constraints = RigidbodyConstraints2D.FreezeRotation;
        Vector3 eulerRotation = _transform.rotation.eulerAngles;
        _transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);

        transform.position = RespawnPoint.position;
        _rigidbody2d.velocity = new Vector2(0, 0);
    }

    public void Push(int direction = 1, int accelerationMultiplyer = 20) // direction: <- -1  1 -> 
        => _rigidbody2d.AddForce(Vector2.right * direction * _speed * accelerationMultiplyer, ForceMode2D.Impulse);

    public void Dash()
    {
        Push(Direction);
        _animator.SetTrigger("attack1");
    }

    public void Strike()
    {
        _animator.SetTrigger("attack2");
    }

    public void Die()
    {
        _animator.SetTrigger("death");
        _rigidbody2d.constraints = RigidbodyConstraints2D.None;
    }

    public void SwithLevel()
    {
        _collider.isTrigger = true;
        _rigidbody2d.velocity = new Vector2(0, 0);
        _rigidbody2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Hurt()
    {
        _animator.SetTrigger("hurt");
    }
}
