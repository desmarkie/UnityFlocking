using UnityEngine;
using System.Collections.Generic;

public class FlockingCameraController : MonoBehaviour
{
	public const int STATIC = 0;
	public const int WATCH_FROM_CENTER = 1;
	public const int BOID_FOLLOW = 2;
	public const int TARGET_FOLLOW = 3;
	public const int USER_CONTROL = 4;

	private Vector3 staticCameraPosition;
	private Quaternion staticCameraRotation;

	private int cameraMode = 0;

	private Vector3 cameraVelocity = Vector3.zero;
	private float cameraRotationX = 0f;
	private float cameraRotationY = 0f;

	private float maxCameraSpeed = 10f;

	private Boid targetBoid;

	private GameObject targetObject;

	public int GetCameraMode ()
	{
		return cameraMode;
	}

	public void SetCameraMode ( int cameraMode )
	{
		this.cameraMode = cameraMode;
		cameraVelocity = Vector3.zero;
		if ( cameraMode == FlockingCameraController.BOID_FOLLOW ) Camera.main.fieldOfView = 90f;
		else Camera.main.fieldOfView = 65f;
	}

	public void SetTargetBoid ( Boid boid )
	{
		targetBoid = boid;
	}

	public void SetTargetObject ( GameObject targetObject )
	{
		this.targetObject = targetObject;
	}

	void Start ()
	{
		staticCameraPosition = Camera.main.transform.position;
		staticCameraRotation = Camera.main.transform.rotation;
	}

	void Update ()
	{
		switch ( cameraMode )
		{
			case FlockingCameraController.STATIC :
				// camera returns to start position
				if ( Camera.main.transform.position != staticCameraPosition )
				{
					Camera.main.transform.position = staticCameraPosition;
					Camera.main.transform.rotation = staticCameraRotation;
				}
				break;

			case FlockingCameraController.BOID_FOLLOW :
				// camera look at target from POV of boid
				if ( targetBoid )
				{
					Camera.main.transform.position = (targetBoid.transform.position - (targetBoid.transform.forward * 6f)) + (targetBoid.transform.up * 10f);
					Camera.main.transform.rotation = targetBoid.transform.rotation;
				}
				break;

			case FlockingCameraController.TARGET_FOLLOW :
				// camera look at boid from POV of target
				if ( targetObject )
				{
					Camera.main.transform.position = targetObject.transform.position;
					Camera.main.transform.rotation = Quaternion.LookRotation( targetBoid.transform.position, Vector3.up );
				}
				break;

			case FlockingCameraController.WATCH_FROM_CENTER :
				// camera look at target form center
				if ( targetObject )
				{
					if ( Camera.main.transform.position != Vector3.zero ) Camera.main.transform.position = Vector3.zero;
					Camera.main.transform.rotation = Quaternion.LookRotation( targetObject.transform.position - Camera.main.transform.position, Vector3.up );
				}
				break;

			case FlockingCameraController.USER_CONTROL :
				CheckUserInput();
				cameraVelocity *= 0.99f;
				if ( cameraVelocity.magnitude < 0.1 ) cameraVelocity = Vector3.zero;
				cameraRotationY *= 0.99f;
				cameraRotationX *= 0.99f;
				if ( Mathf.Abs( cameraRotationY ) < 0.1 ) cameraRotationY = 0f;
				if ( Mathf.Abs( cameraRotationX ) < 0.1 ) cameraRotationX = 0f;
				break;
		}


	}

	private void CheckUserInput ()
	{
		float moveSpeed = 7f;
		float rotateSpeed = 7f;

		Vector3 pos = Camera.main.transform.localPosition;
		Vector3 acc = Vector3.zero;
		if ( Input.GetKey( KeyCode.W ) )
			acc += Camera.main.transform.forward * Time.deltaTime * moveSpeed;
		if ( Input.GetKey( KeyCode.S ) )
			acc -= Camera.main.transform.forward * Time.deltaTime * moveSpeed;
		if ( Input.GetKey( KeyCode.A ) )
			acc -= Camera.main.transform.right * Time.deltaTime * moveSpeed;
		if ( Input.GetKey( KeyCode.D ) )
			acc += Camera.main.transform.right * Time.deltaTime * moveSpeed;

		//if ( Input.GetKey( KeyCode.DownArrow ) )
		//	acc += Vector3.back * 0.3f;

		cameraVelocity = Vector3.ClampMagnitude(cameraVelocity + acc, maxCameraSpeed );
		pos += cameraVelocity;
		Camera.main.transform.localPosition = pos;

		if ( Input.GetKey( KeyCode.UpArrow ) )
			cameraRotationX -= rotateSpeed * Time.deltaTime;

		if ( Input.GetKey( KeyCode.DownArrow ) )
			cameraRotationX += rotateSpeed * Time.deltaTime;

		if ( Input.GetKey( KeyCode.LeftArrow ) )
			cameraRotationY -= rotateSpeed * Time.deltaTime;
		
		if ( Input.GetKey( KeyCode.RightArrow ) )
			cameraRotationY += rotateSpeed * Time.deltaTime;

		Camera.main.transform.RotateAround( Camera.main.transform.position, Camera.main.transform.up, cameraRotationY );
		Camera.main.transform.RotateAround( Camera.main.transform.position, Camera.main.transform.right, cameraRotationX );
		//Vector3 rot = Camera.main.transform.rotation.eulerAngles;
		//rot.x += cameraRotationX;
		//rot.y += cameraRotationY;
		//Camera.main.transform.rotation = Quaternion.Euler( rot );
	}
}