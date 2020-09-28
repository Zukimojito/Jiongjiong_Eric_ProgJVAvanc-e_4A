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

    // Update is called once per frame
    void Update()
    {
        GetKeyCode();
    }

    public void GetKeyCode()
    {
        short keysCount = 0;
        
        if (Input.GetKey(KeyCode.Z))
        {
            animation.SetBool(Run,true);
            keysCount++;
        }
        if (Input.GetKey(KeyCode.S))
        {
            animation.SetBool(Run,true);
            keysCount++;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            animation.SetBool(Run,true);
            keysCount++;
        }
        if (Input.GetKey(KeyCode.D))
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
        float MoveHorizontal = Input.GetAxisRaw("Horizontal");
        float MoveVertical = Input.GetAxisRaw("Vertical");
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

