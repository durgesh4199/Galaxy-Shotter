using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15;
    [SerializeField] float moveSpeedMultiplier = 2;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] int _LifeLeft_P1 = 3;
    //[SerializeField] int _LifeLeft_P2 = 3;
    private float _nextFire = 0;

    [SerializeField] GameObject lazerPrefab;
    [SerializeField] GameObject tripleShotPrefab;
    [SerializeField] GameObject shieldPrefab;
    [SerializeField] GameObject shieldFollowPlayerParent;

    private Scene_Manager sceneManagerScript;
    private UIManager uiGO;
    private SpawnManager spawnManagerGB;
    public Animator playerLeftHurt;
    public Animator playerRightHurt;
    private Animator speedAnim;
    private Animator playerDeathAC;
    [SerializeField] AudioClip lazeraudioClip;
    AudioSource audiodata;

    private bool isTripleShotActive = false;
    private bool isShieldActive = false;
    public bool[] stillActive = new bool[3] {false, false, false};
    [SerializeField] int playerID = 0;
    private int score = 0;

    void Start()
    {
        sceneManagerScript = GameObject.Find("SceneManager").GetComponent<Scene_Manager>();
        spawnManagerGB = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        uiGO = GameObject.Find("Canvas").GetComponent<UIManager>();
        speedAnim = GameObject.Find("SpeedPowerUp_Thrusters").GetComponent<Animator>();
        audiodata = GetComponent<AudioSource>();
        playerDeathAC = GetComponent<Animator>();

        if (sceneManagerScript.isCoopModeActive == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }         
        if (audiodata == null)
        {
            Debug.Log("Audio Source is not Assigned");
        }
        else
        {
            audiodata.clip = lazeraudioClip;
        }
    }
    void Update()
    {
        Player1Controller();
        //Player2Controller();
        BoundPositionPlayer();
        FireLazer();
        ShieldSpwan();
    }
    private void BoundPositionPlayer()
    {
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -3.8f)
        {
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x >= 10.7)
        {
            transform.position = new Vector3(-10.7f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10.7)
        {
            transform.position = new Vector3(10.7f, transform.position.y, 0);
        }
    }
    private void Player1Controller()
    {
        if (playerID == 1)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 directions = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(directions * moveSpeed * Time.deltaTime);
        }    
    }
    /*private void Player2Controller()
    {
        if (playerID == 2)
        {
            float horizontalInput = Input.GetAxis("HorizontalP2");
            float verticalInput = Input.GetAxis("VerticalP2");
            Vector3 directions = new Vector3(horizontalInput, verticalInput, 0);
            transform.Translate(directions * moveSpeed * Time.deltaTime);
        }      
    }*/
    private void FireLazer()
    {
        Vector3 lazerOffset = new Vector3(0, 0.7f, 0);
        Vector3 tripleShotOffset = new Vector3(0, 0.6f, 0);
        if (Time.time > _nextFire)
        {         
            if ((playerID == 1 && Input.GetKeyDown(KeyCode.Space)))
            {
                if (isTripleShotActive == false)
                {
                   _nextFire = Time.time + fireRate;
                   Instantiate(lazerPrefab, transform.position + lazerOffset, Quaternion.identity);
                   audiodata.Play();
                }
                else
                {
                   _nextFire = Time.time + fireRate;
                   Instantiate(tripleShotPrefab, transform.position + tripleShotOffset, Quaternion.identity);
                   audiodata.Play();
                }
            }
        }
    }
    private void ShieldSpwan()
    {
        if (isShieldActive == true)
        {
            GameObject newSheild = Instantiate(shieldPrefab, transform.position, Quaternion.identity);
            newSheild.transform.parent = shieldFollowPlayerParent.transform;
            isShieldActive = false;
        }
    }
    public void Damage()
    {       
        if (playerID == 1)
        {
            if (_LifeLeft_P1 > 0)
            {
                _LifeLeft_P1--;
            }
            playerLeftHurt.SetInteger("Lives", _LifeLeft_P1);
            playerRightHurt.SetInteger("Lives1", _LifeLeft_P1);
            playerDeathAC.SetInteger("Lives2", _LifeLeft_P1);
            uiGO.LifeLeftUpdaterP1(_LifeLeft_P1);
            if (_LifeLeft_P1 < 1)
            {
                Destroy(this.gameObject);
                spawnManagerGB.onPlayerDeath();              
            }
        }
        /*else if (playerID == 2)
        {
            if (_LifeLeft_P2 > 0)
            {
                _LifeLeft_P2--;
            }
            playerLeftHurt.SetInteger("Lives", _LifeLeft_P2);
            playerRightHurt.SetInteger("Lives1", _LifeLeft_P2);
            playerDeathAC.SetInteger("Lives2", _LifeLeft_P2);
            uiGO.LifeLeftUpdaterP1(_LifeLeft_P2);
            if (_LifeLeft_P2 < 1)
            {
                Destroy(this.gameObject);     
                if (_LifeLeft_P1 < 1)
                {
                    spawnManagerGB.onPlayerDeath();
                }
            }
        }*/        

    }
    public void TripleShotActivator()
    {
        isTripleShotActive = true;
        stillActive[0] = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
                                                                          
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isTripleShotActive = false;
        stillActive[0] = false;
    }
    public void SpeedActivator()
    {
        moveSpeed *= moveSpeedMultiplier;
        stillActive[1] = true;
        speedAnim.SetBool("Thruster", true);
        StartCoroutine(SpeedPowerDownRoutine());
    }
    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        moveSpeed /= moveSpeedMultiplier;
        stillActive[1] = false;
        speedAnim.SetBool("Thruster", false);
    }
    public void ShieldActivator()
    {
        isShieldActive = true;
        stillActive[2] = true;
    }

    public void ShieldPowerDown()
    {
        isShieldActive = false;
        stillActive[2] = false;
    }
    public void AddScore(int AddScore)
    {
        score += AddScore;
        if (uiGO != null)
        {
            uiGO.UpdateScore(score);
        }
    }
}
