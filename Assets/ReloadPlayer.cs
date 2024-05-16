using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadPlayer : MonoBehaviour
{
    [SerializeField] ThirdPersonController controller;
    [SerializeField] ClientPlayerMove playerMove;
    // Start is called before the first frame update
    public void Reload()
    {
        controller.Restart();
        playerMove.Restart();
    }
}
