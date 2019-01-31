using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.IO;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    private Slider redSlider, greenSlider, blueSlider;
    [SerializeField]
    private SpriteRenderer sailRender;

    [Space]

    [SerializeField]
    private Button[] joinGame;
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

    [Space]
    [SerializeField] private TMP_InputField ipAdress;

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
        CheckCommands();
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
        if (ValidToJoin() && !joinGame[0].interactable)
        {
            //I know there is always going to be two join game buttons
            joinGame[0].interactable = true;
            joinGame[1].interactable = true;
        }
        else if (!ValidToJoin() && joinGame[0].interactable)
        {
            joinGame[0].interactable = false;
            joinGame[1].interactable = false;
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

    public void Connect(bool lan)
    {
        PlayerPrefs.SetString("username",username.text);
        PlayerPrefs.SetFloat("r", redSlider.value);
        PlayerPrefs.SetFloat("g", greenSlider.value);
        PlayerPrefs.SetFloat("b", blueSlider.value);
        PlayerPrefs.Save();

        if (lan)
        {
            mm.LanConnect();
        }
        else
        {
            mm.NetworkConnect(ipAdress.text);
        }
    }

    private void CheckCommands()
    {
        string[] args = Environment.GetCommandLineArgs();
        foreach (string arg in args)
        {
            Debug.Log(arg);
            if (arg == "host")
            {
                Debug.Log("Hosting Server!");
                mm.Host();
            }
        }
    }
    
}
