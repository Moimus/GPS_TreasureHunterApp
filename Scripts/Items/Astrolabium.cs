using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astrolabium : MonoBehaviour
{
    Animator animator;
    public bool pulsing = true;
    public float pulseInterval = 1.0f;
    public float animationSpeed = 1.0f;
    public ParticleSystem pulseSystem;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(pulseCycle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator pulseCycle()
    {
        while(pulsing)
        {
            if (GeoLocator.instance != null && Player.instance != null)
            {
                calcPulseInterval();
                calcAnimationSpeed();
                if (pulseInterval > 0.1f && pulseInterval < 10f)
                {
                    pulseSystem.Play();
                }
                if (pulseInterval < 10f)
                {
                    yield return new WaitForSeconds(pulseInterval);
                }
                else
                {
                    yield return new WaitForSeconds(GeoLocator.instance.updateDelay);
                }
            }
            else
            {
                yield return null;
            }
        }
        yield return null;
    }

    void calcPulseInterval()
    {
        if (Player.instance.currentQuest != null)
        {
            QuestTarget nearestTarget = Player.instance.currentQuest.getNearestTarget();

            float distanceToNearestTarget = GeoLocator.instance.calcLatLongDistance(nearestTarget.location, GeoLocator.instance.deviceLocation);
            //Debug.Log("distanceToNearest: " + distanceToNearestTarget);
            if (distanceToNearestTarget > 100f)
            {
                pulseInterval = 10f;
            }
            else if (distanceToNearestTarget < 100f && distanceToNearestTarget > 10f)
            {
                pulseInterval = (distanceToNearestTarget * 0.01f) * 2;
            }
            else
            {
                pulseInterval = 0.1f;
            }
        }
    }

    void calcAnimationSpeed()
    {
        if (Player.instance.currentQuest != null)
        {
            QuestTarget nearestTarget = Player.instance.currentQuest.getNearestTarget();
            if (!nearestTarget.completed)
            {
                float distanceToNearestTarget = GeoLocator.instance.calcLatLongDistance(nearestTarget.location, GeoLocator.instance.deviceLocation);
                float newAnimationSpeed = 5 - distanceToNearestTarget * 0.1f;
                if (newAnimationSpeed < 0)
                {
                    newAnimationSpeed = 0;
                }
                animationSpeed = newAnimationSpeed * 2f;
            }
            else
            {
                animationSpeed = 0;
            }

            animator.speed = animationSpeed;
        }
    }
}
