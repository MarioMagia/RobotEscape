using UnityEngine;

public class MovimientoObjeto : MonoBehaviour
{

	private float masaObjeto;
	public bool canPush;
	[Range(0.5f, 5f)] public float strength = 1.1f;


	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		if (canPush) PushRigidBodies(hit);
	}

	private void PushRigidBodies(ControllerColliderHit hit)	{
		
		Rigidbody body = hit.collider.attachedRigidbody;
		if (body == null || body.isKinematic)
		{
			return;
		}


		// We dont want to push objects below us
		if (hit.moveDirection.y < -0.3f)
		{
			return;
		}

		masaObjeto=body.mass;

		// Calculate push direction from move direction, horizontal motion only
		Vector3 pushDir = new Vector3(hit.moveDirection.x, 0.0f, hit.moveDirection.z);

		// Apply the push and take strength into account
		body.AddForce(pushDir * strength, ForceMode.Impulse);
	}
}