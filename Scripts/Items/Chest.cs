using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : QuestItem
{
    public Key relatedKey;
    public bool opened = false;
    public QuestItem loot;

    Animator animator;
    public Transform keyMarker;
    public Transform lootSpawnMarker;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void onUse()
    {
        StartCoroutine(open());
    }

    public IEnumerator open()
    {
        if(Player.instance.optainedKey != null && Player.instance.optainedKey == relatedKey && opened == false)
        {
            relatedKey.transform.position = keyMarker.position;
            relatedKey.transform.rotation = keyMarker.rotation;
            relatedKey.openLock();
            yield return new WaitForSeconds(0.8f);
            if (!animator.IsInTransition(0))
            {
                animator.Play("Open");
            }
            Debug.Log("opening chest...");
            yield return new WaitForSeconds(0.8f);
            spawnLoot();
            opened = true;
        }

        yield return null;
    }

    private void OnMouseDown()
    {
        onUse();
    }

    void spawnLoot()
    {
        GameObject lootInstance = Instantiate(loot.gameObject);
        lootInstance.transform.position = lootSpawnMarker.position;
        lootInstance.transform.rotation = lootSpawnMarker.rotation;
        QuestItem lootComponent = lootInstance.GetComponent<QuestItem>();
        lootComponent.relatedQuestTarget = relatedQuestTarget;
        lootComponent.relatedQuestTarget.relatedQuest = relatedQuestTarget.relatedQuest;
    }

}
