using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PressAllPlatesScript : NetworkBehaviour
{
    public List<GameObject> triggers;
    private Dictionary<GameObject, bool> myTriggers;
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
        myTriggers = new Dictionary<GameObject, bool>();
        foreach (GameObject trigger in triggers)
        {
            myTriggers.Add(trigger, false);
        }
        unpressTriggers();
        timeRemaining = countdownTimer;
    }

    // Update is called once per frame
    void Update()
    {
        if (completed) return;
        if (countdownStarted)
        {
            if (timeRemaining > 0)
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
                myTriggers[trigger] = false;
            }
        }
    }

    public void triggerActivated(GameObject trigger)
    {
        if (completed) return;
        foreach (GameObject myTrigger in triggers)
        {
            if (myTrigger == trigger)
            {
                trigger.GetComponent<Renderer>().material = pressedMaterial;
                countdownStarted = true;
                if (!myTriggers[myTrigger])
                {
                    myTriggers[myTrigger] = true;
                    triggersActivated++;
                    checkComplete();
                }
            }
        }
    }

    private void checkComplete()
    {
        if (triggersActivated == triggers.Count)
        {
            StartCoroutine(OpenDoor());
        }
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void OpenDoorRpc()
    {
        puerta.GetComponent<DoorController>().Abrir();
    }

    IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(0.5f);
        IceBehaviour ice = FindObjectOfType<IceBehaviour>();
        if (ice.plane.activeSelf)
        {
            completed = true;
            OpenDoorRpc();
        }
        
    }
}
