using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ExpPoint : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _movingToPlayer = false;
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player")
                    .GetComponent<Transform>();
    }

    private void Update()
    {
        if (_movingToPlayer == true)
            transform.position =
                Vector2.MoveTowards(transform.position,
                                     GetPlayerCenter(_player.position),
                                     Time.deltaTime * _speed);

        if (Mathf.Abs( _player.position.x - transform.position.x ) < 0.5f)
        {
            _player.gameObject.GetComponent<Player>().ExpCollected?.Invoke(1);
            _player.gameObject.GetComponent<Player>().AmbientEvent?.Invoke();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
            _movingToPlayer = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
            _movingToPlayer = true;
    }

    private Vector3 GetPlayerCenter(Vector3 playerPosition)
    {
        return new Vector3(playerPosition.x, playerPosition.y + 1, playerPosition.z);
    }
}
