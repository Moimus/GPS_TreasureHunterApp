using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoLocator : MonoBehaviour
{
    public static GeoLocator instance;
    public bool tracking = false;
    public float updateDelay = 2f;
    public Vector2 deviceLocation;

    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init()
    {
        instance = this;
        StartCoroutine(startGeoService());
    }

    public IEnumerator startGeoService()
    {
        Debug.Log("initializing GeoLocationService...");

        Input.location.Start();

        int maximumWaitTime = 20;
        int trials = 1;
        while (Input.location.status == LocationServiceStatus.Initializing)
        {
            Debug.Log("GeoLocationService Initializing, tried " + trials + " times");
            yield return new WaitForSeconds(1);
            maximumWaitTime--;
        }

        if (maximumWaitTime < 1)
        {
            Debug.Log("GeoLocationService Timeout!");
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.Log("GeoLocationService Init failed!");
        }
        else
        {
            deviceLocation = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            tracking = true;
            Debug.Log("GeoLocation service initialized!");
            //Debug.Log("Location: " + Input.location.lastData.latitude + "\n " + Input.location.lastData.longitude + "\n " + Input.location.lastData.altitude + "\n " + Input.location.lastData.horizontalAccuracy + "\n " + Input.location.lastData.timestamp);
        }
        StartCoroutine(updatePostion());
        yield return null;
    }

    public IEnumerator updatePostion()
    {
        while(tracking)
        {
            locationDebugCycle();
            Debug.Log("GeoLocator: tracking...");
            deviceLocation = new Vector2(Input.location.lastData.latitude, Input.location.lastData.longitude);
            yield return new WaitForSeconds(updateDelay);
        }
        yield return null;
    }

    public float calcLatLongDistance(Vector2 LatLongA, Vector2 LatLongB)
    {
        int r = 6371 * 1000;
        float omega1 = LatLongA.x * Mathf.PI / 180;
        float omega2 = LatLongB.y * Mathf.PI / 180;
        float deltaA = (LatLongB.x - LatLongA.x) * Mathf.PI / 180;
        float deltaB = (LatLongB.y - LatLongA.y) * Mathf.PI / 180;

        float a = Mathf.Sin(deltaA / 2) * Mathf.Sin(deltaA / 2) + Mathf.Cos(omega1) * Mathf.Cos(omega1) * Mathf.Sin(deltaB / 2) * Mathf.Sin(deltaB / 2);
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        float d = r * c;
        return d;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="initialCoord">starting coordinate (LatLong)</param>
    /// <param name="maxDistance">maximum distance from initialCoord in metres</param>
    /// <returns></returns>
    public Vector2 getRandomCoordinate(Vector2 initialCoord, float maxDistance)
    {
        float rollDirectionLat = Random.Range(-maxDistance, maxDistance);
        float rollDirectionLong = Random.Range(-maxDistance, maxDistance);
        float lat = rollDirectionLat * 0.00001f;
        float longi = rollDirectionLong * 0.00001f;
        Vector2 result = new Vector2(initialCoord.x + lat, initialCoord.y + longi);
        return result;
    }

    void locationDebugCycle()
    {
        try
        {
            DebugUI.instance.Clear();
            DebugUI.instance.Log("CurrentLocation: " + deviceLocation.x.ToString() + " / " + deviceLocation.y.ToString());

            foreach (QuestTarget qt in Player.instance.currentQuest.questTargets)
            {
                DebugUI.instance.Log("Target @" + qt.location.x.ToString() + "/" + qt.location.y.ToString());
            }
            Vector2 nearestTargetLoc = Player.instance.currentQuest.getNearestUnclompetedTarget().location;
            DebugUI.instance.Log("DistanceToNearest: " + GeoLocator.instance.calcLatLongDistance(nearestTargetLoc, deviceLocation) + "m");
        }
        catch(System.Exception e)
        {
            if(DebugUI.instance != null)
            {
                DebugUI.instance.Log(e.Message);
            }
        }
    }
}
