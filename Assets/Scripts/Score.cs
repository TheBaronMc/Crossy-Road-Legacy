using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI text;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        DisplayScore();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayScore();
    }


    public void AddScore(int score)
    {
        count += score;
        DisplayScore();
    }

    private void DisplayScore()
    {
        text.text = "Score: " + count.ToString();
    }
}
