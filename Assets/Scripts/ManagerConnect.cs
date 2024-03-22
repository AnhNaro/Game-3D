using Fusion;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class ManagerConnect : SimulationBehaviour,IPlayerJoined
{
    public Text txtwarning;
    public Text txtIdSi;
    public Text txtIdSiJoin;
    public Text txtnameplayer;
    public InputField JoinSisionName;
    public InputField NamePlayer;
    public List<GameObject> playerHome=new List<GameObject>();
    private NetworkRunner _runner;
    int idr;
    public Button btnStartGame;
    public Button btnJoinGame;
    public Button btnRandomGame;
    public GameObject PanelLoddy;
    bool checkonoffDoor = true;
    public GameObject panelName;
    private void Awake()
    {
        btnStartGame.onClick.AddListener(() =>
        {
            Onclick_InSision();
            btnStartGame.enabled = false;
        });
        btnJoinGame.onClick.AddListener(() =>
        {
            Onclick_JoinSision();
            btnJoinGame.enabled = false;
        });
        btnRandomGame.onClick.AddListener(() =>
        {
            Onclick_RandomSision();
            btnRandomGame.enabled = false;
        });
    }
    private void Start()
    {
        _runner=GetComponent<NetworkRunner>();
    }
    private void Update()
    {
        if (testinternet > 0)
        {
            txtwarning.text = ("Khong The Ket Noi, Hay Thu Lai");
        }
    }
    public void OnDoor()
    {
        checkonoffDoor = true;
    }
    public void offDoor()
    {
        checkonoffDoor=false;
    }
    //in sision
    public void Onclick_InSision()
    {
        StartGame(GameMode.Shared);
    }
    async void StartGame(GameMode mode)
    {
        idr = Random.Range(10, 100000);
        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var Infoscene = new NetworkSceneInfo();
        if(scene.IsValid)
        {
            Infoscene.AddSceneRef(scene,LoadSceneMode.Additive);
        }
        var result = await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            Scene = scene,
            PlayerCount = 4,
            SessionName = idr.ToString(),
            IsOpen = true,
            IsVisible = checkonoffDoor,
        }) ;
        if (result.Ok)
        {
            PanelLoddy.SetActive(false);
            testinternet = 0;//check info internet
            txtIdSi.text = $"ID: {idr}";
            txtwarning.text = " ";
        }
        else
        {
            btnStartGame.enabled= true;
            txtwarning.text = ("Khong The Ket Noi, Hay Thu Lai");
            SceneManager.LoadScene(0);
            testinternet++;
        }
    }
    //Join
    public void Onclick_JoinSision()
    {
        StartJoinGame(GameMode.Shared);
    }
    async void StartJoinGame(GameMode mode)
    {
        //var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        //var Infoscene = new NetworkSceneInfo();
        //if (scene.IsValid)
        //{
        //    Infoscene.AddSceneRef(scene, LoadSceneMode.Additive);
        //}
        var result=await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
          //  Scene = scene,
            SessionName = JoinSisionName.text,
        });
        if (result.Ok)
        {
            txtIdSiJoin.text ="ID: "+JoinSisionName.text;
            PanelLoddy.SetActive(false);
            testinternet = 0;
            txtwarning.text = " ";
        }
        else
        {
            btnJoinGame.enabled = true;
            txtwarning.text = ("Phong Khong Hop Le");
            testinternet++;
            SceneManager.LoadScene(0);
        }
    }
    //Join Random
    public void Onclick_RandomSision()
    {
         StartRandomGame(GameMode.Shared);
    }
    async void StartRandomGame(GameMode mode)
    {
        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var Infoscene = new NetworkSceneInfo();
        if (scene.IsValid)
        {
            Infoscene.AddSceneRef(scene, LoadSceneMode.Additive);
        }
        var result = await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            Scene = scene,
        });
        if (result.Ok)
        {
            PanelLoddy.SetActive(false);
            testinternet = 0;
            txtwarning.text = " ";
        }
        else
        {
            SceneManager.LoadScene(0);
            txtwarning.text = ("Khong The Ket Noi, Hay Thu Lai");
            testinternet++;
        }
    }
    public void PlayerJoined(PlayerRef player)
    {
        if(player==Runner.LocalPlayer)
         {
            int a = Random.Range(2,-3);
            int b = Random.Range(-1,-5);
            Runner.Spawn(playerHome[GameManager.Instance.getplayer], new Vector3(a, 0, b), Quaternion.identity);
         }
    }
    public int testinternet
    {
        get => PlayerPrefs.GetInt("test", 0);
        set => PlayerPrefs.SetInt("test", value);
    }
}
