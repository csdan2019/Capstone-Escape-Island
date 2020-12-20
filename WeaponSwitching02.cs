using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching02 : MonoBehaviour
{
    public int currentWeapon = 0;
    public int maxWeapons;
    public Animator theAnimator;
    public GameObject newObject;
    public GameObject arm;
    public List<string> items = new List<string>();
    
    // Start is called before the first frame update
    void Awake()
    {
        arm = GameObject.FindWithTag("Arm");
        SelectWeapon(0);
    }

    public void rotateWeapon(GameObject weapon)
    {
        if (weapon.tag == "Axe")
        {
            weapon.transform.rotation = Quaternion.Euler(0f, 0, 90);
        }
        else if (weapon.tag == "Torch" || weapon.tag == "Sword")
        {
            weapon.transform.rotation = Quaternion.Euler(-90f, 0, 90);
            if (weapon.tag == "Sword")
            {
                weapon.transform.localScale = new Vector3(.5f, .5f, .3f);

            }
        }
        else if (weapon.tag == "Oars")
        {
            weapon.transform.localScale = new Vector3(.3f, .5f, .5f);

            weapon.transform.rotation = Quaternion.Euler(-90f, 60f, 2f);
        }
        else if (weapon.tag == "Spear")
        {
            weapon.transform.localScale = new Vector3(.3f, .3f, .3f);

            weapon.transform.rotation = Quaternion.Euler(0f, 90f, 2f);
        }
    }
    public void moveObjectToArm(GameObject weapon)
    {
        weapon.transform.parent = arm.transform;
        weapon.transform.localPosition = Vector3.zero;
        rotateWeapon(weapon);
        weapon.layer = 8;
        UpdateLayer(weapon, 8);

    }
    public void createObject(GameObject toCreate)
    {
        Debug.Log("createObject Test");
        GameObject testObject;
        testObject = Instantiate(toCreate) as GameObject;
        testObject.transform.parent = arm.transform;
        testObject.transform.localPosition = Vector3.zero;
        rotateWeapon(testObject);
        testObject.layer = 8;
        UpdateLayer(testObject, 8);
        // testObject.transform.localRotation = Quaternion.identity;
        /*      testObject = Instantiate(toCreate) as GameObject;
       testObject.transform.parent = arm.transform;
       testObject.transform.position = arm.transform.position;*/
    }
    
    void UpdateLayer(GameObject parentObject, int layer)
    {
        for (int i = 0; i < parentObject.transform.childCount; i++)
        {
            parentObject.transform.GetChild(i).gameObject.layer = 8;
            UpdateLayer(parentObject.transform.GetChild(i).gameObject, 8);
        }
    }
    void Update()
    {
        /*        GameObject testObject;
                if (Input.GetKeyDown(KeyCode.M))
                {
                   testObject= createObject(newObject);
​
        *//*            testObject = Instantiate(newObject) as GameObject;
                    testObject.transform.parent = arm.transform;
                    testObject.transform.position = arm.transform.position;*//*
                }*/
        maxWeapons = transform.childCount;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (currentWeapon + 1 <= maxWeapons)
            {
                currentWeapon++;
            }
            else
            {
                currentWeapon = 0;
            }
            //  print("scrolll wheel activated");
            SelectWeapon(currentWeapon);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (currentWeapon - 1 >= 0)
            {
                currentWeapon--;
            }
            else
            {
                currentWeapon = maxWeapons;
            }
            SelectWeapon(currentWeapon);
        }

        if (currentWeapon == maxWeapons + 1)
        {
            currentWeapon = 0;
        }
        if (currentWeapon == -1)
        {
            currentWeapon = maxWeapons;
        }

        //takes numperpad input and selects the weapon
        if (Input.inputString != "")
        {
            int number;
            bool is_a_number = Int32.TryParse(Input.inputString, out number);
            if (is_a_number && number >= 0 && number < 10)
            {
                number--;
                SelectWeapon(number);
            }
        }

        /*        if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    currentWeapon = 0;
                    SelectWeapon(currentWeapon);
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    currentWeapon = 1;
                    SelectWeapon(currentWeapon);
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    currentWeapon = 2;
                    SelectWeapon(currentWeapon);
                }*/
        // print("current weapon is " + currentWeapon);

        //attack
        if (Input.GetMouseButtonDown(1))
        {
            theAnimator.SetBool("Hit01", true);
        }
        else
        {
            theAnimator.SetBool("Hit01", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            theAnimator.SetBool("Hit02", true);
        }
        else
        {
            theAnimator.SetBool("Hit02", false);
        }
    }

    //selects what weapon to put in hand
    public void SelectWeapon(int index)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            //activate the selected weapon
            if (i == index)
            {
                if (transform.GetChild(i).name == "Fists")
                {
                    // print("Fists");
                    theAnimator.SetBool("WeaponIsOn", false);
                }
                else
                {
                    theAnimator.SetBool("WeaponIsOn", true);
                }
                transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}

