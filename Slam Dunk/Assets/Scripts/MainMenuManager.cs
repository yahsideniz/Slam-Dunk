using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
         
        if(PlayerPrefs.HasKey("Level")) // Level diye bir sey var mi kontrol
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level")); // Kaldigi seviyeden devam

        }
        else
        {
            PlayerPrefs.SetInt("Level", 1); // Yoksa Level eklendi
            SceneManager.LoadScene(1);
        }
    }
}
