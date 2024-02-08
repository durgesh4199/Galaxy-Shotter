using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 50.0f;
    private Animator asteroidAnim;
    private SpawnManager spawnManagerScript;
    private UIManager uiScript;
    [SerializeField] AudioClip explosionSoundClip;
    AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        asteroidAnim = GetComponent<Animator>();
        spawnManagerScript = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        uiScript = GameObject.Find("Canvas").GetComponent<UIManager>();
        audioData = GetComponent<AudioSource>();
        if (audioData == null)
        {
            Debug.Log("Audio Source is missing");
        }
        else
        {
            audioData.clip = explosionSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Lazer")
        {
            Destroy(other.gameObject);
            asteroidAnim.SetTrigger("Explosion");
            StartCoroutine(DestroyedAstroidRoutine());
            audioData.Play();
            uiScript.disableInfoText();
        }
    }
    IEnumerator DestroyedAstroidRoutine()
    {
        yield return new WaitForSeconds(2.4f);
        Destroy(this.gameObject);
        spawnManagerScript.startSpawn();
    }
}
