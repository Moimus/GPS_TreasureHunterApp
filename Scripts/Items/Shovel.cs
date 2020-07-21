using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour
{
    public Pile pile;
    public float digValue = 0.1f;
    public float digDelay = 1.0f;
    public bool digging = false;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void startDigging()
    {
        if(pile != null && !digging)
        {
            transform.position = pile.shovelMarker.transform.position;
            transform.rotation = pile.shovelMarker.transform.rotation;
            if (!animator.IsInTransition(0))
            {
                animator.Play("DigForward");
            }
            digging = true;
            StartCoroutine(digLoop());
        }
    }

    void stopDigging()
    {
        if(digging)
        {
            transform.position = Player.instance.shovelMarker.transform.position;
            transform.rotation = Player.instance.shovelMarker.transform.rotation;
            animator.Play("idle");
            digging = false;
        }
    }

    IEnumerator digLoop()
    {
        while(digging)
        {
            if (pile != null && pile.dirt > pile.destructionThreshold)
            {
                pile.reduceDirt(digValue);
                yield return new WaitForSeconds(digDelay);
            }
            else
            {
                stopDigging();
                pile = null;
                yield return null;
            }
        }
        stopDigging();
        pile = null;
        yield return null;
    }

    private void OnMouseDown()
    {
        startDigging();
    }
}
