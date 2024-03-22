using Fusion;
using Fusion.Sockets;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager2 : NetworkBehaviour
{
    float hh = 480f;
    public Text TxtTimeGameplay;
    public GameObject WinTeam;
    public GameObject LossTeam;
    public NetworkRunner runner2;
    PlayerControler[] playerPlay;
    bool ccc;
    bool checkWinloss;
    float timebackhome;

    private void FixedUpdate()
    {
        playerPlay = FindObjectsOfType<PlayerControler>();
        if (playerPlay.Length == 4)
        {
            ccc = true;
        }
        if (ccc)
        {
            hh -= Time.deltaTime;
            float a = hh / 60;
            float b = hh % 60;
            int bb = (int)b;
            int aa = (int)a;
            TxtTimeGameplay.text = string.Format($"{aa}:{bb}");
            if (playerPlay.Length <= 0 && (aa >= 0 || bb >= 0))
            {
                ShowLossRpc();
                checkWinloss=true;
            }
            if (playerPlay.Length > 0 && (aa <= 0 && bb <= 0))
            {
                ShowWinRpc();
                checkWinloss = true;
            }
        }
        if (checkWinloss)
        {
            timebackhome += Time.deltaTime;
            if(timebackhome >3)
            {
            runner2.Shutdown();
            SceneManager.LoadScene(0);
            }
        }
    }
    [Rpc(RpcSources.StateAuthority,RpcTargets.All)]
    public void ShowLossRpc()
    {
        LossTeam.gameObject.SetActive(true);    
    }
    [Rpc(RpcSources.StateAuthority,RpcTargets.All)]
    public void ShowWinRpc()
    {
        WinTeam.gameObject.SetActive(true);
    }
}
