using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAllPlatesScript : MonoBehaviour
{
    public List<GameObject> triggers;
    public Material unpressedMaterial;
    public Material pressedMaterial;
    public float countdownTimer;
    [SerializeField] public GameObject puerta;

    private bool countdownStarted = false;
    private bool completed = false;
    private float timeRemaining;
    private float triggersActivated = 0;
    // Start is called before the first frame update
    void Start()
    {
        unpressTriggers();
        timeRemaining = countdownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (completed) return;
        if (countdownStarted)
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                unpressTriggers();
                countdownStarted = false;
                timeRemaining = countdownTimer;
            }
        }
    }

    private void unpressTriggers()
    {
        triggersActivated = 0;
        foreach (GameObject trigger in triggers)
        {
            if (trigger != null)
            {
                trigger.GetComponent<Renderer>().material = unpressedMaterial;
            }
        }
    }

    public void triggerActivated(GameObject trigger)
    {
        if (completed) return;
        foreach (GameObject myTrigger in triggers)
        {
            if(myTrigger == trigger)
            {
                myTrigger.GetComponent<Renderer>().material = pressedMaterial;
                countdownStarted = true;
                triggersActivated++;
                checkComplete();
            }
        }
    }

    private void checkComplete()
    {
        if (triggersActivated == triggers.Count)
        {
            completed = true;
            puerta.GetComponent<DoorController>().Abrir();
        }
    }
}
