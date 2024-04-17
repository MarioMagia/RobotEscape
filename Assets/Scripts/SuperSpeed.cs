using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class SuperSpeed : MonoBehaviour
{
    public CharacterController characterController;
    public float movimientoNormal = 2.0f;
    public float superVelocidad = 10f;

    private Animator _animator;


    void Start()
    {
        _animator = GetComponent<Animator>(); // Obtén la referencia al Animator
    }


    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.qKey.isPressed)
        {
            // Si se presiona la tecla Q, establecer la velocidad aumentada
            characterController.Move(transform.forward * superVelocidad * Time.deltaTime);

            // Actualiza la variable de velocidad en el Animator
            if (_animator != null)
            {
                float velocidadAnimacion = superVelocidad / movimientoNormal; // Ajusta según tus necesidades
                _animator.SetFloat(Animator.StringToHash("Speed"), velocidadAnimacion);
            }
        }
    }
}
        
    
