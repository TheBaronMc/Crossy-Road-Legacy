using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;

    [SerializeField] GameObject gameController;

    public AudioSource sound;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject fireball = Instantiate(fireballPrefab, transform.position, transform.rotation) as GameObject;
            fireball.GetComponent<FireBall>().SetOnHit(new FireBallCallback(gameController));

            Rigidbody fireballRigibody = fireball.GetComponent<Rigidbody>();

            Vector3 forceToAdd = (transform.forward*-1) * 10f + transform.up;

            fireballRigibody.AddForce(forceToAdd, ForceMode.Impulse);

            if (sound != null)
                sound.Play();
        }
    }

    private class FireBallCallback: CallBack
    {
        GameObject gameController;

        public FireBallCallback(GameObject gameController)
        {
            this.gameController = gameController;
        }

        public void Call()
        {
            Score score = this.gameController.GetComponent<Score>();
            score.AddScore(100);
        }
    }
}
