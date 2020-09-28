using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayers : MonoBehaviour
{
    
    [SerializeField]private new Animator animation;
    private static readonly int Run = Animator.StringToHash("Run");
    
    [SerializeField] private float moveSpeed;
    public CharacterController controller;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVel;
    
    public enum ControllerType
    {
        AI, P1, P2
    }

    public ControllerType controllerType;
    private Dictionary<ControllerType, KeyCode[]> inputs = new Dictionary<ControllerType,KeyCode[]>()
    {
        {ControllerType.P1, new []{KeyCode.Z, KeyCode.S, KeyCode.Q, KeyCode.D}},
        {ControllerType.P2, new []{KeyCode.O, KeyCode.L, KeyCode.K, KeyCode.M}},
    };
    private Dictionary<ControllerType, string[]> axisNames = new Dictionary<ControllerType,string[]>()
    {
        {ControllerType.P1, new []{"Horizontal", "Vertical"}},
        {ControllerType.P2, new []{"Horizontal2", "Vertical2"}},
    };

    // Update is called once per frame
    void Update()
    {
        GetKeyCode();
    }

    public void GetKeyCode()
    {
        short keysCount = 0;
        
        if (Input.GetKey(inputs[controllerType][0]))
        {
            animation.SetBool(Run,true);
            keysCount++;
        }
        if (Input.GetKey(inputs[controllerType][1]))
        {
            animation.SetBool(Run,true);
            keysCount++;
        }
        if (Input.GetKey(inputs[controllerType][2]))
        {
            animation.SetBool(Run,true);
            keysCount++;
        }
        if (Input.GetKey(inputs[controllerType][3]))
        {
            animation.SetBool(Run,true);
            keysCount++;
        }
            
        if (keysCount == 0 || keysCount == 4)
        {
            animation.SetBool(Run, false);
        }

        else
        {
            movePlayer();
        }
    }
    
    private void movePlayer()
    {
        float MoveHorizontal = Input.GetAxisRaw(axisNames[controllerType][0]);
        float MoveVertical = Input.GetAxisRaw(axisNames[controllerType][1]);
        Vector3 direction = new Vector3(MoveHorizontal,0f,MoveVertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg ;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f,angle,0f);

            Vector3 MoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                
            controller.Move(MoveDir.normalized * moveSpeed * Time.deltaTime);
        }
    }
    
}

