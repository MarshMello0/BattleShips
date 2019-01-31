using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using BeardedManStudios.Forge.Networking.Unity;
using System;

public class LeaderboardManager : LeaderboardBehavior
{
    [SerializeField]
    private Transform gridHolder;
    [SerializeField]
    private GameObject prefab;

    private List<PlayerEntry> leaderboard = new List<PlayerEntry>();
    //leaderboard.Sort((x, y) => x.score.CompareTo(y.score));

    protected override void NetworkStart()
    {
        base.NetworkStart();
        NetWorker netWorker = NetworkManager.Instance.Networker;
        if (netWorker.IsServer)
        {
            netWorker.playerConnected += playerConnected;
            netWorker.playerDisconnected += playerDisconnected;
        }
            

    }

    private void playerDisconnected(NetworkingPlayer player, NetWorker sender)
    {
        foreach (PlayerEntry entry in leaderboard)
        {
            if (entry.id == player.NetworkId)
            {
                networkObject.SendRpc(RPC_REMOVE_PLAYER, Receivers.All, entry.id);
            }
        }
    }

    private void playerConnected(NetworkingPlayer player, NetWorker sender)
    {
        Debug.Log("Player has connected " + player.NetworkId);

        foreach (PlayerEntry entry in leaderboard)
        {
            networkObject.SendRpc(player, RPC_ADD_PLAYER_WITH_SCORE, entry.id, entry.username, entry.score);
        }
    }

    public void SendInfo(uint ID, string username)
    {
        networkObject.SendRpc(RPC_ADD_PLAYER, Receivers.All, ID, username);
    }


    public void UpdateScore(uint id)
    {
        networkObject.SendRpc(RPC_UPDATE_SCORE, Receivers.Owner, id,0);
    }

    private void SortLeaderboard()
    {
        leaderboard.Sort(SortByScore);

        for (int i = 0; i < leaderboard.Count; i++)
        {
            leaderboard[i].playerEntry.transform.SetSiblingIndex(leaderboard[i].playerEntry.transform.parent.childCount);
        }
    }

    static int SortByScore(PlayerEntry p1, PlayerEntry p2)
    {
        return p2.score.CompareTo(p1.score);
    }
    #region RPC's
    /*
    All the RPC calls should be client side only so that the host
    keeps all of the real data for the leaderboard so that
    clients cant change their scores on the leaderboard with
    simple things such as cheat engine.

    The goal of these RPC's is just to update the client's leaderboards
    */
    public override void AddPlayer(RpcArgs args)
    {
        uint id = args.GetNext<uint>();
        string name = args.GetNext<string>();
        PlayerEntry newPlayer = new PlayerEntry();
        newPlayer.id = id;
        newPlayer.score = 0;
        newPlayer.playerEntry = Instantiate(prefab);
        newPlayer.playerEntry.transform.SetParent(gridHolder, false);
        newPlayer.playerEntry.transform.SetSiblingIndex(newPlayer.playerEntry.transform.childCount);//This should add it to the bottom
        newPlayer.playerEntry.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        newPlayer.playerEntry.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "0";
        newPlayer.username = name;
        leaderboard.Add(newPlayer);
    }

    public override void UpdateScore(RpcArgs args)
    {
        uint id = args.GetNext<uint>();
        int newscore = args.GetNext<int>();
        if (networkObject.IsOwner)
        {
            foreach (PlayerEntry entry in leaderboard)
            {
                if (entry.id == id)
                {
                    entry.score++;
                    networkObject.SendRpc(RPC_UPDATE_SCORE, Receivers.Others, id, entry.score);
                    Debug.Log("Updated player over the network");
                }
                entry.playerEntry.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = entry.score.ToString();
                Debug.Log("Updating score for " + entry.username + " who has a id of " + entry.id + " | " + id);
            }
            SortLeaderboard();
        }
        else
        {
            foreach (PlayerEntry entry in leaderboard)
            {
                if (entry.id == id)
                {
                    entry.score = newscore;
                }
                entry.playerEntry.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = entry.score.ToString();
            }
            SortLeaderboard();
        }


        
    }

    public override void AddPlayerWithScore(RpcArgs args)
    {
        //This will only be ran on the client who just joined mid game
        uint id = args.GetNext<uint>();
        string name = args.GetNext<string>();
        int score = args.GetNext<int>();
        PlayerEntry newPlayer = new PlayerEntry();
        newPlayer.id = id;
        newPlayer.score = score;
        newPlayer.playerEntry = Instantiate(prefab);
        newPlayer.playerEntry.transform.SetParent(gridHolder, false);
        newPlayer.playerEntry.transform.SetSiblingIndex(newPlayer.playerEntry.transform.childCount);//This should add it to the bottom
        newPlayer.playerEntry.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;
        newPlayer.playerEntry.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = score.ToString();
        newPlayer.username = name;
        leaderboard.Add(newPlayer);
    }

    public override void RemovePlayer(RpcArgs args)
    {
        uint id = args.GetNext<uint>();
        int position = 0;
        for (int i = 0; i < leaderboard.Count; i++)
        {
            if (leaderboard[i].id == id)
            {
                GameObject objectToDestory = leaderboard[i].playerEntry;
                MainThreadManager.Run(() =>
                {
                    lock (leaderboard)
                    {
                        Destroy(objectToDestory); 
                    }
                });
                Debug.Log("Removed Player with ID of " + id);
                position = i;
            }
        }

        leaderboard.RemoveAt(position);
    }
    #endregion
}

public class PlayerEntry
{
    public uint id; //This is there networkObject.MyPlayerId
    public int score; //This is there current score
    public GameObject playerEntry = null; //This is the gameobject in the scene so I can find it easily
    public string username = "Player0001"; //This is their display name
}
