using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caveChest : MonoBehaviour
{
    public bool chestHit = false;
    bool done = false;
    ParticleSystem chestFire; 
    // For the timer, the flames will only last 20 secs
//    float initTime = 0.0F;        // Initialization time
    public float durTime = 10.0F;        // Duration of the flames
//    float intTime = 0.0F;         // The time interval (check if less than durTime)
    GameObject chest;
    BoxCollider spear;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();

        // Check to see if the chest has been opened, if so we are skipping everythign
        if (!worldMem.hasSpear)
        {
            chestFire = GetComponent<ParticleSystem>();
            var emit = chestFire.emission;
            emit.enabled = false;
            chestFire.Stop();
        }
        else
        {
            chest = GameObject.Find("Chest");
            chest.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();
        if (!worldMem.hasSpear)
        {
            if (chestHit && !done)
            {
                chestFire = GetComponent<ParticleSystem>();
                var emit = chestFire.emission;
                chestFire.Play();
                emit.enabled = true;
                if (Time.time >= durTime)
                {
                    emit.enabled = false;
                    chestFire.Stop();
                    chest = GameObject.Find("Chest");
                    chest.SetActive(false);
                    done = true;
                    spear = GameObject.Find("Spear").GetComponent<BoxCollider>();
                    spear.enabled = true;
                }
            }
        }
    }
}
