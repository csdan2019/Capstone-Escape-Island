using System.Collections;
using UnityEngine;

public class Mirror01 : MonoBehaviour
{
    Light mirrorLight;
    LineRenderer lightLine;
    bool done = false;

    // Start is called before the first frame update
    void Start()
    {
        mirrorLight = GetComponent<Light>();
        mirrorLight.intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!done && GameObject.Find("Light-Occulus").GetComponent<LightOcculus>().prismPlaced)
        {
            mirrorLight.intensity = 300;
            lightLine = GetComponent<LineRenderer>();
            lightLine.enabled = true;

            Vector3 lightSource = transform.position;
            Vector3 lightEnd = lightSource + (transform.forward * 10000);

            //Debug.Log("lightSource = " + lightSource.ToString("F4"));
            //Debug.Log("lightEnd = " + lightEnd.ToString("F4"));

            RaycastHit hit;

            lightLine.SetPosition(0, lightSource);
            Debug.DrawRay(lightSource, transform.forward * 1000, Color.red);
            if (Physics.Raycast(lightSource, transform.forward, out hit, 3000))
            {
                lightEnd = hit.point;
                Debug.Log("Hit " + hit.transform.name);
                if (hit.transform.name == "mirror02")
                {
                    GameObject.Find("mirror02").GetComponent<Mirror02>().mirror02Hit = true;
                    GameObject.Find("mirror02").GetComponent<Mirror02>().srcPos = lightSource;
                    GameObject.Find("mirror02").GetComponent<Mirror02>().pointHit = hit.point;
                    GameObject.Find("mirror02").GetComponent<Mirror02>().pointNorm = hit.normal;
                    done = true;
                }

                //hit.transform.SendMessage("mirrorHit", transform.position);
            }
            lightLine.SetPosition(1, lightEnd);
        }
    }
}
