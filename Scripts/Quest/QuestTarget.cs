using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTarget : MonoBehaviour
{
    public Quest relatedQuest;
    public Vector2 location;
    public bool completed = false;
    /// <summary>
    /// distance of the target to the users device. -1 is the default value, means distance hasn't been calculated yet
    /// </summary>
    public float distanceToDevice = -1f;
    public Pile pileInstance;
    public QuestItem itemInstance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(Vector2 location, Quest relatedQuest)
    {
        this.relatedQuest = relatedQuest;
        this.location = location;
        GameObject newPileInstance = Instantiate(relatedQuest.pilePrefab, transform);
        this.pileInstance = newPileInstance.GetComponent<Pile>();
        pileInstance.relatedQuestTarget = this;
    }

    public void updateDistance()
    {
        distanceToDevice = GeoLocator.instance.calcLatLongDistance(GeoLocator.instance.deviceLocation, location);
    }

    public void onInRange()
    {
        Player.instance.currentTarget = this;
        Player.instance.shovel.pile = pileInstance;
        if(!completed)
        {
            showPile();
        }
        else
        {
            showReward();
        }
    }

    public void onOutOfRange()
    {
        Player.instance.currentTarget = null;
        Player.instance.shovel.pile = null;
        if (!completed)
        {
            hidePile();   
        }
        else
        {
            hideReward();
        }
    }

    public void showPile()
    {
        if(pileInstance != null && !pileInstance.gameObject.activeInHierarchy)
        {
            pileInstance.transform.position = Player.instance.objectSpawnMarker.transform.position;
            pileInstance.transform.rotation = Player.instance.objectSpawnMarker.transform.rotation;
            pileInstance.gameObject.SetActive(true);
        }
    }

    public void hidePile()
    {
        if (pileInstance != null)
        {
            pileInstance.gameObject.SetActive(false);
        }
    }

    public void showReward()
    {
        if(itemInstance != null && !itemInstance.gameObject.activeInHierarchy)
        {
            itemInstance.transform.position = Player.instance.objectSpawnMarker.transform.position;
            itemInstance.transform.rotation = Player.instance.objectSpawnMarker.transform.rotation;
            itemInstance.gameObject.SetActive(true);
        }
    }

    public void hideReward()
    {
        if (itemInstance != null)
        {
            itemInstance.gameObject.SetActive(false);
        }
    }


}
