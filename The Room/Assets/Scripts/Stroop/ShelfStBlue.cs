using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShelfStBlue : MonoBehaviour
{
    int score;
    TextMeshProUGUI scoreText;

    public GameObject[] alarm;
    public float colourChangeDelay = 0.5f;
    float currentDelay = 0f;
    bool colourChangeCollision = false;
    public AudioSource sound;

    private void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.GetComponent<Renderer>().tag.Contains("Blue"))
        {
            //deactivating the box that collided with the shelf
            //setting its position to 0,0,0 as otherwise it would restrict the ability to pick up other incoming boxes
            col.collider.gameObject.SetActive(false);
            col.collider.gameObject.transform.localPosition = Vector3.zero;

            //increase score and update display  
            score = PlayerPrefs.GetInt("LevelScore") + 1;
            PlayerPrefs.SetInt("LevelScore", score);
            PlayerPrefs.Save();
            scoreText.SetText(score.ToString());
        }
        else
        {
            colourChangeCollision = true;
            currentDelay = Time.time + colourChangeDelay;
        }

    }

    void checkColourChange()
    {
        if (colourChangeCollision)
        {
            alarm[0].GetComponent<Renderer>().material.color = Color.white;
            alarm[1].GetComponent<Renderer>().material.color = Color.white;

            if (Time.time > currentDelay)
            {
                alarm[0].GetComponent<Renderer>().material.color = Color.black;
                alarm[1].GetComponent<Renderer>().material.color = Color.black;
                colourChangeCollision = false;
            }
        }
    }

    void Update()
    {
        checkColourChange();
    }
}
