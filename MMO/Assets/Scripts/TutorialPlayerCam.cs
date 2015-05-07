using UnityEngine;
using System.Collections;

public class TutorialPlayerCam : MonoBehaviour {

    float zoom = 50.0f;
    bool started = false;
    float movement = 2;
    Vector3 camPos = new Vector3();
    Vector3 offset;
    public Transform _target;// camera target
    
    [SerializeField]
    Transform
        cam;

    bool stupidTutorialPlayerBehaviourScriptCheck = false;
    
    void Start()
    {
        offset = _target.transform.position - transform.position;
        
        Vector3 pos = new Vector3();
        pos.x = _target.transform.position.x;
        pos.y = _target.transform.position.y;
        pos.z = _target.transform.position.z;
        
        transform.position = pos;
    }
    
    public new Camera camera {
        get { return cam.GetComponent<Camera>(); }
    }
    
    void Awake ()
    {
        DontDestroyOnLoad (gameObject);
    }
    
    public void setStartLocation(Vector3 pos)
    {
        camPos.x = pos.x -500;
        camPos.y = pos.y - 50;
        camPos.z = pos.z;
    }
    
    void Update ()//Camera (bool allowSmoothing)
    {
        if (!stupidTutorialPlayerBehaviourScriptCheck)
        {
            GameObject player = GameObject.FindGameObjectWithTag("TutorialPlayer");
            player.GetComponent<TutorialPlayerBehaviour>().enabled = true;
            stupidTutorialPlayerBehaviourScriptCheck = true;
        }
        Vector3 pos;
        Quaternion rot;
        if (_target != null) {       
            
            if (!started)
            { 
                camPos.z = _target.position.z-80;
                camPos.x = _target.position.x;
                camPos.y = _target.position.y+45; 
                transform.position = camPos;
                offset = _target.transform.position - transform.position;
                started = true; 
                
            }
            var x = movement * (-1) * Input.GetAxis("Mouse X");
            var y = movement * (-1) * Input.GetAxis("Mouse Y");
        }
    }
    void LateUpdate()
    {
        float horizontal = Input.GetAxis("Mouse X") * movement;
        _target.transform.Rotate(0, horizontal, 0);
        float vertical = Input.GetAxis("Mouse Y") * movement;
        float desiredAngle = _target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = (_target.transform.position) - ( rotation*offset); // boomnana -> range = new offset from position * angle.
        Vector3 lookPos = _target.position;
        lookPos.y += 30;
        transform.LookAt(lookPos);
    }
    
    public void SetTarget (BoltEntity entity)
    {
        _target = entity.transform;
    }
}
