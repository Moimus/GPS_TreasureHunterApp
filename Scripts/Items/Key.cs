using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : QuestItem
{
    public Animator animator;

    private void Start()
    {

    }

    public void openLock()
    {
        gameObject.SetActive(true);
        if(!animator.IsInTransition(0))
        {
            animator.Play("Open");
        }
    }

    public override void onUse()
    {
        if(relatedQuestTarget != null)
        {
            Player.instance.optainedKey = this;
            relatedQuestTarget.itemInstance = null;
            gameObject.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        onUse();
    }
}
