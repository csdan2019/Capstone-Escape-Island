using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caveSpear : MonoBehaviour
{
    GameObject spear;

    // Start is called before the first frame update
    void Start()
    {
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();

        // Check to see if the chest has been opened, if so we are skipping everythign
        if (worldMem.hasSpear)
        {
            spear = GameObject.Find("Spear");
            spear.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
