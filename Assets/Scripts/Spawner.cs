using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Объект, который будет спавниться")]
    [SerializeField] private GameObject _spawningPrefab;

    [Header("Сколько секунд надо ждать до спауна")]
    [Min(1f)]
    [SerializeField] private float _cooldown = 1f;

    [Header("Стартовая координата x")]
    [SerializeField] private float _spawningX = 10f;

    [Header("На какой высоте спавнить")]
    [SerializeField] private float _spawningY = 16f;

    [Header("Какой разброс по x")]
    [Min(0f)]
    [SerializeField] private float _spawningRadius = 20f;

    [Header("Через какое время уничтожить объект")]
    [Min(0.1f)]
    [SerializeField] private float _destroyTime = 20f;
    
    private void Start()
    {
        if (_spawningPrefab == null)
             _spawningPrefab = Resources.Load<GameObject>("Prefabs/Orc");
        StartCoroutine(SpawnEverySeconds(_cooldown));
    }

    private IEnumerator SpawnEverySeconds(float seconds)
    {
        while (true)
        {
            var newX = _spawningX + Random.Range(-_spawningRadius, _spawningRadius);
            var position = new Vector3(newX, _spawningY, 0);
            var spawned = Instantiate(_spawningPrefab, position, Quaternion.identity);
            Destroy(spawned, _destroyTime);
            yield return new WaitForSeconds(seconds);
        }
    }
}
