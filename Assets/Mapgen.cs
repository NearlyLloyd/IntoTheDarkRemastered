using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapgen : MonoBehaviour
{
    public GameObject room;
    public GameObject player;
    // Start is called before the first frame update
    private GameObject[,] layout = new GameObject[5,5];
    public enum genMethod
    {
        original, //method1 - how original intothedark generated
        test //hopefully more in depth generation method
    }
    public genMethod generationMethod;

    private Dictionary<string,string> genDictionary = new Dictionary<string, string>
    {
        {"original", "method1"}, 

    }; //update dictionary alongside enum methods added
    private string generationMethodString;

    void Start()
    {

        bool v = genDictionary.TryGetValue(generationMethod.ToString(), out string genMethodString); //get string from enum and compare to the dictionary, converting the enum names to the actual method names.
        generationMethodString = genMethodString; //setting the string to global string.
        Invoke(generationMethodString + "Start", 0f); //calling the methodname, using the dictionary eg: method1 + start.
        
    }

    // Update is called once per frame
    void Update()
    {

        Invoke(generationMethodString + "Update", 0f);
    }

    public void method1Start()
    {
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                layout[i + 2, j + 2] = Instantiate(room);
                //GameObject g = Instantiate(room);
                layout[i + 2, j + 2].transform.position = new Vector3(i * 10, 0, j * 10);
            }

        }
    }
    public void method1Update()
    {
        //if player travels too far, unload the room and load a new set of rooms in front of player - each room is 10x10
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                GameObject temp = layout[i+2,j+2];
                
                if(player.transform.position.x  >= temp.transform.position.x + 25)
                {
                    Vector3 temp2 = layout[i + 2, j + 2].transform.position;
                    Destroy(layout[i+2,j+2]);
                    layout[i + 2, j + 2] = Instantiate(room);
                    layout[i+2,j+2].transform.position = new Vector3(temp2.x + 50, temp2.y, temp2.z);
                    
                }
                else if (player.transform.position.x <= temp.transform.position.x - 25)
                {
                    Vector3 temp2 = layout[i + 2, j + 2].transform.position;
                    Destroy(layout[i + 2, j + 2]);
                    layout[i + 2, j + 2] = Instantiate(room);
                    layout[i + 2, j + 2].transform.position = new Vector3(temp2.x - 50, temp2.y, temp2.z);

                }
                if (player.transform.position.z >= temp.transform.position.z + 25)
                {
                    Vector3 temp2 = layout[i + 2, j + 2].transform.position;
                    Destroy(layout[i + 2, j + 2]);
                    layout[i + 2, j + 2] = Instantiate(room);
                    layout[i + 2, j + 2].transform.position = new Vector3(temp2.x , temp2.y, temp2.z + 50);

                }
                else if (player.transform.position.z <= temp.transform.position.z - 25)
                {
                    Vector3 temp2 = layout[i + 2, j + 2].transform.position;
                    Destroy(layout[i + 2, j + 2]);
                    layout[i + 2, j + 2] = Instantiate(room);
                    layout[i + 2, j + 2].transform.position = new Vector3(temp2.x , temp2.y, temp2.z - 50);

                }
            }

        }
    }

}
