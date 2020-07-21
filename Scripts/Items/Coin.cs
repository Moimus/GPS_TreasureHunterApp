using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : QuestItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void onUse()
    {
        collectCoin();
    }

    void collectCoin()
    {
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        onUse();
    }
}
