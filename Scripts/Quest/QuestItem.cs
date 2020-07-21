using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestItem : MonoBehaviour
{
    public QuestTarget relatedQuestTarget;

    public virtual void onUse()
    {

    }
}
