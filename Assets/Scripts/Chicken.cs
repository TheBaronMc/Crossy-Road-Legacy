using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public float sensitivityHor = 9.0f;
    public float speed = 9.0f;
    public float gravity = -9.8f;

    public bool controlEnabled = true;

    public AudioSource Crawl;

    private CharacterController charController;

    // Start is called before the first frame update
    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled)
            return;

        // Translate
        float deltaX = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(0, 0, -deltaX);

        movement = Vector3.ClampMagnitude(movement, speed);
        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        charController.Move(movement);

        // Rotation
        transform.Rotate(0, Input.GetAxis("Horizontal") * sensitivityHor * Time.deltaTime, 0);

        if (Input.GetMouseButtonDown(1) && Crawl != null)
            Crawl.Play();

    }

    public void enableControl()
    {
        enabled = true;
        gameObject.GetComponent<Spell>().enabled = true;
    }

    public void disableControl()
    {
        enabled = false;
        gameObject.GetComponent<Spell>().enabled = false;
    }
}
