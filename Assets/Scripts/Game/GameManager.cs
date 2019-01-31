using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Network manager is used to spawn items in over the network
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject hostPrefab;

    [SerializeField] private GameObject pauseMenu;
    public bool isPaused;
    [SerializeField] private KeyCode pauseButton = KeyCode.P;

    [HideInInspector]
    public Player localPlayer;

    [HideInInspector]
    public ChatManager cm;

    private IEnumerator Start()
    {
        networkManager = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
        yield return networkManager;
        if (networkManager.IsServer)
            Host();
        else
            Client();

        networkManager.Networker.disconnected += disconnected;
    }

    private void disconnected(NetWorker sender)
    {
        //This is to wait till they have fully disconnected before switching scenes
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(pauseButton) && cm.inputHidden)
        {
            if (isPaused)
            {
                //Unpausing
                isPaused = false;
                
                
            }
            else
            {
                //Pausing
                isPaused = true;
            }
            pauseMenu.SetActive(isPaused);
        }
    }

    private void Host()
    {
        Instantiate(hostPrefab);
        networkManager.InstantiateChat(0);
        networkManager.InstantiateLeaderboard(0);
    }

    private void Client()
    {
        networkManager.InstantiatePlayer(0);
    }

    public void Shoot(Vector3 pos, float angle, uint id)
    {
        GameObject cannon = networkManager.InstantiateCannonBall(0,pos,Quaternion.Euler(0,0,angle),true).gameObject;
        cannon.GetComponent<CannonBall>().ID = id;
    }

    public void Quit()
    {
        localPlayer.networkObject.Destroy();
        networkManager.Disconnect();
    }


}
