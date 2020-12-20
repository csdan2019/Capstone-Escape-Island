using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameWorld : MonoBehaviour
{
    //GameObjects that may need Instantiated
    public GameObject beachFire;
    public GameObject brazierFire;
    public GameObject stairReset;
    public GameObject houseBottle;
    public GameObject houseSprite;

    public void OnTriggerEnter(Collider Col)
    {
        //Get the world manager to check for bools
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();

        //If the player has entered the cave, spawn them outside the cave
        //Destroy the Intro audio clues
        if (worldMem.hasEnteredCave)
        {
            Col.transform.position = new Vector3(341f, 86f, 555f);
            Destroy(GameObject.Find("IntroAudio"));
            Destroy(GameObject.Find("Path Fork Audio"));
        }

        //Otherwise spawn them on the initial beach
        else
            Col.transform.position = new Vector3(530f, 20.2f, 950f);

        //If the beach fire is lit, Instantiate it and destroy the beach audio
        if (worldMem.beachFireLit)
        {
            Instantiate(beachFire, new Vector3(860f, 20.2f, 263f), Quaternion.identity);
            Destroy(GameObject.Find("BeachAudio"));

            //Handling edge case where Player doesn't pick up torch after lighting fire then goes in cave
            if (!worldMem.hasTorch)
            {
                GameObject mover = GameObject.Find("Torch");
                mover.transform.position = new Vector3(862f, 20f, 263.5f);
            }
        }

        //If first brazier is lit, destroy Meadow audio clues and ligth brazier
        if (worldMem.northBrazierLit)
        {
            Destroy(GameObject.Find("Meadow Found Audio"));
            Destroy(GameObject.Find("Stonehenge Found Audio"));
            Instantiate(brazierFire, new Vector3(550f, 26.3f, 585f), Quaternion.identity);
        }

        //Brazier lighting for South and East braziers
        if (worldMem.southBrazierLit)
            Instantiate(brazierFire, new Vector3(558f, 26.5f, 618f), Quaternion.identity);

        if (worldMem.eastBrazierLit)
            Instantiate(brazierFire, new Vector3(526.5f, 25.1f, 603f), Quaternion.identity);

        //If last brazier is lit, light it and destroy scroll
        if (worldMem.westBrazierLit)
        {
            Instantiate(brazierFire, new Vector3(581f, 25.5f, 596f), Quaternion.identity);
            Destroy(GameObject.FindGameObjectWithTag("Scroll"));

            //Handle edge case in which Player spawned sword, but did not collect it before entering Cave
            if (!worldMem.hasSword)
            {
                GameObject mover = GameObject.Find("Sword");
                mover.transform.position = new Vector3(551.5f, 27.2f, 600f);
            }
        }

        //If stairs were spawned respawn them and destroy Magic Tree audio
        if (worldMem.stairSpawned)
        {
            Destroy(GameObject.Find("Tree Found Audio"));
            Instantiate(stairReset, new Vector3(600f, 26f, 468f), Quaternion.identity);
        }

        //If the bottle has been placed respawn it on the table and destroy audio clue
        if (worldMem.bottlePlaced)
        {
            Instantiate(houseBottle, new Vector3(199f, 14.4f, 54.6f), Quaternion.identity);
            Destroy(GameObject.Find("Swamp Bottle Found"));

            //Handle edge case where Player solved bottle puzzle, but did not pick up Picture
            //before going back to the Cave for whatever reason
            if (!worldMem.hasPicture)
            {
                GameObject mover = GameObject.Find("Picture");
                mover.transform.position = new Vector3(204.7f, 16f, 53f);
            }
        }

        //If the chest was opened, respawn the sprite, move waterfall into place 
        //Destroy audio clue and lake barrier
        if (worldMem.chestOpened)
        {
            Instantiate(houseSprite, new Vector3(190.3f, 20.3f, 50.0f), Quaternion.identity);
            GameObject mover = GameObject.Find("Waterfall");
            mover.transform.position = new Vector3(278.8f, 17.2f, 186.3f);
            Destroy(GameObject.Find("Lake Barrier"));
            Destroy(GameObject.Find("Swamp Chest Found"));
        }

        //Destroy collected objects
        if (worldMem.hasAxe)
            Destroy(GameObject.Find("Axe"));

        if (worldMem.hasWood)
            Destroy(GameObject.Find("Beach Tree"));

        if (worldMem.hasLighter)
            Destroy(GameObject.Find("Lighter"));

        if (worldMem.hasCrystal)
            Destroy(GameObject.Find("Crystal"));

        if (worldMem.hasBottle)
        {
            Destroy(GameObject.Find("Swamp Entrance Audio"));
            Destroy(GameObject.Find("Church Audio"));
            Destroy(GameObject.Find("Church Bottle"));
        }

        if (worldMem.hasOars)
        {
            Destroy(GameObject.Find("Oars"));
            Destroy(GameObject.Find("Oars Found"));
        } 
    }
}
