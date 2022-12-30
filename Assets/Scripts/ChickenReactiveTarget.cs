using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChickenReactiveTarget: ReactiveTarget
{
    [SerializeField] GameObject SpawnPoint;

    public override void ReactToHit()
    {
        base.ReactToHit();

        // Respawn
        if (base.NbOfLive > 0)
        {
            StartCoroutine("Respawn");
        }
    }

    private IEnumerator Respawn()
    {
        Chicken player = gameObject.GetComponent<Chicken>();
        player.disableControl();

        yield return new WaitForSeconds(0.01f);

        transform.position = SpawnPoint.transform.position;
        transform.eulerAngles = new Vector3(0, 180, 0);

        yield return new WaitForSeconds(1f);

        player.enableControl();
    }
}

