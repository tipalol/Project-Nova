using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private string _nextSceneName;

    public void Load()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!player.IsDead)
        {
            player.ComingToNewLevel = true;
            player.ComeToNewLevel();
            PlayerPrefs.SetString("LastSceneName", _nextSceneName);
            Initiate.Fade(_nextSceneName, Color.black, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Load();
        }
    }
}
