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

    enum State {
        Main,
        Play,
        Options,
        Team,
        JoinGame,
        Audio,
        Video,
        Controls,
    }
    State state;

    GameObject ResolutionPanel;
    GameObject DisplayModePanel;

    GameObject BackButton;

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
    bool keyChange = false;
    Button keyBind;
    #endregion

    // Use this for initialization
	void Start ()
    {
        resolutionList = new string[] { "1920x1080", "1366x768", "1280x768", "1280x720", "1024x768", "800x600" };
        #region Find GameObject(s)
        MainMenu = transform.Find("Main").gameObject;
        PlayMenu = transform.Find("Play").gameObject;
        OptionsMenu = transform.Find("Options").gameObject;

        TeamMenu = transform.Find("TeamSelect").gameObject;
        JoinGameMenu = transform.Find("JoinGame").gameObject;

        AudioMenu = transform.Find("Audio").gameObject;
        VideoMenu = transform.Find("Video").gameObject;
        ControlsMenu = transform.Find("Controls").gameObject;

        ControlsMenu.SetActive(true);
        BackButton = GameObject.Find("ControlsBackButton");

        VideoMenu.SetActive(true);
        ResolutionPanel = GameObject.Find("ResolutionScrollPanel");
        DisplayModePanel = GameObject.Find("DisplayPanel");
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

        ResolutionPanel.SetActive(false);
        DisplayModePanel.SetActive(false);
        #endregion


        #region Disable all Menus but Main
        state = State.Main;
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

        #region Controls
        makeKeyBindings();

        if (keyChange && keyHolder != KeyCode.None)
        {
            keyBind.GetComponentInChildren<Text>().text = keyHolder.ToString();
            keyChange = false;
        }

        // Failed attempt of checking for multi-bound keys 
        if (state == State.Controls)
        {
            bool fakau = false;
            for (int i = 0; i < KeyBindings.Length; i++)
                for (int j = 0; j < KeyBindings.Length; j++)
                {
                    if (!fakau && i != j && (KeyBindings[i] == KeyBindings[j] || 
                        KeyBindings[i] == KeyCode.W || KeyBindings[i] == KeyCode.A || KeyBindings[i] == KeyCode.S || KeyBindings[i] == KeyCode.D))
                    {
                        BackButton.SetActive(false);
                        Debug.Log("EQUALS FOUND");
                        fakau = true;
                    }
                    else if(!fakau)
                    {
                        BackButton.SetActive(true);
                        Debug.Log("Keybinds are " + KeyBindings[i].ToString() + " and " + KeyBindings[j].ToString() + " (i, j) = (" + i + ", " + j + ")");
                        Debug.Log("EQUALS NOT FOUND");
                    }
                }
        }
        #endregion
    }

    #region GUI Functions
    public void Play()
    {
        state = State.Play;
        MainMenu.SetActive(false);
        PlayMenu.SetActive(true);
    }
    public void Options()
    {
        state = State.Options;
        MainMenu.SetActive(false);
        OptionsMenu.SetActive(true);
    }
    public void BackToMain()
    {
        state = State.Main;
        MainMenu.SetActive(true);
        PlayMenu.SetActive(false);
        OptionsMenu.SetActive(false);
    }
    public void NewGame()
    {
        state = State.Team;
        isServer = true;
        isClient = false;
        PlayMenu.SetActive(false);
        TeamMenu.SetActive(true);
    }
    public void JoinGame()
    {
        state = State.JoinGame;
        isServer = false;
        isClient = true;
        PlayMenu.SetActive(false);
        JoinGameMenu.SetActive(true);
    }
    public void BackToPlay()
    {
        state = State.Play;
        PlayMenu.SetActive(true);
        TeamMenu.SetActive(false);
        JoinGameMenu.SetActive(false);
    }
    public void Audio()
    {
        state = State.Audio;
        OptionsMenu.SetActive(false);
        AudioMenu.SetActive(true);
    }
    public void Video()
    {
        state = State.Video;
        OptionsMenu.SetActive(false);
        VideoMenu.SetActive(true);
    }
    public void Controls()
    {
        state = State.Controls;
        OptionsMenu.SetActive(false);
        ControlsMenu.SetActive(true);
    }
    public void BackToOptions()
    {
        state = State.Options;
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
        GameObject.Find("MasterTextValue").GetComponent<Text>().text = "" + System.Math.Ceiling(MasterSoundLevel * 100f);
    }
    public void MusicVolume()
    {
        MasterSoundLevel = GameObject.Find("MusicSlider").GetComponent<Slider>().value;
        GameObject.Find("MusicTextValue").GetComponent<Text>().text = "" + System.Math.Ceiling(MusicSoundLevel * 100f);
    }
    public void SFXVolume()
    {
        MasterSoundLevel = GameObject.Find("SFXSlider").GetComponent<Slider>().value;
        GameObject.Find("SFXTextValue").GetComponent<Text>().text = "" + System.Math.Ceiling(SFXSoundLevel * 100f);
    }
    //Controls
    public void KeyBind(Button b)
    {
        keyChange = true;
        keyBind = b;
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
        if (keyChange) {
            Event e = Event.current;
            if (e.isKey) {
                keyHolder = e.keyCode;
            }
        }
        else if (keyHolder != KeyCode.None) {
            keyHolder = KeyCode.None;
        }
    }
    KeyCode[] makeKeyBindings()
    {
        if (state != State.Controls)
            ControlsMenu.SetActive(true);
        if (state == State.Controls) {
            BackButton.SetActive(true);

        }
        Button[] KeyBindButtons = ControlsMenu.GetComponentsInChildren<Button>();
        KeyCode TailSlap    = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindButtons[1].GetComponentInChildren<Text>().text);
        KeyCode BOOMnana    = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindButtons[2].GetComponentInChildren<Text>().text);
        KeyCode BananaPuke  = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindButtons[3].GetComponentInChildren<Text>().text);
        KeyCode FishSlam    = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindButtons[4].GetComponentInChildren<Text>().text);
        KeyCode Heal        = (KeyCode)System.Enum.Parse(typeof(KeyCode), KeyBindButtons[5].GetComponentInChildren<Text>().text);
        if (state != State.Controls)
            ControlsMenu.SetActive(false);
        if (state == State.Controls)
            BackButton.SetActive(false);


        return KeyBindings = new KeyCode[] { TailSlap, BOOMnana, BananaPuke, FishSlam, Heal };
    }
    public void Exit()
    {
        Application.Quit();
    }
}
