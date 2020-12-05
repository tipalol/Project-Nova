using UnityEngine;

public class RespawnTrigger : MonoBehaviour
{
    //Когда сработал триггер
    private void OnTriggerEnter2D(Collider2D other)
    {
        //Временная переменная, чтобы запомнить, кто вошел
        Respawnable respawnable = null;

        Destroyable destroyable = null;

        //Запоминаем, кто вошел и, если он имеет скрипт Respawnable
        if (other.gameObject.TryGetComponent(out respawnable) != false)
        {
            //То вызываем его метод Respawn()
            respawnable.Respawn();
        }

        if (other.gameObject.TryGetComponent(out destroyable) != false)
        {
            destroyable.Destroy();
        }
    }
}
