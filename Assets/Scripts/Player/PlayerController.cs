using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int I_health = 100;
    [SerializeField] int I_maxHealth = 100;
    Rigidbody rb;
    public float F_DefaultWalkSpeed = 5;
    bool b_canDash = true, b_controllerActive = true, b_canShoot = true;
    public float F_WalkSpeed = 5;
    public float F_DashSpeed = 10;
    public float F_DashTime = 0.3f;
    public float F_DashCooldownTime = 3;
    public bool B_AboveAcid = false;
    Gun gun_playerGun;
    [SerializeField] TextMesh Txt_tempHealthIndicator;
    Animator anmtr_anim;
    GameCamera gameCam_PlayerCamera;
    AudioSource AudSrc_ThisSource;
    public GameObject[] GO_Arr_DashParticles;

    [SerializeField] GameObject Go_muzzleShot;
    HealthBar HlthBr_Script;
    EventManager evMan_script;

    // Use this for initialization
    void Start()
    {
        GetVariables();
        SwitchDashParticles(false);
        StartCoroutine(IEnum_HealthRegen());
    }

    // Update is called once per frame
    void Update()
    {
        if (b_controllerActive)
        {


            ///Use these to test the player inputs to see if everything is working.
            //Debug.Log("Left Hor is " +Input.GetAxis("Left_Horizontal"));
            //Log("Left Ver is " +Input.GetAxis("Left_Vertical"));
            //Debug.Log("Right Hor is " +Input.GetAxis("Right_Horizontal"));
            //Debug.Log("Right Ver is " +Input.GetAxis("Right_Vertical"));
            //Debug.Log("Mouse Pos is " + Input.GetAxis("MousePos"));

            //Get the left and right controller's input based on some select axes.
            Vector3 left_Input =
            new Vector3(Input.GetAxisRaw("Left_Horizontal"), 0, Input.GetAxisRaw("Left_Vertical"));
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
    }

    void GetVariables()
    {
        rb = GetComponent<Rigidbody>();
        gun_playerGun = GetComponentInChildren<Gun>();
        anmtr_anim = GetComponent<Animator>();
        gameCam_PlayerCamera = FindObjectOfType<GameCamera>();
        AudSrc_ThisSource = GetComponent<AudioSource>();
        HlthBr_Script = FindObjectOfType<HealthBar>();
        evMan_script = FindObjectOfType<EventManager>();
    }

    void CheckForShot()//On input, shoot.
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton5) || Input.GetMouseButtonDown(0)) //Was 5 PS4
        {
            if (b_canShoot)
            {
                Shoot();
                StartCoroutine(IEnum_ShootRecovery());
            }

            else PlaySound("SFX_Jai_GunCock_01");
        }
    }

    void Shoot()//Shoot from gun script.
    {
        anmtr_anim.SetTrigger("Shot");
        PlaySound("SFX_Jai_Jgun_2x");//Overly complicated process.
        gun_playerGun.ShootProjectile(gun_playerGun.transform);

        foreach (ParticleSystem _prtcl in Go_muzzleShot.GetComponentsInChildren<ParticleSystem>())
        {
            _prtcl.Play();
        }

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
        //Make a new object to calculate Jai's direction.
        GameObject directionCalc = new GameObject();
        directionCalc.transform.SetParent(transform);
        directionCalc.transform.localPosition = new Vector3(0, 0, 0);
        directionCalc.transform.SetParent(null);

        //Jai starts dashing before the anim.
        PlaySound("SFX_Jai_Dash_2x");
        b_canDash = false;
        F_WalkSpeed = F_DashSpeed;
        B_AboveAcid = true;
        SwitchDashParticles(true);


        //Check Jai's movement since creating the object and dash in the right direction.
        yield return new WaitForSeconds(0.05f);
        directionCalc.transform.SetParent(transform);
        anmtr_anim.SetFloat("LeftRightMovement", -directionCalc.transform.localPosition.x);
        anmtr_anim.SetFloat("ForwardBackMovement", -directionCalc.transform.localPosition.z);
        anmtr_anim.SetTrigger("Dash");
        Destroy(directionCalc);


        yield return new WaitForSeconds(F_DashTime);
        SwitchDashParticles(false);
        F_WalkSpeed = F_DefaultWalkSpeed;
        B_AboveAcid = false;
        yield return new WaitForSeconds(F_DashCooldownTime);
        b_canDash = true;
    }

    public void DamageHealth(int DecreaseBy)
    {
        gameCam_PlayerCamera.CamShake();
        ChangeHealth(-DecreaseBy);
    }

    public void AcidDamageHealth(int DecreaseBy)
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
        I_health += _healthChange;
        if (I_health >= I_maxHealth)
        {
            I_health = I_maxHealth;
            if (HlthBr_Script.gameObject.activeInHierarchy == true) HlthBr_Script.SwitchRightCap(true);
        }
        else
        {
            if (HlthBr_Script.gameObject.activeInHierarchy == true) HlthBr_Script.SwitchRightCap(false);
        }

        if (I_health <= 0)
        {
            I_health = 0;
            if (HlthBr_Script.gameObject.activeInHierarchy == true) HlthBr_Script.SwitchLeftCap(false);
        }
        else
        {
            if (HlthBr_Script.gameObject.activeInHierarchy == true) HlthBr_Script.SwitchLeftCap(true);
        }

        if (HlthBr_Script.gameObject.activeInHierarchy == true) HlthBr_Script.ScalePurpleBar((float)I_health / I_maxHealth);
        if (HlthBr_Script.gameObject.activeInHierarchy == true) HlthBr_Script.ScaleYellowBar((float)I_health / I_maxHealth);

        if (I_health <= 0)
        {
            Die();
        }


        Txt_tempHealthIndicator.text = I_health.ToString();
    }


    void PlaySound(string _soundName)
    {

        int _stringCount = _soundName.Length;
        if (_soundName.Substring(_stringCount - 1, 1) == "x")
        {
            int _numberOfSounds = int.Parse(_soundName.Substring(_stringCount - 2, 1));
            int _random = Random.Range(1, _numberOfSounds + 1);

            string _toPlay = _soundName.Substring(0, _stringCount - 2) + "0" + _random;

            AudSrc_ThisSource.clip = Resources.Load<AudioClip>("Sounds/Jai/" + _toPlay);
            AudSrc_ThisSource.Play();
            return;

        }

        AudSrc_ThisSource.clip = Resources.Load<AudioClip>("Sounds/Jai/" + _soundName);
        AudSrc_ThisSource.Play();
    }

    void SwitchDashParticles(bool _onOff)
    {
        foreach (GameObject _go in GO_Arr_DashParticles)
        {
            if (_go.GetComponent<ParticleSystem>() && _onOff) _go.GetComponent<ParticleSystem>().Play();
            if (_go.GetComponent<ParticleSystem>() && !_onOff) _go.GetComponent<ParticleSystem>().Stop();

            if (_go.GetComponent<TrailRenderer>()) _go.SetActive(_onOff);

        }
    }

    IEnumerator IEnum_TempDisableController(float _disableTime)
    {
        b_controllerActive = false;
        yield return new WaitForSeconds(_disableTime);
        b_controllerActive = true;
    }

    public void TempDisableController(float _disableTime)
    {
        StartCoroutine(IEnum_TempDisableController(_disableTime));
    }

    IEnumerator IEnum_ShootRecovery()
    {
        b_canShoot = false;
        yield return new WaitForSeconds(0.5f);
        b_canShoot = true;
    }

    IEnumerator IEnum_HealthRegen()
    {
        yield return new WaitForSeconds(1);
        ChangeHealth(1);
        StartCoroutine(IEnum_HealthRegen());
    }
}
