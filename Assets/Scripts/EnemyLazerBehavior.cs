using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLazerBehavior : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15;
    [SerializeField] AudioClip enemyLazerSoundClip;
    AudioSource audioData;
    private Player_Controller playerCode;
    // Start is called before the first frame update
    void Start()
    {
        playerCode = GameObject.Find("Player").GetComponent<Player_Controller>();
        audioData = GetComponent<AudioSource>();
        if (audioData == null)
        {
            Debug.Log("The Audio Source is missing");
        }
        else
        {
            audioData.clip = enemyLazerSoundClip;
        }
        audioData.Play();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        if (transform.position.y <= -7)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {     
        if (other.tag == "Player")
        {
            playerCode.Damage();
            Destroy(this.gameObject);
        }
        if (other.tag == "Shield")
        {
            Destroy(other.gameObject);
            playerCode.ShieldPowerDown();          
            Destroy((this.gameObject));            
        }
    }
}
