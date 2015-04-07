using UnityEngine;
using System.Collections;

public class SimpleMovement : MonoBehaviour
{

    public float speed = 17f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move(transform);
        if (Input.GetKey(KeyCode.P))
            transform.position = new Vector3(0, 0, 0);
    }

    /// <summary>
    /// From http://arakawa.asia/2014/04/04/ - because didn't want to bother with making it :P
    /// </summary>
    /// <param name="transform"></param>
    public void Move(Transform transform)
    {
        var velocity = new Vector3();
        var movement = new Vector3();

        var inAirMultiplier = 0.25f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            var z = Input.GetKey(KeyCode.W) ? 1.0f : 0;
            z = Input.GetKey(KeyCode.S) ? -1.0f : z;

            var x = Input.GetKey(KeyCode.D) ? 1.0f : 0;
            x = Input.GetKey(KeyCode.A) ? -1.0f : x;

            movement.z = speed * z;
            movement.x = speed * x;

            //face movement dir
            Vector3 keyboardPosition = new Vector3(x, 0, z);
            transform.LookAt(transform.position + keyboardPosition);
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        movement.x *= inAirMultiplier;
        movement.z *= inAirMultiplier;

        movement += velocity;
        movement += Physics.gravity;
        movement *= Time.deltaTime;
        transform.GetComponent<CharacterController>().Move(movement);
    }
}
