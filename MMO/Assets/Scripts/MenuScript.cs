using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UdpKit;

public class MenuScript : MonoBehaviour
{
    GameObject MainMenu;
    GameObject PlayMenu;
    GameObject OptionsMenu;
    GameObject TeamMenu;
    GameObject JoinGameMenu;
    GameObject AudioMenu;
    GameObject VideoMenu;
    GameObject ControlsMenu;

    public static bool hasPickedTeamOne = false;
    public static bool hasPickedTeamTwo = false;
    bool isServer = false;
    bool isClient = false;

    string map;
    string serverAddress = "";
    int serverPort = 27000;

	// Use this for initialization
	void Start ()
    {
        #region Find GameObject
        MainMenu = transform.Find("Main").gameObject;
        PlayMenu = transform.Find("Play").gameObject;
        OptionsMenu = transform.Find("Options").gameObject;

        TeamMenu = transform.Find("TeamSelect").gameObject;
        JoinGameMenu = transform.Find("JoinGame").gameObject;

        AudioMenu = transform.Find("Audio").gameObject;
        VideoMenu = transform.Find("Video").gameObject;
        ControlsMenu = transform.Find("Controls").gameObject;

        MainMenu.SetActive(true);
        PlayMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        TeamMenu.SetActive(false);
        JoinGameMenu.SetActive(false);
        AudioMenu.SetActive(false);
        VideoMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        #endregion
    }

    void Awake()
    {
        serverPort = BoltRuntimeSettings.instance.debugStartPort;
    }

    #region GUI Functions
    public void Play()
    {
        MainMenu.SetActive(false);
        PlayMenu.SetActive(true);
    }
    public void Options()
    {
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void BackToMain()
    {
        MainMenu.SetActive(true);
        PlayMenu.SetActive(false);
        OptionsMenu.SetActive(false);
    }
    public void NewGame()
    {
        isServer = true;
        isClient = false;
        PlayMenu.SetActive(false);
        TeamMenu.SetActive(true);
    }
    public void JoinGame()
    {
        isServer = false;
        isClient = true;
        PlayMenu.SetActive(false);
        JoinGameMenu.SetActive(true);
    }
    public void BackToPlay()
    {
        PlayMenu.SetActive(true);
        TeamMenu.SetActive(false);
        JoinGameMenu.SetActive(false);
    }
    public void Audio()
    {
        OptionsMenu.SetActive(false);
        AudioMenu.SetActive(true);
    }
    public void Video()
    {
        OptionsMenu.SetActive(false);
        VideoMenu.SetActive(true);
    }
    public void Controls()
    {
        OptionsMenu.SetActive(false);
        ControlsMenu.SetActive(true);
    }
    public void BackToOptions()
    {
        OptionsMenu.SetActive(true);
        AudioMenu.SetActive(false);
        VideoMenu.SetActive(false);
        ControlsMenu.SetActive(false);
    }
    public void Connect()
    {
        serverAddress = GameObject.Find("ServerIP").GetComponent<Text>().text;
        JoinGameMenu.SetActive(false);
        TeamMenu.SetActive(true);
    }
    #endregion
    
    public void StartServer()
    {
        foreach (string value in BoltScenes.AllScenes) 
            map = value;
        BoltLauncher.StartServer(new UdpEndPoint(UdpIPv4Address.Any, (ushort)serverPort));
        BoltNetwork.LoadScene(map);
    }
    void StartClient()
    {
        BoltLauncher.StartClient(UdpEndPoint.Any);
        BoltNetwork.Connect(new UdpEndPoint(UdpIPv4Address.Parse(serverAddress), (ushort)serverPort));
    }
    public void JoinFishTeam()
    {
        hasPickedTeamOne = true;
        hasPickedTeamTwo = false;
        if (isClient == true) 
            StartClient();
        else if (isServer == true) 
            StartServer();
    }
    public void JoinBananaTeam()
    {
        hasPickedTeamOne = false;
        hasPickedTeamTwo = true;
        if (isClient == true) 
            StartClient();
        else if (isServer == true) 
            StartServer();
    }
    public void Exit()
    {
        Application.Quit();
    }
}
