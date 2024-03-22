
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Fusion;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    public int getplayer=0;
    public List<GameObject> playerItem=new List<GameObject>();
    public Transform poitspaw;
    GameObject aa;
    public NetworkRunner runner1;
    public GameObject Enemy;
    public List<Transform> transpoitEne=new List<Transform>();
    public bool checkplayerin;
    [Networked] private TickTimer delay { get; set; }
    PlayerControler[] playerCC;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        aa= Instantiate(playerItem[getplayer], poitspaw.position, playerItem[getplayer].transform.rotation);
    }
    public void OnclickChanPlayer()
    {
        Destroy(aa);
        getplayer++;
        if (getplayer ==4)
        {
            getplayer= 0;
        }
        aa = Instantiate(playerItem[getplayer], poitspaw.position, playerItem[getplayer].transform.rotation);
    }
    private void FixedUpdate()
    {
        playerCC=FindObjectsOfType<PlayerControler>();
      if (playerCC.Length ==4)
        {
            checkplayerin= true;
        }
    }
    public override void FixedUpdateNetwork()
    {
        if (delay.ExpiredOrNotRunning(Runner))
        {
            if (checkplayerin)
            {
                int i = Random.Range(0, transpoitEne.Count);
                Runner.Spawn(Enemy, transpoitEne[i].position, Quaternion.identity, Object.InputAuthority, (Runner, oo) =>
                {
                    oo.GetComponent<Enemy>().creatlife();
                });
            }
            delay = TickTimer.CreateFromSeconds(Runner, 6f);
        }
    }
    public void LoadScene()//button
    {
        Loadload();
    }
    public void Loadload()
    {
        runner1.Shutdown();
        SceneManager.LoadScene(0);
    }
}
