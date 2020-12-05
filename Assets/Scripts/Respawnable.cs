using UnityEngine;

public class Respawnable : MonoBehaviour
{
    //Точка респауна
    public Vector3 RespawnPosition;

    //Наш компонент Трансформ
    private Transform _self;

    //Выполняется при старте уровня
    private void Start()
    {
        _self = GetComponent<Transform>();
        RespawnPosition = _self.position;
    }

    public void Respawn()
    {
        _self.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        _self.position = RespawnPosition;

        Player mb;
        if (TryGetComponent(out mb))
            mb.Respawn();
    }
}
