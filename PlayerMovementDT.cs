using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

#pragma warning disable 618, 649

public class PlayerMovementDT : MonoBehaviour
{
    //public List<string> interactList = new List<string>();
    public List<string> tempInventory = new List<string>();
    private WeaponSwitching02 weaponScript;
    public GameObject realWeapon;
    // public GameObject newlyCreated;

    public int itemList = 2;
    public GameObject tree;

    public CharacterController controller;
    private GameObject HUD;
    private HudScript hudScript;

    public float moveSpeed = 10f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public Transform groundCheck;
    //groundDistance creates the sphere to detect ground
    public float groundDistance = 0.4f;
    //groundMask registers what objects it should check for, such as ground only
    public LayerMask groundMask;
    bool isGrounded;

    private RaycastHit hit;
    private Ray ray;
    public float rayDistance = 4f;

    //store current velocity
    Vector3 velocity;
    // Use this for initialization
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        tree = GameObject.FindWithTag("TreeTag");
        HUD = GameObject.FindWithTag("HUD");
        realWeapon = GameObject.FindWithTag("Arm");
        weaponScript = realWeapon.GetComponent<WeaponSwitching02>();
        hudScript = HUD.GetComponent<HudScript>();
        PopulateInventory();
        weaponScript.SelectWeapon(0);
    }
    public float hudTime = 3;

    // Update is called once per frame
    void Update()
    {
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 20f;
        }
        else
        {
            moveSpeed = 10f;
        }
        // Ray is automatic object of player
        ray = new Ray(transform.position + new Vector3(0f, controller.center.y, 0f), transform.forward);

        //ray origin is at transform.position
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
   
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //force player down to the ground
        if (velocity.y < 0)
        {
            velocity.y = -30f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + controller.transform.forward * z;

        //controller.transform.position = transform.position + Camera.main.transform.forward;
        controller.Move(move * moveSpeed * Time.deltaTime);
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        //resets popup message for pickup and hud display
        hudTime -= Time.deltaTime;
        if (hudTime < 0)
        {
            HUD.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    void UpdateOnPickUp(string pickedupItem)
    {
        GameObject Overlord = GameObject.Find("WorldManager");

        WorldManager worldMem = Overlord.GetComponent<WorldManager>();

        if (pickedupItem == "Axe")
            worldMem.hasAxe = true;

        if (pickedupItem == "TreeTag")
            worldMem.hasWood = true;

        if (pickedupItem == "Lighter")
            worldMem.hasLighter = true;

        if (pickedupItem == "Torch")
            worldMem.hasTorch = true;

        if (pickedupItem == "Sword")
            worldMem.hasSword = true;

        if (pickedupItem == "Crystal")
            worldMem.hasCrystal = true;

        if (pickedupItem == "Spear")
            worldMem.hasSpear = true;

        if (pickedupItem == "Bottle")
            worldMem.hasBottle = true;

        if (pickedupItem == "Picture")
            worldMem.hasPicture = true;

        if (pickedupItem == "Oars")
            worldMem.hasOars = true;

        Destroy(GameObject.FindGameObjectWithTag(pickedupItem));
    }

    void PopulateInventory()
    {
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();
        foreach (string item in worldMem.items)
        {
            Debug.Log(item);

            if (!tempInventory.Contains(item))
            {
                GameObject newMaze = Instantiate(Resources.Load(item)) as GameObject;
                HUD.transform.GetChild(0).GetChild(hudScript.itemList).GetChild(0).gameObject.SetActive(true);
                Image testImage = HUD.transform.GetChild(0).GetChild(hudScript.itemList).GetChild(0).GetComponent<Image>();
                testImage.sprite = Resources.Load<Sprite>(item + "Image");
                Debug.Log(newMaze.tag);
                weaponScript.moveObjectToArm(newMaze);
                tempInventory.Add(item);
                hudScript.itemList++;
            }
        }
    }

    // Displays message panel upon collision
    void OnTriggerStay(Collider hit)
    {
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();

        if (hit.tag == "Mirror")
        {
            Debug.Log("Mirro hit");
            HUD.transform.GetChild(2).gameObject.SetActive(true);
        }

        if (worldMem.interactList.Contains(hit.tag))
        {
            //displays pickup message by setting hud active
            hudTime = 0.5f;
            HUD.transform.GetChild(1).gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("f pressed");

                if (hit.tag == "TreeTag" && !worldMem.hasAxe)
                    return;

                //if inventory item tag is not in list you may pick it up
                if (!weaponScript.items.Contains(hit.tag))
                {
                    Debug.Log("In first IIF");

                    if (!HUD.transform.GetChild(0).GetChild(hudScript.itemList).GetChild(0).gameObject.activeSelf)
                    {
                        //flags hud inventory icon to active
                        HUD.transform.GetChild(0).GetChild(hudScript.itemList).GetChild(0).gameObject.SetActive(true);
                        //gets image and displays it
                        Image testImage = HUD.transform.GetChild(0).GetChild(hudScript.itemList).GetChild(0).GetComponent<Image>();
                        testImage.sprite = Resources.Load<Sprite>(hit.tag + "Image");
                        Debug.Log("In create object");

                        //creates game object and places in hand
                        weaponScript.createObject(hit.gameObject);
                        weaponScript.SelectWeapon(hudScript.itemList);
                        hudScript.itemList++;
                    }
                    //add item to inventory list, no longer able to pick up
                    weaponScript.items.Add(hit.tag);
                    worldMem.items.Add(hit.tag);
                    // Debug.Log("item Add itemList");
                    tempInventory.Add(hit.tag);
                    //change game world based on item picked up
                    UpdateOnPickUp(hit.tag);
                }
            }
        }
    }
}

