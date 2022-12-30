using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public float speed = 9.0f;

    public AudioSource horn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        ReactiveTarget target = other.GetComponent<ReactiveTarget>();

        if (target != null)
        {
            if (horn != null)
                horn.Play();
            target.ReactToHit();
        }
    }
}
