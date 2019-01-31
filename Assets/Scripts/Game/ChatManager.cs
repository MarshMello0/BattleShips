using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using System;
using TMPro;
using UnityEngine.UI;

public class ChatManager : ChatBehavior
{
    public bool inputHidden, outputHidden;
    [Space]
    [SerializeField]
    private TMP_InputField input;
    [SerializeField]
    private TextMeshProUGUI output;
    [SerializeField]
    private List<string> messages = new List<string>();
    [SerializeField]
    private int maxMessages = 100;
    [SerializeField]
    private ScrollRect scroll;
    [Space]
    [SerializeField]
    private KeyCode chatButton;
    private string username;
    private Color userColour;
    [Space]
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        gm.cm = this;
        ShowInput(false);
        ShowOutput(false);
        username = PlayerPrefs.GetString("username", "ERROR");
        userColour = new Color(PlayerPrefs.GetFloat("r", 255f), PlayerPrefs.GetFloat("g", 0f), PlayerPrefs.GetFloat("b", 0f));
    }

    protected override void NetworkStart()
    {
        base.NetworkStart();
        networkObject.SendRpc(RPC_PLAYER_JOINED, Receivers.All, username, userColour);
    }

    private void Update()
    {
        //Debug.Log(inputHidden + " | " + input.isFocused + " | " + input.text);
        if (Input.GetKeyDown(chatButton) && !gm.isPaused)
        {
            if (!inputHidden && input.text.Length > 0)
                Message();
            else if (!inputHidden)
                ShowInput(false);
            else if (inputHidden)
                ShowInput(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !inputHidden)
        {
            ShowInput(false);
        }
    }

    private void Message()
    {
        string message = input.text;
        networkObject.SendRpc(RPC_RECIVE_MESSAGE, Receivers.All, username, userColour, message);
        input.text = "";
        input.ActivateInputField();
        input.Select();
    }

    public override void ReciveMessage(RpcArgs args)
    {
        string name = args.GetNext<string>();
        Color colour = args.GetNext<Color>();
        string message = args.GetNext<string>();

        messages.Insert(0, MessageToString(name, colour, message));
        CheckMessages();
    }

    private void ShowOutput(bool state)
    {
        outputHidden = !state;
        output.gameObject.SetActive(state);
        if (state)
        {
            StartCoroutine(HideOutput());
        }
        else if (!state)
        {
            StopAllCoroutines();
        }
    }

    private void ShowInput(bool state)
    {
        inputHidden = !state;
        input.gameObject.SetActive(state);
        if (state)
        {
            input.ActivateInputField();
            input.Select();
        }
    }

    string MessageToString(string name, Color colour, string message)
    {
        string hex = ColorUtility.ToHtmlStringRGB(colour);
        return "<b> <color=#" + hex + ">" + name + ":</color> </b>" + message + "<br>";
    }

    IEnumerator HideOutput()
    {
        yield return new WaitForSeconds(5);
        ShowOutput(false);
    }

    public override void PlayerJoined(RpcArgs args)
    {
        string name = args.GetNext<string>();
        Color colour = args.GetNext<Color>();

        messages.Insert(0, Info(name, colour, " has joined the game"));
        CheckMessages();
    }

    private string Info(string name, Color colour, string message)
    {
        string hex = ColorUtility.ToHtmlStringRGB(colour);
        return "<b> <color=#" + hex + ">" + name + "</color></b>" + message + " <br>";
    }

    private void OnApplicationQuit()
    {
        networkObject.SendRpc(RPC_PLAYER_LEFT, Receivers.All, username, userColour);
    }

    public override void PlayerLeft(RpcArgs args)
    {
        string name = args.GetNext<string>();
        Color colour = args.GetNext<Color>();

        messages.Insert(0, Info(name, colour, " has left the game"));
        CheckMessages();
    }

    private void CheckMessages()
    {
        output.text = output.text + messages[0];

        if (messages.Count > maxMessages)
        {
            messages.RemoveAt(maxMessages + 1);
        }

        if (outputHidden)
            ShowOutput(true);
    }
}
