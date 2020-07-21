using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile : QuestItem
{
    public float dirt = 1.0f;
    public float destructionThreshold = 0.5f;
    public Transform shovelMarker;

    public void reduceDirt(float amount)
    {
        dirt -= amount;
        if(dirt < destructionThreshold)
        {
            if(relatedQuestTarget.itemInstance != null)
            {
                relatedQuestTarget.showReward();
            }
            Player.instance.shovel.pile = null;
            relatedQuestTarget.completed = true;
            Destroy(gameObject);
        }
    }

    public void reduceScale()
    {
        //TODO reduce scale while digging
    }

    public override void onUse()
    {
        //TODO add information popUp onClick
    }
}
