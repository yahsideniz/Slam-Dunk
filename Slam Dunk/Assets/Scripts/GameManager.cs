using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("----Level Objects")]
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject basketballHoop;
    [SerializeField] private GameObject hoopMagnification;
    [SerializeField] private GameObject[] objectSpawnPoints;
    [SerializeField] private AudioSource[] sounds;
    [SerializeField] private ParticleSystem[] effects;

    [Header("----UI Objects")]
    [SerializeField] private Image[] taskImages;
    [SerializeField] private Sprite taskCompletedSprite;
    [SerializeField] private int ballCount;
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private TextMeshProUGUI levelIndex;
    int successfulShot;
    float fingerPosX;

    void Start()
    {
        //Level index yazildi
        levelIndex.text =SceneManager.GetActiveScene().name;

        for (int i = 0; i < ballCount; i++)
        {
            taskImages[i].gameObject.SetActive(true);
        }

        //Invoke("CreateObject", 3f);
    }

    void CreateObject()
    {
        int randomNumber=Random.Range(0, objectSpawnPoints.Length-1);

        hoopMagnification.transform.position = objectSpawnPoints[randomNumber].transform.position;
        hoopMagnification.SetActive(true);
    }

    void Update()
    {
        if(Time.timeScale!=0)
        {
            // Dokunmatik ekran
            if (Input.touchCount > 0) // Dokunma var mi
            {
                Touch touch = Input.GetTouch(0); // Dokunulan parmagi aldik
                Vector3 TouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10)); // Parmagin ekrandaki yeri alindi

                switch (touch.phase)//parmagin durumunu alcaz ilk dokunma ve sonraki hareket anýný almaliyiz
                {
                    case TouchPhase.Began: // Dokunma basladiysa
                        fingerPosX = TouchPosition.x - platform.transform.position.x; // Parmagin posizyonu bulundu
                        break;

                    case TouchPhase.Moved: // Parmak hareket ettiriliyor mu
                        if (TouchPosition.x - fingerPosX > -1.20 && TouchPosition.x - fingerPosX < 1.20)
                        {
                            platform.transform.position = Vector3.Lerp(platform.transform.position,
                              new Vector3(TouchPosition.x - fingerPosX,
                              platform.transform.position.y, platform.transform.position.z), 5f);
                        }
                        break;
                }
            }
        }
    }


    // Basarili atis oldukca
    public void Basket(Vector3 Pos)
    {
        successfulShot++;
        taskImages[successfulShot - 1].sprite = taskCompletedSprite; // 0. indexten baslamasi gerektigi icin -1 yazildi
        effects[0].transform.position = Pos;
        effects[0].gameObject.SetActive(true);
        sounds[1].Play();


        // Level tamamlandiysa
        if (successfulShot==ballCount)
        {
            Win();
        }

        if (successfulShot == 1)
        {
            CreateObject();
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;

        sounds[2].Play();

        Panels[2].SetActive(true);

        Time.timeScale = 0;
    }

    void Win()
    {
        Time.timeScale = 0;

        sounds[3].Play();

        Panels[1].SetActive(true);

        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

        Time.timeScale = 0;
    }

    public void BasketballHoopMagnification(Vector3 Pos)
    {
        effects[1].transform.position = Pos;
        effects[1].gameObject.SetActive(true);

        sounds[0].Play();
        basketballHoop.transform.localScale = new Vector3(55f, 55f, 55f);
    }

    public void ButtonOperations(string Value)
    {
        switch (Value)
        {
            case "Pause":
                Time.timeScale = 0;
                Panels[0].SetActive(true);
                break;

            case "Resume":
                Time.timeScale = 1;
                Panels[0].SetActive(false);
                break;

            case "TryAgain":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                break;

            case "NextLevel":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                Time.timeScale = 1;
                break;

            case "Quit":
                Application.Quit();
                break;

        }
    }
}
