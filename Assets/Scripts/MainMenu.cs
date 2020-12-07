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
        switch (resolution)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true);
            break;
            case 1:
                Screen.SetResolution(800, 600, true);
            break;
            default:
                Screen.SetResolution(400, 300, true);
            break;
        }
        
        Debug.Log(resolution);
    }
}
