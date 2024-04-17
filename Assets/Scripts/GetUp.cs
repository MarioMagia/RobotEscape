using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUp : StateMachineBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponentInParent<ThirdPersonController>().down = false;
    }
}
