using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int i_health = 100;
    [SerializeField] int i_MaxHealth = 100;
    public float rotationSpeed = 450;
    Rigidbody rb;
    public float defaultWalkSpeed = 5;
    bool canDash = true;
    public float walkSpeed = 5;
    public float dashSpeed = 10;
    public float dashTime = 0.3f;
    public float dashCoolDownTime = 3;
    private Quaternion targetRotation;

    public bool b_AboveAcid = false;
    Gun playerGun;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerGun = GetComponentInChildren<Gun>();

    }

    // Update is called once per frame
    void Update()
    {
///Use these to test the player inputs to see if everything is working.
        //Debug.Log("Left Hor is " +Input.GetAxis("Left_Horizontal"));
        //Log("Left Ver is " +Input.GetAxis("Left_Vertical"));
        //Debug.Log("Right Hor is " +Input.GetAxis("Right_Horizontal"));
        //Debug.Log("Right Ver is " +Input.GetAxis("Right_Vertical"));

        //Get the left and right controller's input based on some select axes.
        Vector3 left_Input =
        new Vector3(Input.GetAxisRaw("Left_Horizontal"), 0, -Input.GetAxisRaw("Left_Vertical"));
        Vector3 right_Input =
        new Vector3(Input.GetAxisRaw("Right_Horizontal"), 0, -Input.GetAxisRaw("Right_Vertical"));
        Vector3 motion = left_Input;

//Only face the direction of right input if the player is pressing something.
        if (right_Input != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(right_Input);

        }

        //If left input is full
        motion *= (Mathf.Abs(left_Input.x) == 1 && Mathf.Abs(left_Input.z) == 1) ? .7f : 1;
        motion *= walkSpeed;

        rb.velocity = motion;

        // }
        motion += Vector3.up * -8;
        CheckForShot();
        CheckDash();
    }

    void CheckForShot()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton5)) //Was 5 PS4
        {
            //            Debug.Log("Brat brat");
            playerGun.ShootProjectile(playerGun.transform);
        }
    }

    void CheckDash()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton4)) //Was 4 PS4
        {
            if (canDash) StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        walkSpeed = dashSpeed;
        b_AboveAcid=true;
        yield return new WaitForSeconds(dashTime);
        walkSpeed = defaultWalkSpeed;
        b_AboveAcid=false;
        yield return new WaitForSeconds(dashCoolDownTime);
        canDash = true;
    }

    public void DamageHealth(int DecreaseBy)
    {
        if (b_AboveAcid == false)
        {
            i_health -= DecreaseBy;
            if (i_health <= 0)
            {
                i_health = 0;
                this.enabled = false;
            }

        }
    }
}
