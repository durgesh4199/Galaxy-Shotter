using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerBehavior : MonoBehaviour
{

    [SerializeField] float lazerSpeed = 15;

    void Start()
    {

    }
    void Update()
    {

        transform.Translate(0, lazerSpeed * Time.deltaTime, 0);

        if (transform.position.y >= 8)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);
        }
    }

}
