﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int I_health = 100;
    [SerializeField] int I_maxHealth = 100;
    Rigidbody rb;
    public float F_DefaultWalkSpeed = 5;
    bool b_canDash = true;
    public float F_WalkSpeed = 5;
    public float F_DashSpeed = 10;
    public float F_DashTime = 0.3f;
    public float F_DashCooldownTime = 3;
    public bool B_AboveAcid = false;
    Gun gun_playerGun;
    [SerializeField] TextMesh Txt_tempHealthIndicator;
    Animator anmtr_anim;


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

        //This makes sure that if both inputs are down at the same time then the player doesn't move around at a higher speed.
        motion *= (Mathf.Abs(left_Input.x) == 1 && Mathf.Abs(left_Input.z) == 1) ? .7f : 1;
        motion *= F_WalkSpeed;

        rb.velocity = motion;
        anmtr_anim.SetFloat("Speed", rb.velocity.magnitude);
        motion += Vector3.up * -8;
        CheckForShot();
        CheckDash(motion.x, motion.z);
        CheckMousePoint();
    }

    void GetVariables()
    {
        rb = GetComponent<Rigidbody>();
        gun_playerGun = GetComponentInChildren<Gun>();
        anmtr_anim = GetComponent<Animator>();
    }

    void CheckForShot()//On input, shoot.
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetMouseButtonDown(0)) //Was 5 PS4
        {
            Shoot();
        }
    }

    void Shoot()//Shoot from gun script.
    {
        anmtr_anim.SetTrigger("Shot");
        gun_playerGun.ShootProjectile(gun_playerGun.transform);
    }

    void CheckDash(float _motionX, float _motionY)//Check Dash input.
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton4) || Input.GetKeyDown(KeyCode.Space)) //Was 4 PS4
        {
            if (b_canDash)
            {
                StartCoroutine(Dash());
            }
        }
    }

    IEnumerator Dash()//Move faster, then move slower. Also prevent acid damage during this time. MUST LATER SEPARATE THIS VAR
    {

        GameObject directionCalc = new GameObject();
        directionCalc.transform.SetParent(transform);
        directionCalc.transform.localPosition = new Vector3(0, 0, 0);
        directionCalc.transform.SetParent(null);


        b_canDash = false;
        F_WalkSpeed = F_DashSpeed;
        B_AboveAcid = true;


        yield return new WaitForSeconds(0.1f);
        directionCalc.transform.SetParent(transform);
        anmtr_anim.SetFloat("LeftRightMovement", -directionCalc.transform.localPosition.x);
        anmtr_anim.SetFloat("ForwardBackMovement", -directionCalc.transform.localPosition.z);
        anmtr_anim.SetTrigger("Dash");

        
        yield return new WaitForSeconds(F_DashTime);
        F_WalkSpeed = F_DefaultWalkSpeed;
        B_AboveAcid = false;
        yield return new WaitForSeconds(F_DashCooldownTime);
        b_canDash = true;
    }

    public void DamageHealth(int DecreaseBy)
    {
        if (B_AboveAcid == false)
        {
            ChangeHealth(-DecreaseBy);
        }
    }

    void Die()//Respawn by reloading the scene. 
    {
        FindObjectOfType<Scene_Manager>().ReloadScene();
    }

    public void CheckMousePoint()//See where the mouse is pointing only if there is mouse motion.
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
        Mathf.Clamp(I_health += _healthChange, 0, I_maxHealth);

        if (I_health <= 0)
        {
            Die();
        }


        Txt_tempHealthIndicator.text = I_health.ToString();
    }

    public void AddHealthFromPotion(int _amountToAdd, HealthPotion _HP)
    {
        if (I_health < I_maxHealth)
        {
            ChangeHealth(_amountToAdd);
            Destroy(_HP.gameObject);
        }
    }
}
