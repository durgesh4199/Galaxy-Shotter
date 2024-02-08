using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] float movSpeed = 3;
    // PowerupIds- 0 = tripleShot , 1 = Speed , 2 = Shield
    [SerializeField] int powerUpID = 0;
    [SerializeField] AudioClip powerPickSoundClip;
    AudioSource audioData;
    void Start()
    {
        audioData = GameObject.Find("PowerupPickSound").GetComponent<AudioSource>();
        if (audioData == null)
        {
            Debug.Log("Audio Source is missing");
        }
        else
        {
            audioData.clip = powerPickSoundClip;
        }
    }
    void Update()
    {
        PowerUpBehaviour();
    }
    private void PowerUpBehaviour()
    {
        transform.Translate(0, -1 * movSpeed * Time.deltaTime,  0);

        if (transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player_Controller player = other.transform.GetComponent<Player_Controller>();

            if (player != null)
            {
                switch (powerUpID)
                {
                    case 0:
                        if (player.stillActive[0] == false)
                        {
                            player.TripleShotActivator();
                            audioData.Play();
                            Destroy(this.gameObject);                            
                        }                     
                        break;
                    case 1:
                        if (player.stillActive[1] == false)
                        {
                            player.SpeedActivator();
                            audioData.Play();
                            Destroy(this.gameObject);                           
                        }                   
                        break;
                    case 2:
                        if (player.stillActive[2] == false)
                        {
                            player.ShieldActivator();
                            audioData.Play();
                            Destroy(this.gameObject);
                        }
                        break;
                    default:
                        Debug.Log("Default Case");
                        break;
                }
            }
        }
    }   

}
