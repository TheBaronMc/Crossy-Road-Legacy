using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLife : MonoBehaviour
{
    public TextMeshProUGUI life;
    public ReactiveTarget player;

    // Start is called before the first frame update
    void Start()
    {
        DisplayPlayerLife();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayPlayerLife();
    }

    private void DisplayPlayerLife()
    {
        life.text = "Life: " + player.GetNbLife();
    }
}
