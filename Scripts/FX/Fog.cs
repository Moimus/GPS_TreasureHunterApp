using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public enum states
    {
        idle = 0,
        closing = 1,
        clearing = 2
    }

    public static Fog instance;
    public int state = 0;
    public float clearSpeed = 1.0f;

    public GameObject fogLeft;
    public GameObject fogRight;
    public GameObject fogBottom;

    public Vector3 fogMarkerInitialRight;
    public Vector3 fogMarkerInitialLeft;
    public Vector3 fogMarkerInitialBottom;
    public Vector3 FogMarkerClearRight;
    public Vector3 FogMarkerClearLeft;
    public Vector3 FogMarkerClearBottom;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        lifeCycle();
    }

    void init()
    {
        instance = this;
        fogMarkerInitialRight = fogRight.transform.position;
        fogMarkerInitialLeft = fogLeft.transform.position;
        fogMarkerInitialBottom = fogBottom.transform.position;
        FogMarkerClearRight = new Vector3(fogMarkerInitialRight.x + 9f, fogMarkerInitialRight.y, fogMarkerInitialRight.z);
        FogMarkerClearLeft = new Vector3(fogMarkerInitialLeft.x - 9f, fogMarkerInitialLeft.y, fogMarkerInitialLeft.z);
        FogMarkerClearBottom = new Vector3(fogMarkerInitialBottom.x, fogMarkerInitialBottom.y, fogMarkerInitialBottom.z - 9f);
    }

    void lifeCycle()
    {
        if(state == (int)states.idle)
        {

        }
        else if (state == (int)states.closing)
        {
            closeFog();
        }
        else if (state == (int)states.clearing)
        {
            clearFog();
        }
    }

    void clearFog()
    {
        if(!Math.valueIsBetween(fogRight.transform.position.x, FogMarkerClearRight.x - 0.1f , FogMarkerClearRight.x + 0.1f))
        {
            fogRight.transform.Translate(Vector3.right * Time.deltaTime * clearSpeed);
        }

        if (!Math.valueIsBetween(fogLeft.transform.position.x, FogMarkerClearLeft.x - 0.1f, FogMarkerClearLeft.x + 0.1f))
        {
            fogLeft.transform.Translate(-Vector3.right * Time.deltaTime * clearSpeed);
        }

        if (!Math.valueIsBetween(fogBottom.transform.position.z, FogMarkerClearBottom.z - 0.1f, FogMarkerClearBottom.z + 0.1f))
        {
            fogBottom.transform.Translate(Vector3.up * Time.deltaTime * clearSpeed);
        }
    }

    void closeFog()
    {
        if (!Math.valueIsBetween(fogRight.transform.position.x, fogMarkerInitialRight.x - 0.1f, fogMarkerInitialRight.x + 0.1f))
        {
            fogRight.transform.Translate(-Vector3.right * Time.deltaTime * clearSpeed);
        }

        if (!Math.valueIsBetween(fogLeft.transform.position.x, fogMarkerInitialLeft.x - 0.1f, fogMarkerInitialLeft.x + 0.1f))
        {
            fogLeft.transform.Translate(Vector3.right * Time.deltaTime * clearSpeed);
        }

        if (!Math.valueIsBetween(fogBottom.transform.position.z, fogMarkerInitialBottom.z - 0.1f, fogMarkerInitialBottom.z + 0.1f))
        {
            fogBottom.transform.Translate(-Vector3.up * Time.deltaTime * clearSpeed);
        }
    }

}
