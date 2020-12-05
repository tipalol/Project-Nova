using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string LastSceneName;
    public GameObject ContinueButton;

    public void Start()
    {
        if (PlayerPrefs.HasKey("LastSceneName"))
            LastSceneName = PlayerPrefs.GetString("LastSceneName");
        else
        {
            LastSceneName = "FirstLevel";
            ContinueButton.GetComponent<Button>().interactable = false;
            ContinueButton.GetComponent<Image>().color = new Color(145, 135, 135);
        }
    }

    public void NewGame()
    {
        Initiate.Fade("FirstLevel", Color.black, 1f);
        PlayerPrefs.SetString("LastSceneName", "FirstLevel");
        PlayerPrefs.SetFloat("Exp", 0);
    }

    public void Continue()
    {
        Initiate.Fade(LastSceneName, Color.black, 1f);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
