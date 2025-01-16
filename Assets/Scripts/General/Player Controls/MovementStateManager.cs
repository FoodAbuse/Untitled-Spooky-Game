using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{

    public float moveSpeed = 3;
    [HideInInspector] public Vector3 dir;
    float hzInput, vInput;
    CharacterController controller;

    [SerializeField] float groundOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;

    [SerializeField] float gravity = -9.81f;
    Vector3 velocity;

    //audio
    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
        Gravity();

        //audio test - it works - can call this function in the relevant scripts
        //atm 

        if (Input.GetKeyDown(KeyCode.O))
        {
            AudioManager.Instance.PlaySoundEffect("PhoneMenuExit", 0.6f, 1f);
        }

        //random pitch test
        if (Input.GetKeyDown(KeyCode.P))
        {
            float randomPitchTest = Random.Range(0.8f, 1.2f);
            AudioManager.Instance.PlaySoundEffect("PhoneMenuExit", 0.6f, randomPitchTest);
        }
        
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;

        controller.Move(dir.normalized * moveSpeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    void Gravity()
    {
        if(!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if(velocity.y<0) velocity.y = -2;

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmos() 

    {
        if(spherePos!=null && controller!=null) //avoids errors if references are missing
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
        }
        
    }
}
