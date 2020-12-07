using UnityEngine;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{
    public string LastSceneName;
    public GameObject ContinueButton;

    public GameObject MainPanel;
    public GameObject OptionsPanel;

    public void Start()
    {
        MainPanel.SetActive(true);
        OptionsPanel.SetActive(false);

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

    public void Options()
    {
        MainPanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }

    public void Back()
    {
        MainPanel.SetActive(true);
        OptionsPanel.SetActive(false);
    }

    public void ResolutionChanged(int resolution)
    {
        Screen.SetResolution(800, 600, true);
        Debug.Log(resolution);
    }
}
