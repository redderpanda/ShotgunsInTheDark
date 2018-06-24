using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class S_P_Cont : MonoBehaviour {
    public Transform spawnPoint;
    public Camera cam;
    public GameObject spawnManager;
    private int maxHealth = 100;
    public int currentHealth;
    public string ctr_num;
    private Rigidbody2D rigidB;
    public float move_speed = 4f;
    private float reg_speed = 7f;
    public Transform[] spawnPositions;
    public AttackSpot[] spawnPoints;
    public List<AttackSpot> notOccupiedSpots = new List<AttackSpot>();
    public Dictionary<AttackSpot, Transform> places = new Dictionary<AttackSpot, Transform>();
    
    public GameObject bulletPrefab;
    public GameObject flashLight;

    public Transform firePoint;
    public bool isBasicAttacking;
    public GameObject Trail;
    public AudioSource GunShot;
    public GameObject bloodsplatter;
    public AudioSource MonsterGrowl;
    public GameObject MonsterSauce;
    public AudioSource MonsterHit;
    public float win_value;

    //MonsterVariables
    public Animator anim;
    private int monster_max_health = 300;
    public GameObject sprite_object;
    private float monster_speed = 12f;
    public Sprite MonsterSprite; //need to make sprite
    public bool isMonster;


    //PowerUp Variables
    public bool hasShotGun;
    public int ShotGunAmmo;

    //GunVariables
    public float FireDelay = 0.25f;

    //movement
    float movement_H;
    float movement_V;

    //Healthbar
    public Slider healthbar;

    //score
    public float _currentScore;
    public Text _scoreText;

    // Use this for initialization
    void Start () {
        //anim = GetComponent<Animator>();
        move_speed = reg_speed;
        rigidB = transform.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthbar.maxValue = maxHealth;
        healthbar.value = currentHealth;
        isMonster = false;
        //flashLight = Transform.GetChild("Flash_Light");
        _currentScore = 0;
        places.Add(spawnPoints[0], spawnPositions[0]);
        places.Add(spawnPoints[1], spawnPositions[1]);
        places.Add(spawnPoints[2], spawnPositions[2]);
        places.Add(spawnPoints[3], spawnPositions[3]);
    }
	
	// Update is called once per frame
	void Update () {
        OnDeath();
        checkAmmo();
        FlashLightToggle();
        healthbar.value = currentHealth;
	}

    void FixedUpdate()
    {
        // if testing with controller
        float V_move = Input.GetAxis(ctr_num + " LEFT JOYSTICK VERTICAL");
        float H_move = Input.GetAxis(ctr_num + " LEFT JOYSTICK HORIZONTAL");

        // if testing with keyboard
        //float V_move = Input.GetAxis("Vertical");
        //float H_move = Input.GetAxis("Horizontal");
        if (!isMonster)//this is new
        {
            StartCoroutine(BasicAttack());
        }//ends here
        else
        {
            StartCoroutine(MonsterAttack());
        }

         movement_H = H_move * move_speed;
         movement_V = V_move * move_speed;

         // if testing with controller
         rigidB.velocity = new Vector2(movement_H, -movement_V);
         // if testing with keyboard
         //rigidB.velocity = new Vector2(movement_H, movement_V);
       


    }

    private IEnumerator MonsterAttack()
    {
        if (Input.GetAxis(ctr_num + " RIGHT TRIGGER") < 0)
        {
            if (!isBasicAttacking)
            {
                anim.SetBool("isAttacking", true);
                isBasicAttacking = true;
                yield return new WaitForSeconds(0.25f);
                float rot_x = Mathf.Cos(Mathf.Deg2Rad * firePoint.eulerAngles.z);
                float rot_y = Mathf.Sin(Mathf.Deg2Rad * firePoint.eulerAngles.z);
                RaycastHit2D hit = BoxCast(firePoint.position, new Vector2(1f, 2f), firePoint.eulerAngles.z, new Vector2(rot_x, rot_y), 0.5f, 1);
                //Debug.Log(hit.collider.gameObject.tag);
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.tag == "Player")
                    {
                        S_P_Cont hit_player = hit.collider.gameObject.GetComponent<S_P_Cont>();
                        Instantiate(bloodsplatter, hit_player.transform.position, hit_player.transform.rotation);
                        if((hit_player.currentHealth - 10) <= 0)
                        {
                            _currentScore++;
                            _scoreText.text = "Monster Kills: " + _currentScore;
                        }
                        hit_player.currentHealth -= 10;
                        MonsterHit.Play();
                    }
                }
                yield return new WaitForSeconds(0.25f);
                RaycastHit2D hit_2 = BoxCast(firePoint.position, new Vector2(1f, 2f), firePoint.eulerAngles.z, new Vector2(rot_x, rot_y), 0.5f, 1);
                if (hit_2.collider != null)
                {
                    if (hit_2.collider.gameObject.tag == "Player")
                    {
                        S_P_Cont hit_player = hit_2
                            .collider.gameObject.GetComponent<S_P_Cont>();
                        Instantiate(bloodsplatter, hit_player.transform.position, hit_player.transform.rotation);
                        if ((hit_player.currentHealth - 10) <= 0)
                        {
                            _currentScore++;
                            _scoreText.text = "Monster Kills: " + _currentScore;
                        }
                        hit_player.currentHealth -= 10;
                        if (_currentScore >= win_value)
                        {
                            Debug.Log("Won");
                            SceneManager.LoadScene("LAST");
                        }
                    }
                }
                anim.SetBool("isAttacking", false);
                isBasicAttacking = false;
            }
        }
    }

   

        private IEnumerator BasicAttack()
    {
        //Debug.Log(Input.GetAxis(ctr_num + " RIGHT TRIGGER"));
        if(Input.GetAxis(ctr_num +  " RIGHT TRIGGER") < 0)
        {
            if (!isBasicAttacking)
            {
                isBasicAttacking = true;
                Shoot();
                yield return new WaitForSeconds(FireDelay);
                isBasicAttacking = false;
            }
        }
    }
    private void OnDeath()
    {
        if(currentHealth <= 0)
        {
            cam.cullingMask &= ~(1 << 9);
            if (isMonster)
            {
                flashLight.SetActive(true);
                Trail.SetActive(true);
                Instantiate(MonsterSauce,transform.position, transform.rotation);
            }
            anim.SetBool("IsMonster", false);
            isMonster = false;
            //flashLight.GetComponent<Light>().color = new Color32(55, 193, 187, 255);
            gameObject.tag = "Player";
            move_speed = reg_speed;
            currentHealth = maxHealth;
            healthbar.maxValue = maxHealth;
            healthbar.value = currentHealth;
            transform.localScale = new Vector3(1f, 1f, 1f);
            //gameObject.SetActive(false);
            //for(int i = 0; i < spawnPoints.Length; i++)
            //{
            //    if(spawnPoints[i].bounds.Contains())
            //}
            for(int i = 0; i < spawnPoints.Length; i++)
            {
                if(!spawnPoints[i].GetComponent<AttackSpot>().containsMonster)
                {
                    notOccupiedSpots.Add(spawnPoints[i]); 
                }
            }
            int go_here = Random.Range(0, notOccupiedSpots.Count);
            Debug.Log(go_here);
            Debug.Log(notOccupiedSpots);
            AttackSpot temp = notOccupiedSpots[go_here];
            transform.position = places[temp].position;
            notOccupiedSpots.Clear();

        }
    }

    public void FlashLightToggle()
    {
        if(Input.GetButtonDown(ctr_num + " B BUTTON"))
        {
            if (!isMonster)
            {
                if (flashLight.activeSelf)
                {
                    flashLight.SetActive(false);
                }
                else
                {
                    flashLight.SetActive(true);
                }
            }
        }
    }

    void Shoot()
    {
        //Debug.Log("SHOOTING");
        if (hasShotGun)
        {
            for (int x = 0; x < 3; x++)
            {
                float spread = Random.Range(-10, 10);
                Vector3 eulerRotation = firePoint.eulerAngles;
                eulerRotation.z = eulerRotation.z + spread;
                Quaternion RotationAngle = new Quaternion();
                RotationAngle.eulerAngles = eulerRotation;
                GunShot.Play();
                Instantiate(bulletPrefab, firePoint.position, RotationAngle);
            }
            ShotGunAmmo--;
        }
        else
        {
            GunShot.Play();
            Instantiate(bulletPrefab, firePoint.position, firePoint.transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//all of this is new
        if (collision.CompareTag("MonsterItem"))
        {
            isMonster = true;
            cam.cullingMask |= (1 << 9);
            gameObject.tag = "Monster";
            currentHealth = monster_max_health;
            healthbar.maxValue = monster_max_health;
            healthbar.value = currentHealth;
            anim.SetBool("IsMonster", true);
            sprite_object.GetComponent<SpriteRenderer>().sprite = MonsterSprite;
            //GetComponent<SpriteRenderer>().sprite = MonsterSprite;
            
            //flashLight.GetComponent<Light>().color = Color.red;
            flashLight.SetActive(false);
            Trail.SetActive(false);
            move_speed = monster_speed;
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            MonsterGrowl.Play();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("ShotGun"))
        {
            if (!isMonster)
            {
                hasShotGun = true;
                ShotGunAmmo = 10;
                //GetComponent<SpriteRenderer>().sprite = MonsterSprite;
                FireDelay = 0.45f;
                spawnManager.GetComponent<SpawnSystem>().counter--;
                Destroy(collision.gameObject);
            }
        }
        else if (collision.CompareTag("HealthPack"))
        {
            if (!isMonster)
            {
                currentHealth += 20;
                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;
                healthbar.value = currentHealth;
                spawnManager.GetComponent<SpawnSystem>().counter--;
                Destroy(collision.gameObject);
            }
        }
        else if (collision.CompareTag("MachineGun"))
        {
            if (!isMonster)
            {
                FireDelay = .10f;
                StartCoroutine(MachinegunActive());
                Destroy(collision.gameObject);
            }
        }
        else if (collision.CompareTag("SpeedBoost"))
        {
            if (!isMonster)
            {
                move_speed = 10f;
                StartCoroutine(MovementSpeedActive());
                Destroy(collision.gameObject);
            }
        }
    }

    static public RaycastHit2D BoxCast(Vector2 origen, Vector2 size, float angle, Vector2 direction, float distance, int mask)
    {
        RaycastHit2D hit = Physics2D.BoxCast(origen, size, angle, direction, distance, mask);

        //Setting up the points to draw the cast
        Vector2 p1, p2, p3, p4, p5, p6, p7, p8;
        float w = size.x * 0.5f;
        float h = size.y * 0.5f;
        p1 = new Vector2(-w, h);
        p2 = new Vector2(w, h);
        p3 = new Vector2(w, -h);
        p4 = new Vector2(-w, -h);

        Quaternion q = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        p1 = q * p1;
        p2 = q * p2;
        p3 = q * p3;
        p4 = q * p4;

        p1 += origen;
        p2 += origen;
        p3 += origen;
        p4 += origen;

        Vector2 realDistance = direction.normalized * distance;
        p5 = p1 + realDistance;
        p6 = p2 + realDistance;
        p7 = p3 + realDistance;
        p8 = p4 + realDistance;


        //Drawing the cast
        Color castColor = hit ? Color.red : Color.green;
        Debug.DrawLine(p1, p2, castColor);
        Debug.DrawLine(p2, p3, castColor);
        Debug.DrawLine(p3, p4, castColor);
        Debug.DrawLine(p4, p1, castColor);

        Debug.DrawLine(p5, p6, castColor);
        Debug.DrawLine(p6, p7, castColor);
        Debug.DrawLine(p7, p8, castColor);
        Debug.DrawLine(p8, p5, castColor);

        Debug.DrawLine(p1, p5, Color.grey);
        Debug.DrawLine(p2, p6, Color.grey);
        Debug.DrawLine(p3, p7, Color.grey);
        Debug.DrawLine(p4, p8, Color.grey);
        if (hit)
        {
            Debug.DrawLine(hit.point, hit.point + hit.normal.normalized * 0.2f, Color.yellow);
        }

        return hit;
    }

    public void checkAmmo()
    {
        if (hasShotGun && ShotGunAmmo <= 0)
        {
            hasShotGun = false;
            FireDelay = 0.25f;
        }
    }

    IEnumerator MachinegunActive()
    {
        yield return new WaitForSeconds(5);
        FireDelay = .25f;
    }
    IEnumerator MovementSpeedActive()
    {
        yield return new WaitForSeconds(5);
        move_speed = reg_speed;
    }

}
