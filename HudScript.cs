using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject HUD;
    private GameObject messagePanel;
    private GameObject Tree;
    private GameObject Player;
    public Color myColor;
    public int itemList = 1;
    void Start()
    {
        Tree = GameObject.FindWithTag("TreeTag");
        Player = GameObject.FindWithTag("Player");
        
        //allows messagePanel
        //  transform.GetChild(1).gameObject.SetActive(true);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.inputString != "")
        {
            int number;
            bool is_a_number = Int32.TryParse(Input.inputString, out number);
            if (is_a_number && number >= 0 && number < 10)
            {
                //Debug.Log("is_a_number "+number);
                number--;
                for (int i = 0; i < transform.GetChild(0).childCount; i++)
                {
                    if (i == number)
                    {
                       // Debug.Log(" light up" + i);
                        Image itemImage = transform.GetChild(0).GetChild(i).gameObject.GetComponent<Image>();
                        myColor = new Color(.3f, .8f, .2f);
                        itemImage.color = myColor;
                    }
                    else
                    {
                     //   Debug.Log("turn black" + i);
                        Image itemImage = transform.GetChild(0).GetChild(i).gameObject.GetComponent<Image>();
                        myColor = new Color(.0f, .0f, .0f); ;
                        itemImage.color = myColor;
                    }
                }
            }
        }
    }
    
    /*    public GameObject createInventoryImage(GameObject toCreate)
        {
            Debug.Log("createObject Test");
            GameObject testObject;
            testObject = Instantiate(toCreate) as GameObject;
            testObject.transform.parent = arm.transform;
            testObject.transform.position = arm.transform.position;
            return testObject;
        }*/
}

