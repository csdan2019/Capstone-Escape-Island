using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    //Inventory items and interactables
    public List<string> items = new List<string>();
    public List<string> interactList = new List<string>();

    //World Manager
    public static WorldManager Overlord;

    //bools for each item the player can collect
    public bool hasAxe = false;
    public bool hasWood = false;
    public bool hasLighter = false;
    public bool hasTorch = false;
    public bool hasSword = false;
    public bool hasCrystal = false;
    public bool hasBottle = false;
    public bool hasPicture = false;
    public bool hasSpear = false;
    public bool hasOars = false;

    //bools for key events that will happen during the game
    public bool beachFireLit = false;
    public bool hasEnteredCave = false;
    public bool northBrazierLit = false;
    public bool southBrazierLit = false;
    public bool eastBrazierLit = false;
    public bool westBrazierLit = false;
    public bool stairSpawned = false; 
    public bool bottlePlaced = false;
    public bool chestOpened = false;
    public bool hasEscaped = false;
    
    
    void Start()
    {
        //Set up the specific interactables by their Tag
        interactList.Add("TreeTag");
        interactList.Add("Lighter");
        interactList.Add("Torch");
        interactList.Add("Sword");
        interactList.Add("Crystal");
        interactList.Add("Spear");
        interactList.Add("Bottle");
        interactList.Add("Picture");
        interactList.Add("Oars");

        //If this is the first Overlord create it
        //Otherwise, not only does the original get kept, but a new one spawns
        if (Overlord != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Overlord = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}
