using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private Slider redSlider, greenSlider, blueSlider;
    [SerializeField]
    private SpriteRenderer sailRender;

    [Space]

    [SerializeField]
    private Button joinGame;
    [SerializeField]
    private TMP_InputField username;

    [Space]
    private Resolution[] resolutions;
    [SerializeField]
    private TMP_Dropdown resDropdown;
    [SerializeField]
    private TextMeshProUGUI fullscreenText;

    [Space]

    [SerializeField] private MultiplayerMenu mm;

    private void Start()
    {
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();
        foreach (Resolution res in resolutions)
        {
            options.Add(res.width + " x " + res.height);
        }
        resDropdown.AddOptions(options);
        resDropdown.onValueChanged.AddListener(delegate { ChangeResolution(); });
    }

    public void ToggleFullScreen()
    {
        if (Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            fullscreenText.text = "Fullscreen: False";
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            fullscreenText.text = "Fullscreen: True";
        }
    }

    public void ChangeResolution()
    {
        int number = resDropdown.value;
        string text = resDropdown.options[number].text;
        int width = int.Parse(text.Split(' ')[0]);
        int height = int.Parse(text.Split(' ')[2]);
        Screen.SetResolution(width, height, Screen.fullScreen);
    }

    public void Quit()
    {
        Application.Quit();
    }

    private void Update()
    {
        SliderValues();
        JoinButton();
    }

    private void SliderValues()
    {
        sailRender.color = new Color(redSlider.value / 255f, greenSlider.value / 255f, blueSlider.value / 255f);
    }

    private void JoinButton()
    {
        if (ValidToJoin() && !joinGame.interactable)
        {
            joinGame.interactable = true;
        }
        else if (!ValidToJoin() && joinGame.interactable)
        {
            joinGame.interactable = false;
        }
    }

    private bool ValidToJoin()
    {
        if (username.text != "")
        {
            return true;
        }
        else
            return false;
    }

    public void Connect()
    {
        PlayerPrefs.SetString("username",username.text);
        PlayerPrefs.SetFloat("r", redSlider.value);
        PlayerPrefs.SetFloat("g", greenSlider.value);
        PlayerPrefs.SetFloat("b", blueSlider.value);
        PlayerPrefs.Save();

        mm.Connect();
    }
    
}
