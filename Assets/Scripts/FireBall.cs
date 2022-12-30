using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireBall : MonoBehaviour
{
    public float speed = 10.0f;

    private CallBack cb;

    void Update()
    {
        //transform.Translate(0, 0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        ReactiveTarget player = other.GetComponent<ReactiveTarget>();
        if (player != null)
        {
            player.ReactToHit();

            if (cb != null)
            {
                cb.Call();
                cb = null;
            }
        }
        Destroy(this.gameObject);
    }

    public void SetOnHit(CallBack cb)
    {
        this.cb = cb;
    }
}
