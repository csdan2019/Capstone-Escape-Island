using System.Collections;
using UnityEngine;

public class Mirror04 : MonoBehaviour
{
    Light mirrorLight;
    LineRenderer lightLine;
    public bool mirror04Hit = false;
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
            if (mirror04Hit && !done)
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
                        transform.Rotate(0, 2, 0);
                        //Debug.Log("Q pressed");
                    }
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        // Right
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
                    //Debug.Log("Hit " + hit.transform.name);
                    lightEnd = hit.point;
                    if (hit.transform.name == "mirror05")
                    {
                        GameObject.Find("mirror05").GetComponent<Mirror05>().mirror05Hit = true;
                        finished = true;
                        mirrorLight = GetComponent<Light>();
                        mirrorLight.intensity = 0;
                        mirrorLight.enabled = false;
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

