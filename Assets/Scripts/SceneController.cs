using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Globalization;
using TMPro;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject car;
    [SerializeField] GameObject player;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject spawnPoint;

    public TextMeshProUGUI time;
    public TextMeshProUGUI end;
    public GameObject exitButton;

    public float GameTime;

    public int MAX_CAR;
    public const float SPAWN_DELTA_S = 1f;

    private List<GameObject> LeftToRightCars;
    private List<GameObject> RightToLeftCars;

    private List<float> yRightStartPositions;
    private List<float> yLeftStartPositions;

    private float xLeftStartPoint;
    private float xRightStartPoint;

    private bool toTheLeft = true;
    private int startPositionIndex = 0;
    private float lastSpawn;

    private Party game = new Party();

    // Start is called before the first frame update
    void Start()
    {
        MAX_CAR = PlayerPrefs.GetInt("ENNEMIES");
        player.GetComponent<ReactiveTarget>().SetNbLife(PlayerPrefs.GetInt("LIFE"));
        GameTime = PlayerPrefs.GetInt("TIME");

        LeftToRightCars = new List<GameObject>();
        RightToLeftCars = new List<GameObject>();

        yRightStartPositions = new List<float> { -6.5f, 0.5f, 7.5f };
        yLeftStartPositions = new List<float> { 5.5f, -1.5f, -8.5f };

        xLeftStartPoint = -28f;
        xRightStartPoint = 15f;

        lastSpawn = 0;

        cam.transform.SetParent(player.transform);

        player.GetComponent<ReactiveTarget>().SetBeforeDeath(new BeforeDeathCallBack(cam, end, game));

        end.enabled = false;
        exitButton.SetActive(false);

        createCar();
    }

    // Update is called once per frame
    void Update()
    {
        createCar();
        removeCar();

        if (!game.IsEnded())
        {
            GameTime -= Time.deltaTime;
            if (GameTime <= 0)
            {
                game.SetEnded();
                end.text = "Party End";
                player.GetComponent<Chicken>().disableControl();
                StartCoroutine("Respawn");
            }
            time.text = "Time: " + ((int)GameTime).ToString();
        }
            

        if (game.IsEnded())
        {
            end.enabled = true;
            exitButton.SetActive(true);
        }
    }

    void createCar()
    {
        lastSpawn += Time.deltaTime;

        if (LeftToRightCars.Count + RightToLeftCars.Count == MAX_CAR || lastSpawn <= SPAWN_DELTA_S)
            return;

        GameObject car = Instantiate(this.car) as GameObject;

        if (toTheLeft)
        {
            car.transform.Rotate(0, 180, 0);
            car.transform.position = new Vector3(
                xRightStartPoint,
                0.22f,
                yRightStartPositions[startPositionIndex]);
            RightToLeftCars.Add(car);
        } else
        {
            car.transform.position = new Vector3(
                xLeftStartPoint,
                0.22f,
                yLeftStartPositions[startPositionIndex]);
            LeftToRightCars.Add(car);
        }

        startPositionIndex = (startPositionIndex + 1) % 3;
        toTheLeft = !toTheLeft;

        lastSpawn = 0f;
    }

    void removeCar()
    {
        List<GameObject> toDestroy = new List<GameObject>();

        for (int i=LeftToRightCars.Count-1; i >= 0; i--)
        {
            if (LeftToRightCars[i] == null)
            {
                LeftToRightCars.RemoveAt(i);
            }
            else if (LeftToRightCars[i].transform.position.x >= xRightStartPoint)
            {
                toDestroy.Add(LeftToRightCars[i]);
                LeftToRightCars.RemoveAt(i);
            }
        }

        for (int i = RightToLeftCars.Count-1; i >= 0; i--)
        {
            if (RightToLeftCars[i] == null)
            {
                RightToLeftCars.RemoveAt(i);
            }
            else if (RightToLeftCars[i].transform.position.x <= xLeftStartPoint)
            {
                toDestroy.Add(RightToLeftCars[i]);
                RightToLeftCars.RemoveAt(i);
            }
        }

        foreach (GameObject car in toDestroy)
        {
            Destroy(car);
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(0.01f);
        player.transform.position = spawnPoint.transform.position;
        player.transform.eulerAngles = new Vector3(0, 180, 0);
    }

    class BeforeDeathCallBack : CallBack
    {
        GameObject cam;
        TextMeshProUGUI end;
        Party game;

        public BeforeDeathCallBack(GameObject cam, TextMeshProUGUI end, Party game)
        {
            this.cam = cam;
            this.game = game;
            this.end = end;
        }

        public void Call()
        {
            cam.transform.SetParent(null);
            end.text = "Game Over";
            game.SetEnded();
        }
    }

    class Party
    {
        bool ended = false;

        public bool IsEnded()
        {
            return ended;
        }

        public void SetEnded()
        {
            ended = true;
        }
    }
}