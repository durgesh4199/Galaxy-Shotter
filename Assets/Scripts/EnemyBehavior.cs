using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] float moveSpeed = 4;
    private Player_Controller playerScript;
    private Animator Enemyanim;
    private BoxCollider2D enemyColl;
    [SerializeField] AudioClip explosionSoundClip;
    [SerializeField] GameObject enemyLazerPrefab;
    AudioSource audioData;
    private bool canEnemyAbleToFire = true;
    private bool stopEnemySpwan = false;

    void Start()
    {
        //StartCoroutine(EnemyLazerSpwaning());
        playerScript = GameObject.Find("Player").GetComponent<Player_Controller>();
        if (playerScript == null)
        {
            Debug.LogError("The Player is NULL");
        }
        Enemyanim = GetComponent<Animator>();
        enemyColl = GetComponent<BoxCollider2D>();
        audioData = GetComponent<AudioSource>();
        if (Enemyanim == null)
        {
            Debug.LogError("The Animator is NULL");
        }
        if (audioData == null)
        {
            Debug.Log("Audio Source is missing");
        }
        else
        {
            audioData.clip = explosionSoundClip;
        }
    }
    void Update()
    {        
        SpawnEnemy();
        IsPlayerDead();
        if (canEnemyAbleToFire == true && stopEnemySpwan == false)
        {
            StartCoroutine(EnemyLazerSpwaning());
            canEnemyAbleToFire = false;
        }
    }
    void SpawnEnemy()
    {
        transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
        float randomX = Random.Range(-11f, 11f);

        if (transform.position.y <= -6.5f)
        {
            //transform.position = new Vector3(randomX, 8.5f, 0); // Destory on outoff bound but to spwan again form different location.
            Destroy(this.gameObject); // Destroyy the enemy when out of bounds

        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player_Controller player = other.transform.GetComponent<Player_Controller>();
            if (player != null)
            {
                player.Damage();
            }
            
            Enemyanim.SetTrigger("OnEnemyDeath");
            enemyColliderDisable();
            stopEnemySpwan = true;
            DestoyEnemyRoutine();
            audioData.Play();
        }

        if (other.tag == "Lazer")
        {
            Destroy(other.gameObject);
            Enemyanim.SetTrigger("OnEnemyDeath");
            enemyColliderDisable();
            stopEnemySpwan = true;
            DestoyEnemyRoutine();
            audioData.Play();
            if (playerScript != null)
            {
                playerScript.AddScore(10);
            }
        }

        if (other.tag == "Shield")
        {
            Destroy(other.gameObject);
            Enemyanim.SetTrigger("OnEnemyDeath");
            enemyColliderDisable();
            stopEnemySpwan = true;
            DestoyEnemyRoutine();
            audioData.Play();
            playerScript.ShieldPowerDown();
        }
    }
    private void IsPlayerDead()
    {
        if (playerScript == null)
        {
            Destroy(this.gameObject);
        }
    }
    IEnumerator DestoyEnemyRoutine()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
    private void enemyColliderDisable()
    {
        enemyColl.enabled = !enemyColl.enabled;
    }
    IEnumerator EnemyLazerSpwaning()
    {
        Vector3 lazerOffset = new Vector3(-0.24f, -1.33f, 0);   
        Instantiate(enemyLazerPrefab, transform.position + lazerOffset, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        canEnemyAbleToFire = true;
    }
}
