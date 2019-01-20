using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking.Unity;
using BeardedManStudios.Forge.Networking;

public class GameManager : MonoBehaviour
{
    //Network manager is used to spawn items in over the network
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject hostPrefab;

    private IEnumerator Start()
    {
        networkManager = GameObject.FindWithTag("NetworkManager").GetComponent<NetworkManager>();
        yield return networkManager;
        if (networkManager.IsServer)
            Host();
        else
            Client();
    }

    private void Host()
    {
        Instantiate(hostPrefab);
    }

    private void Client()
    {
        networkManager.InstantiatePlayer(0);
    }

    public void Shoot(Vector3 pos, float angle)
    {
        networkManager.InstantiateCannonBall(0,pos,Quaternion.Euler(0,0,angle),true);
    }
}
