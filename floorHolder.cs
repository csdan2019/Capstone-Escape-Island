using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorHolder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Inside collision trigger, turning light on");
        GameObject Overlord = GameObject.Find("WorldManager");
        WorldManager worldMem = Overlord.GetComponent<WorldManager>();
        //worldMem.hasCrystal = true;
        if (worldMem.hasCrystal)
        {
            GameObject.Find("Light-Occulus").GetComponent<LightOcculus>().prismPlaced = true;
        }
        // LightOcculus.prismPlaced = true;
    }
}
