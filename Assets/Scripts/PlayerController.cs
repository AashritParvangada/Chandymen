using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int i_health = 100;
    [SerializeField] int i_MaxHealth = 100;
    Rigidbody rb;
    public float defaultWalkSpeed = 5;
    bool canDash = true;
    public float walkSpeed = 5;
    public float dashSpeed = 10;
    public float dashTime = 0.3f;
    public float dashCoolDownTime = 3;
    public bool b_AboveAcid = false;
    Gun playerGun;
    [SerializeField] TextMesh Txt_TempHealthIndicator;
    Animator anim;


    // Use this for initialization
    void Start()
    {
        GetVariables();
    }

    // Update is called once per frame
    void Update()
    {
        ///Use these to test the player inputs to see if everything is working.
        //Debug.Log("Left Hor is " +Input.GetAxis("Left_Horizontal"));
        //Log("Left Ver is " +Input.GetAxis("Left_Vertical"));
        //Debug.Log("Right Hor is " +Input.GetAxis("Right_Horizontal"));
        //Debug.Log("Right Ver is " +Input.GetAxis("Right_Vertical"));
        //Debug.Log("Mouse Pos is " + Input.GetAxis("MousePos"));

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

        //This makes sure that if both inputs are down at the same time then the player doesn't move around.
        motion *= (Mathf.Abs(left_Input.x) == 1 && Mathf.Abs(left_Input.z) == 1) ? .7f : 1;
        motion *= walkSpeed;

        rb.velocity = motion;
        anim.SetFloat("Speed", rb.velocity.magnitude);
        motion += Vector3.up * -8;
        CheckForShot();
        CheckDash();
        CheckMousePoint();
    }

    void GetVariables()
    {
        rb = GetComponent<Rigidbody>();
        playerGun = GetComponentInChildren<Gun>();
        anim = GetComponent<Animator>();
    }

    void CheckForShot()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetMouseButtonDown(0)) //Was 5 PS4
        {
            Shoot();
        }
    }

    void Shoot()
    {
        anim.SetTrigger("Shot");
        playerGun.ShootProjectile(playerGun.transform);
    }

    void CheckDash()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.Space)) //Was 4 PS4
        {
            anim.SetTrigger("Dash");
            if (canDash) StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        walkSpeed = dashSpeed;
        b_AboveAcid = true;
        yield return new WaitForSeconds(dashTime);
        walkSpeed = defaultWalkSpeed;
        b_AboveAcid = false;
        yield return new WaitForSeconds(dashCoolDownTime);
        canDash = true;
    }

    public void DamageHealth(int DecreaseBy)
    {
        if (b_AboveAcid == false)
        {
            ChangeHealth(-DecreaseBy);
        }
    }

    void Die()
    {
        FindObjectOfType<Scene_Manager>().ReloadScene();
    }

    public void CheckMousePoint()
    {
        if (Input.GetAxis("MousePos_X") > 0.1 || Input.GetAxis("MousePos_Y") > 0.1 || Input.GetAxis("MousePos_X") < -0.1 || Input.GetAxis("MousePos_Y") < -0.1)
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Vector3 V3_LookPos = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                transform.LookAt(V3_LookPos);
            }
        }
    }

    void ChangeHealth(int _healthChange)
    {
        Mathf.Clamp(i_health += _healthChange, 0, i_MaxHealth);

        if (i_health <= 0)
        {
            Die();
        }


        Txt_TempHealthIndicator.text = i_health.ToString();
    }

    public void AddHealthFromPotion(int _amountToAdd, HealthPotion _HP)
    {
        if (i_health < i_MaxHealth)
        {
            ChangeHealth(_amountToAdd);
            Destroy(_HP.gameObject);
        }
    }
}
