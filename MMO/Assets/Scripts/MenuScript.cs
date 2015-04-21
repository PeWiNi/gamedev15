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
    GameObject ResolutionPanel;
    GameObject DisplayModePanel;

    public static bool hasPickedTeamOne = false;
    public static bool hasPickedTeamTwo = false;
    bool isServer = false;
    bool isClient = false;

    string map;
    string serverAddress = "";
    int serverPort = 27000;

    #region Options variables
    public static float AmbientLight = 0.2f;
    string[] resolutionList;

    public static float MasterSoundLevel = 1.0f;
    public static float MusicSoundLevel = 1.0f;
    public static float SFXSoundLevel = 1.0f;

    public static KeyCode[] KeyBindings;
    KeyCode keyHolder = KeyCode.A;
    Button[] KeyBindButtons;
    #endregion

    // Use this for initialization
	void Start ()
    {
        resolutionList = new string[] {"1920x1080", "1280x720", "1024x768" };
        #region Find GameObject(s)
        MainMenu = transform.Find("Main").gameObject;
        PlayMenu = transform.Find("Play").gameObject;
        OptionsMenu = transform.Find("Options").gameObject;

        TeamMenu = transform.Find("TeamSelect").gameObject;
        JoinGameMenu = transform.Find("JoinGame").gameObject;

        AudioMenu = transform.Find("Audio").gameObject;
        VideoMenu = transform.Find("Video").gameObject;
        ControlsMenu = transform.Find("Controls").gameObject;

        ResolutionPanel = GameObject.Find("ResolutionPanel");
        DisplayModePanel = GameObject.Find("DisplayPanel");
        /*
        foreach (string text in resolutionList) //Failed attempt of making a dynamic list of possible resolutions (they are too small and always apply the text form last generated to buttonText)
        {
            Button btn = Instantiate((GameObject)Resources.Load("Prefabs/UIButton")).GetComponent<Button>();
            btn.transform.parent = ResolutionPanel.transform;
            btn.GetComponentInChildren<Text>().text = text;
            btn.onClick.AddListener(() => { resolution(text); });
        }
         */
        Button[] resolutions = ResolutionPanel.GetComponentsInChildren<Button>();
        int i = 0;
        foreach (Button btn in resolutions)
        {
            try
            {
                string text = resolutionList[i];
                btn.GetComponentInChildren<Text>().text = text;
                btn.onClick.AddListener(() => { resolution(text); });
            }
            catch (System.Exception e)
            {
               btn.gameObject.SetActive(false);
            }
            i++;
        }
        KeyBindButtons = ControlsMenu.GetComponentsInChildren<Button>();

        ResolutionPanel.SetActive(false);
        DisplayModePanel.SetActive(false);
        #endregion


        #region Disable all Menus but Main
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

    void resolution(string s)
    {
        GameObject.Find("CurrentResolutionText").GetComponent<Text>().text = s;
    }

    void Awake()
    {
        serverPort = BoltRuntimeSettings.instance.debugStartPort;
    }

    void Update()
    {
        //Brightness Settings Update(s)
        RenderSettings.ambientLight = new Color(AmbientLight, AmbientLight, AmbientLight, 1.0f);
        //TODO: Account for all light sources and/or figure out how to change the "brightness" of the Renderer

        //Sound Settings Update(s)
        AudioListener.volume = MasterSoundLevel;
        //TODO: Add Music controller and set volume
        //GameObject.Find("SoundController").GetComponent<SoundController>().getSoundPlayer().volume = SFXSoundLevel; //Example of how to set the SFX - dunno if works in practice
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

    #region Bolt Server Functions
    public void StartServer()
    {
        makeKeyBindings();
        foreach (string value in BoltScenes.AllScenes) 
            map = value;
        BoltLauncher.StartServer(new UdpEndPoint(UdpIPv4Address.Any, (ushort)serverPort));
        BoltNetwork.LoadScene(map);
    }
    void StartClient()
    {
        makeKeyBindings();
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
    #endregion

    #region Options Functions
    //Video
    public void Brightness()
    {
        AmbientLight = GameObject.Find("BrightnessSlider").GetComponent<Slider>().value;
    }
    //Audio
    public void MasterVolume()
    {
        MasterSoundLevel = GameObject.Find("MasterSlider").GetComponent<Slider>().value;
    }
    public void MusicVolume()
    {
        MasterSoundLevel = GameObject.Find("MusicSlider").GetComponent<Slider>().value;
    }
    public void SFXVolume()
    {
        MasterSoundLevel = GameObject.Find("SFXSlider").GetComponent<Slider>().value;
    }
    //Controls
    public void KeyBind(Button b)
    {
        b.GetComponentInChildren<Text>().text = keyHolder.ToString();
    }

    public void SetResolution() //Borderless not currently supported
    {
        //Input values
        string resolution = GameObject.Find("CurrentResolutionText").GetComponent<Text>().text;
        string display = GameObject.Find("CurrentDisplayText").GetComponent<Text>().text;

        bool fullscreen;
        string[] resol = resolution.Split(char.Parse("x"));
        switch (display)
        {
            case "FullScreen":
                fullscreen = true;
                break;
            default:
                fullscreen = false;
                break;
        }
        Debug.Log("resolution = " + int.Parse(resol[0]) + " by " + int.Parse(resol[1]) + " fullscreen = " + fullscreen);
        Screen.SetResolution(int.Parse(resol[0]), int.Parse(resol[1]), fullscreen);
    }
    #endregion

    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey) {
            keyHolder = e.keyCode;
            Debug.Log("Detected key code: " + e.keyCode);
        }
    }
    KeyCode[] makeKeyBindings()
    {
        KeyCode TailSlap    = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindButtons[1].GetComponentInChildren<Text>().text);
        KeyCode BOOMnana    = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindButtons[2].GetComponentInChildren<Text>().text);
        KeyCode BananaPuke  = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindButtons[3].GetComponentInChildren<Text>().text);
        KeyCode FishSlam    = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindButtons[4].GetComponentInChildren<Text>().text);
        KeyCode Heal        = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindButtons[5].GetComponentInChildren<Text>().text);

        return KeyBindings = new KeyCode[] { TailSlap, BOOMnana, BananaPuke, FishSlam, Heal };
    }
    public void Exit()
    {
        Application.Quit();
    }
}
