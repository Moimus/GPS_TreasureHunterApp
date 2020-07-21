using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public Vector2 startingLocation;
    public GameObject questTargetPrefab;
    public GameObject pilePrefab;
    public GameObject keyPrefab;
    public GameObject chestPrefab;

    public List<QuestTarget> questTargets;
    public bool completed = false;
    public float minTargetDistance = 10f;
    public float updateDelay = 2f;

    public Key keyInstance;
    public Chest chestInstance;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(init()); //TODO
        StartCoroutine(lifeCycle());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator init()
    {
        while(GeoLocator.instance == null)
        {
            Debug.Log("waiting for GeoLocator...");
            yield return null;
        }
        while (GeoLocator.instance.tracking == false)
        {
            Debug.Log("initializing GeoLocator...");
            yield return null;
        }
        startingLocation = GeoLocator.instance.deviceLocation;
        Player.instance.currentQuest = this;
        generateTargets(3, 100f); //TODO
        hideKey();
        hideChest();
        yield return null;
    }

    IEnumerator lifeCycle()
    {
        while(!completed)
        {
            if(GeoLocator.instance != null && GeoLocator.instance.tracking)
            {
                updateDistances();
                checkNearestTargetDistance();
            }
            yield return new WaitForSeconds(updateDelay);
        }
        yield return null;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="num"></param>
    /// <param name="distance"></param>
    void generateTargets(int num, float distance)
    {
        //List<Vector2> mockTargets = MockProvider.getMockTargets(); //TODO

        for (int n = 0; n < num; n++)
        {
            GameObject questTargetInstance = Instantiate(questTargetPrefab, transform);
            QuestTarget targetComponent = questTargetInstance.GetComponent<QuestTarget>();
            targetComponent.init(GeoLocator.instance.getRandomCoordinate(GeoLocator.instance.deviceLocation, distance), this);
            questTargets.Add(targetComponent);
        }
    }

    void hideKey()
    {
        GameObject keyInstance = Instantiate(keyPrefab, transform);
        Key keyComponent = keyInstance.GetComponent<Key>();
        this.keyInstance = keyComponent;
        int randomRoll = Random.Range(0, questTargets.Count);
        keyComponent.relatedQuestTarget = questTargets[randomRoll];
        while (questTargets[randomRoll].itemInstance != null)
        {
            randomRoll = Random.Range(0, questTargets.Count);
        }
        questTargets[randomRoll].itemInstance = keyComponent;
        keyInstance.SetActive(false);
    }

    void hideChest()
    {
        GameObject chestInstance = Instantiate(chestPrefab, transform);
        Chest chestComponent = chestInstance.GetComponent<Chest>();
        this.chestInstance = chestComponent;
        this.chestInstance.relatedKey = keyInstance;
        int randomRoll = Random.Range(0, questTargets.Count);
        chestComponent.relatedQuestTarget = questTargets[randomRoll];
        while (questTargets[randomRoll].itemInstance != null)
        {
            randomRoll = Random.Range(0, questTargets.Count);
        }
        questTargets[randomRoll].itemInstance = chestComponent;
        chestInstance.SetActive(false);
    }

    void updateDistances()
    {
        foreach(QuestTarget t in questTargets)
        {
            t.updateDistance();
        }
    }

    void checkNearestTargetDistance()
    {
        if(questTargets.Count > 0)
        {
            QuestTarget nearestTarget = getNearestTarget();
            if (nearestTarget.distanceToDevice < minTargetDistance && nearestTarget.distanceToDevice != -1)
            {
                Debug.Log("target in range!");
                if (nearestTarget.completed == false || (nearestTarget.completed && nearestTarget.itemInstance != null))
                {
                    Fog.instance.state = (int)Fog.states.clearing;
                }
                nearestTarget.onInRange();
            }
            else if (nearestTarget.distanceToDevice > minTargetDistance)
            {
                Fog.instance.state = (int)Fog.states.closing;
                Debug.Log("target out of range!");
                nearestTarget.onOutOfRange();
            }
        }
    }

    public QuestTarget getNearestTarget()
    {
        QuestTarget result;
        List<QuestTarget> sortedList = questTargets.OrderBy(o => o.distanceToDevice).ToList<QuestTarget>();
        result = sortedList[0];
        return result;
    }

    public QuestTarget getNearestUnclompetedTarget()
    {
        QuestTarget result = null;
        List<QuestTarget> sortedList = questTargets.OrderBy(o => o.distanceToDevice).ToList<QuestTarget>();
        foreach(QuestTarget qt in sortedList)
        {
            if(qt.completed == false)
            {
                result = qt;
                break;
            }
        }

        return result;
    }

    public void endQuest()
    {

    }
}
