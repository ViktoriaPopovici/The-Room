using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    public static int score;
    private int temp;
    //TextMeshProUGUI scoreTextMissed;

    private void Start()
    {
       // scoreTextMissed = GameObject.Find("ScoreTextMissed").GetComponent<TextMeshProUGUI>();
        score = PlayerPrefs.GetInt("Score",0);

    }
    //When the "Box" GameObjects collide with the "Pit" they get deactivated 
    //their position is also set to 0,0,0 as to not restrict the ability to pick up incoming boxes
    void OnCollisionEnter(Collision collision)
    {
        collision.collider.gameObject.SetActive(false);
        collision.collider.gameObject.transform.localPosition = Vector3.zero;
        temp++;
        PlayerPrefs.SetInt("Score", score+temp);
        PlayerPrefs.Save();
    }

}