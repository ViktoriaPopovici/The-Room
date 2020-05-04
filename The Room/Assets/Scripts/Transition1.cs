using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition1 : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        PlayerPrefs.SetInt("LevelScore", 0);
        SceneManager.LoadScene("StroopRoom");
    }
}
