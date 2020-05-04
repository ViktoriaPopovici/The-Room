using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    public GameObject[] alarm;
    public float colourChangeDelay = 0.5f;
    float currentDelay = 0f;
    bool colourChangeCollision = false;
    public AudioSource sound;

    private void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    void OnCollisionEnter()
    {
        colourChangeCollision = true;
        currentDelay = Time.time + colourChangeDelay;

        sound.Play();
    }
    void checkColourChange()
    {
        if (colourChangeCollision)
        {
            alarm[0].GetComponent<Renderer>().material.color = new Color(153, 0, 0);
            alarm[1].GetComponent<Renderer>().material.color = new Color(153, 0, 0);

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
