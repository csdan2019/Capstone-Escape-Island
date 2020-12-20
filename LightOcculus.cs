// The occulus light is the large, bright overhead light that beams down once the prism is placed in the holder
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOcculus : MonoBehaviour
{
    Light occLight;
    public bool prismPlaced = false;

    // Start is called before the first frame update
    void Start()
    {
        occLight = GetComponent<Light>();

        occLight.color = Color.white;
        // We want a tight beam of light
        occLight.innerSpotAngle = 2;
        occLight.spotAngle = 4;
        // Long range
        occLight.range = 200;

        // Starts off disabled until the prism is placed
        occLight.intensity = 0;
    }
   
    // Update is called once per frame
    void Update()
    {
		// If the prism is placed in the sconce, then we open the occulus
		if (prismPlaced) 
		{
            int success;
			success = turnOnOcculus();
		}
    }
    
    int turnOnOcculus()
    {
        // Turn on the occulus once the prism is placed in the holder
        occLight.intensity = 500;
        return 1;
    }
}
