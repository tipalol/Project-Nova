using System.Collections;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    private Rigidbody2D _rigidbody2d;
    private Collider2D _collider2d;
    private Player _player;

    [SerializeField] public float _jumpForce;
    [SerializeField] private LayerMask GroundLayer;

    private void Start()
    {
        _rigidbody2d = GetComponent<Rigidbody2D>();
        _collider2d = GetComponent<Collider2D>();
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded && !_player.IsDead && _player.CanMove)
            _rigidbody2d.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    //Проверяет, стоим ли мы на земле
    private bool IsGrounded
    {
        get
        {
            //Насколько ниже нашего колайдера проверяем
            float extraHeight = 0.3f;
            //Создаем невидимый квадрат вокруг нашего персонажа и чуть ниже
            RaycastHit2D hit = Physics2D.BoxCast(_collider2d.bounds.center, _collider2d.bounds.size, 0f, Vector2.down, extraHeight, GroundLayer);

            //Если квадрат столкнулся с землей, то возвращаем правду (true), иначе ложь (false)
            return hit.collider != null;
        }
    }
}
