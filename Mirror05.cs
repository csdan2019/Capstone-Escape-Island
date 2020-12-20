using System.Collections;
using UnityEngine;

public class Mirror05 : MonoBehaviour
{
    Light mirrorLight;
    LineRenderer lightLine;
    public bool mirror05Hit = false;
    public Vector3 srcPos;
    public Vector3 pointHit;
    public Vector3 pointNorm;
    bool done = false;
    Vector3 lightSource;
    Vector3 lightEnd;
    Vector3 lightDir;
    bool enableRotate = false;
    bool finished = false;


    // Start is called before the first frame update
    void Start()
    {
        mirrorLight = GetComponent<Light>();
        mirrorLight.intensity = 0;
        lightSource = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!finished)
        {
            lightLine = GetComponent<LineRenderer>();
            if (mirror05Hit && !done)
            {
                BoxCollider boxCol;
                boxCol = GetComponent<BoxCollider>();
                boxCol.size = new Vector3(5.645847f, 7.273544f, 16.91642f);
                done = true;
                mirrorLight.intensity = 300;

                lightLine.enabled = true;

                done = true;
            }

            if (done)
            {
                // Rotate the mirror!
                // q = left by 2deg
                // r = right by 2deg
                // t = up by 2deg
                // g = down by 2deg
                if (enableRotate)
                {
                    if (Input.GetKeyDown(KeyCode.Q))
                    {
                        // Left
                        //transform.RotateAround(transform.position, Vector3.down, 5);
                        transform.Rotate(0, 2, 0);
                        //Debug.Log("Q pressed");
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // Right
                        //transform.RotateAround(transform.position, Vector3.up, 5);
                        transform.Rotate(0, -2, 0);
                        //Debug.Log("E pressed");
                    }
                    if (Input.GetKeyDown(KeyCode.T))
                    {
                        // Up
                        transform.Rotate(2, 0, 0);
                        //Debug.Log("T pressed");
                    }
                    if (Input.GetKeyDown(KeyCode.G))
                    {
                        // Down
                        transform.Rotate(-2, 0, 0);
                        //Debug.Log("G pressed");
                    }
                }

                lightDir = transform.forward;
                Vector3 lightEnd = lightSource + (lightDir * 10000);
                RaycastHit hit;

                lightLine.SetPosition(0, lightSource);
                Debug.DrawRay(lightSource, lightDir * 1000, Color.red);
                if (Physics.Raycast(lightSource, lightDir, out hit, 3000))
                {
                    lightEnd = hit.point;
                    if (hit.transform.name == "Chest")
                    {
                        // Start the animation and set the end time based on current time and the duration
                        GameObject.Find("Chest").GetComponent<caveChest>().chestHit = true;
                        GameObject.Find("Chest").GetComponent<caveChest>().durTime = Time.time + GameObject.Find("Chest").GetComponent<caveChest>().durTime;
                        Vector3 tarPos = GameObject.Find("Chest").transform.position;
                        lightEnd = tarPos;
                        transform.LookAt(tarPos);
                        finished = true;
                    }

                    lightLine.SetPosition(1, lightEnd);

                }
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        enableRotate = true;
        //Debug.Log("Inside Trigger");
    }

    private void OnTriggerExit(Collider col)
    {
        enableRotate = false;
        //Debug.Log("Leaving Trigger");
    }
}

