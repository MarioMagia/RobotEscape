using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DetectMarks : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject TextPrefab;
    private GameObject nuevo;
    private Teleport tp;
    private StarterAssetsInputs inputs;
    private GameOptions options;

    private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Teleport") && options.TakeTPs)
            {
            if (!nuevo)
            {
                nuevo = Instantiate(TextPrefab, new Vector3(other.gameObject.transform.position.x, other.gameObject.transform.position.y + 1.5f, other.gameObject.transform.position.z), playerPrefab.transform.rotation);
            }
            if (inputs.takemark){
                    inputs.takemark = false;
                    Marca marca = tp.GetMarca(other.gameObject.transform.position);
                    tp.TakeMark(marca);
                }
            }
        }
    private void OnTriggerExit(Collider other)
    {
            Destroy(nuevo);

    }
    public void Awake()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        tp = GetComponent<Teleport>();
        options = GetComponent<GameOptions>();
    }

}
