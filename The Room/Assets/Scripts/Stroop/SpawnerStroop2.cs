using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerStroop2 : MonoBehaviour
{
    public GameObject[] boxes;
    private GameObject parentBox;
    private List<GameObject> cubes;
    private GameObject box;
    private float timer;
    public float beat;
    private float var;
    private int count = 0;
    private int randomIndex;
    private bool active;

    void Start()
    {
        parentBox = new GameObject("parentBox");
        cubes = new List<GameObject>();
        timer = 0;

        //creates 200 inactive gameobjects and puts them n a list that we'll pick from for the incoming boxes
        for (int a = 0; a < 200; a++)
        {
            box = Instantiate(boxes[Random.Range(0, 12)]);
            box.SetActive(false);
            cubes.Add(box);
            box.transform.parent = parentBox.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        active = false;
        while (timer > beat && count < 119)
        {
            active = true;
            //count measures how many boxes were spawned
            //var changes the spawn speed after count number of boxes are spawned
            if (count < 21)
            {
                var = 3;
            }
            else if (count < 49)
            {
                var = 1;
            }
            else if (count < 70)
            {
                var = 3;
            }
            else if (count < 98)
            {
                var = 1;
            }
            else if (count < 119)
            {
                var = 3;
            }

            //Picks a random coloured cube and activates it
            do
            {
                randomIndex = Random.Range(0, cubes.Count);
            } while (cubes[randomIndex].activeSelf);
            cubes[randomIndex].SetActive(true);

            //Sets the location of the initial cube at the spawner
            Vector3 temp = new Vector3(0, -1.75f, 6.85f);
            cubes[randomIndex].transform.localPosition = temp;

            timer -= beat + var;
            count++;
        }

        //checking if there are any active cubes
        for (int a = 0; a < 200; a++)
        {
            if (cubes[a].activeSelf)
                active = true;
        }

        //if we reached the end of all cubes that should spawn and there are no more active cubes in the scene >> change scene
        if (count == 119 && active == false)
        {
            SceneManager.LoadScene("StartScreen");
        }

        timer += Time.deltaTime;
    }
}
