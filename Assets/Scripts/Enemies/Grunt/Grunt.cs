using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : MonoBehaviour
{
    [SerializeField] int I_totalHealth = 100;
    int i_currentHealth;

    PlayerController playcont_player; Gun gun_playaGun;
    [SerializeField] float F_rayDistance = 50;
    List<Zone> zon_List_zones = new List<Zone>();
    [SerializeField] float F_minMovementCheckTime, F_maxMovementCheckTime;
    [SerializeField] float F_minShootTime, F_maxShootTime;

    NavMeshAgent navMesAg_agent;

    [SerializeField] Transform Trans_gruntGun;
    [SerializeField] GameObject GO_bullet;
    [SerializeField] GameObject GO_healthBarAnchor;
    [SerializeField] float F_bulletSpeed;
    public bool B_ShootOnStart = true, B_ShootOnDialogue = false;
    EventManager evMan_eventManager;
    Animator anim_Controller;
    Rigidbody rb_RB;
    float f_currentSpeed;
    [SerializeField] ParticleSpawner ParticleSpawner_death, ParticleSpawner_hit;
    [SerializeField] SkinnedMeshRenderer SknMshRndr_gruntSkin;
    [SerializeField] Material Mat_grunt;
    bool b_spawning = true;
    AudioSource audSrc_thisSource;
    //How this agent works:
    //Ray cast to player.
    //If the player isn't found, set destination to player while raycasting for player every half second.
    //If the player is found, set a destination depending on which zone the player is in.
    private void OnEnable()
    {
        EventManager.OnDialogueComplete += DialogueEventEnded;
    }
    private void OnDisable()
    {
        EventManager.OnDialogueComplete -= DialogueEventEnded;
    }
    private void Start()//Get variables.
    {
        GetVariables();
        StartCoroutine(IEnum_LerpClippingThresehold(F_minShootTime));
    }

    public void CheckAttackOnStart()
    {
        if (B_ShootOnStart)
        {
            StartCombat();
        }
    }

    public void StartCombat()
    {
        StartCoroutine(CheckToMove());//Start the movement. Will delay this later during the cutscene.
        StartCoroutine(CheckToShoot());//Start shooting. Will delay this later.
    }

    void DialogueEventEnded()
    {
        if (B_ShootOnDialogue)
        {
            StartCombat();
        }
    }

    void GetVariables()//Get event manager, nav msh agent, player cont, gun, and make zone list.
    {
        i_currentHealth = I_totalHealth;
        evMan_eventManager = FindObjectOfType<EventManager>();
        navMesAg_agent = GetComponent<NavMeshAgent>();
        playcont_player = FindObjectOfType<PlayerController>();
        gun_playaGun = FindObjectOfType<Gun>();
        anim_Controller = GetComponentInChildren<Animator>();
        rb_RB = GetComponent<Rigidbody>();
        audSrc_thisSource = GetComponent<AudioSource>();
        GetZones();
    }

    private void Update()
    {
        transform.LookAt(playcont_player.transform);//Just stare at the player.
        anim_Controller.SetFloat("Speed", navMesAg_agent.velocity.magnitude);
        //CheckToShoot(); //Use later for shotgun.
    }

    void CheckIfCanShootPlayer()//When this is called, sees if the player is in the line of sight. If so, shoot.
    {
        RaycastHit hit;

        Vector3 rayCastOrigin = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Vector3 rayCastDirectionPoint = new Vector3(playcont_player.transform.position.x, playcont_player.transform.position.y + 1, playcont_player.transform.position.z);

        if (Physics.Raycast(rayCastOrigin, (rayCastDirectionPoint - rayCastOrigin), out hit, F_rayDistance))
        {
            if (hit.transform == playcont_player.transform || hit.transform == gun_playaGun.transform)
            {
                Shoot();
            }

        }
    }

    void GetZones()//Gets all the zones in the map.
    {
        foreach (Zone _zone in FindObjectsOfType<Zone>())
        {
            zon_List_zones.Add(_zone);
        }
    }

    void Shoot()//Shoots a spray of bullets.
    {
        anim_Controller.SetTrigger("Shot");
        PlaySound("SFX_Grunt_Shotgun_01");

        GameObject _bullet = Instantiate(GO_bullet, null);
        _bullet.transform.position = Trans_gruntGun.position;
        _bullet.transform.forward = transform.forward;
        _bullet.GetComponent<Rigidbody>().velocity = transform.forward * F_bulletSpeed;

        GameObject _bullet2 = Instantiate(GO_bullet, null);
        _bullet2.transform.position = Trans_gruntGun.position;
        _bullet2.transform.forward = transform.forward;
        _bullet2.transform.eulerAngles = new Vector3(_bullet2.transform.eulerAngles.x, _bullet2.transform.eulerAngles.y + 10, 0);
        _bullet2.GetComponent<Rigidbody>().velocity = _bullet2.transform.forward * F_bulletSpeed;


        GameObject _bullet3 = Instantiate(GO_bullet, null);
        _bullet3.transform.position = Trans_gruntGun.position;
        _bullet3.transform.forward = transform.forward;
        _bullet3.transform.eulerAngles = new Vector3(_bullet3.transform.eulerAngles.x, _bullet3.transform.eulerAngles.y - 10, 0);
        _bullet3.GetComponent<Rigidbody>().velocity = _bullet3.transform.forward * F_bulletSpeed;

    }

    void PlaySound(string _soundName)
    {
        audSrc_thisSource.clip = Resources.Load<AudioClip>("Sounds/Grunt/" + _soundName);
        audSrc_thisSource.Play();
    }

    IEnumerator CheckToShoot()//Repeats on itself to keep shooting at the player.
    {
        yield return new WaitForSeconds(CheckShootTime());
        CheckIfCanShootPlayer();
        StartCoroutine(CheckToShoot());
    }

    IEnumerator CheckToMove() //Waits, then moves to a point within the zone.
    {
        yield return new WaitForSeconds(CheckMovementTime());
        MoveToPointInPlayerZone();

        StartCoroutine(CheckToMove());
    }

    void MoveToPointInPlayerZone()//Moves to a random point among the zone's points.
    {
        int pointToMoveTo = Random.Range(0, CheckPlayerZone().Trans_List_NavDestinations.Count);

        SetNavDestination(CheckPlayerZone().Trans_List_NavDestinations[pointToMoveTo].transform.position);

    }

    //Checks which zone the player is closest to.
    Zone CheckPlayerZone()//Checks which zone the player is in.
    {
        float shortestDistance = 1000;
        Zone closestZone = null;

        foreach (Zone _zone in zon_List_zones)
        {
            if (Vector3.Distance(_zone.transform.position, playcont_player.transform.position) < shortestDistance)
            {
                shortestDistance = Vector3.Distance(_zone.transform.position, playcont_player.transform.position);
                closestZone = _zone;
            }
        }
        return closestZone;
    }

    float CheckMovementTime()//Randomizes movement time.
    {
        float _movementTime = Random.Range(F_minMovementCheckTime, F_maxMovementCheckTime);
        return _movementTime;
    }

    float CheckShootTime()//Randomizes shoot time
    {
        float _shoottime = Random.Range(F_minShootTime, F_maxShootTime);
        return _shoottime;
    }

    void SetNavDestination(Vector3 _position)//Sets a destination.
    {
        navMesAg_agent.SetDestination(_position);
    }

    public void DamageHealth(int _Damage)//Called in Plasma Bullet.
    {
        if (!b_spawning)
        {
            i_currentHealth -= _Damage;
            InstantiateParticles(ParticleSpawner_hit);
            GO_healthBarAnchor.transform.localScale = new Vector3((float)i_currentHealth / I_totalHealth, 1, 1);

            if (i_currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void InstantiateParticles(ParticleSpawner _prtclSpawn)
    {
        ParticleSpawner prtclSpawn = Instantiate(_prtclSpawn, transform);
        prtclSpawn.GetVariables();
        prtclSpawn.Activate();
    }

    void Die()
    {
        InstantiateParticles(ParticleSpawner_death);
        Destroy(gameObject);
    }

    private void OnDestroy()//When this enemy is killed, trigger event count enemies killed.   
    {
        evMan_eventManager.CountEnemyKilled();
    }

    IEnumerator IEnum_LerpClippingThresehold(float _time)
    {
        b_spawning = true;
        float _newClipSrength = 0;
        float elapsedTime = 0;
        SknMshRndr_gruntSkin.material.SetFloat("Vector1_B15186D7", 1);
        float startingClippingSrength = SknMshRndr_gruntSkin.material.GetFloat("Vector1_B15186D7");

        while (elapsedTime < _time)
        {
            float _currentClipStrength = Mathf.Lerp(startingClippingSrength, _newClipSrength, elapsedTime / _time);
            SknMshRndr_gruntSkin.material.SetFloat("Vector1_B15186D7", _currentClipStrength);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        SknMshRndr_gruntSkin.material = Mat_grunt;
        b_spawning = false;
    }
}
