using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("LevelScore", 0);
        SceneManager.LoadScene("StroopRoom2");
    }
}
