using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour {

	private Vector3 _velocity = Vector3.zero;
	public Vector3 velocity
	{
		get { return _velocity; }
	}

	private bool _isAlive = false;
	public bool isAlive
	{
		get { return _isAlive; }
		set { 
			_isAlive = value;
			TrailRenderer trail = GetComponentInChildren<TrailRenderer>();
			if ( trail != null ) trail.time = 0.01f;
			gameObject.SetActive( value );
		}
	}

	private float _arriveDistance = 9f;

	private Vector3 acceleration = Vector3.zero;

	private GameObject target;

	private AppController app;

	private float boidMass = 10f;

	private List<Boid> visibleNeighbours = new List<Boid>();

	/* PUBLIC */

	public void InitialiseBoid( AppController app, float mass, float visionSize )
	{
		this.app = app;
		boidMass = mass;
		transform.localScale = new Vector3( 1 + boidMass / 10, 1 + boidMass / 10, 1 + boidMass / 10 );
		GetComponent<BoxCollider>().size = new Vector3( visionSize, visionSize, visionSize );
		isAlive = true;

		TrailRenderer trail = GetComponentInChildren<TrailRenderer>();
		if ( trail != null ) trail.time = 9.1f;
	}


	public void ShowTrail ( bool show )
	{
		GetComponent<TrailRenderer>().enabled = show;
	}

	public void ShowMesh ( bool show )
	{
		MeshRenderer[] meshes = GetComponentsInChildren< MeshRenderer > ();
		for ( int i = 0; i < meshes.Length; i++ )
		{
			meshes[ i ].enabled = show;
		}
	}

	void OnTriggerEnter ( Collider obj )
	{
		//Debug.Log( "COLLIDE : " + this.name );
		Boid bObj = obj.GetComponentInParent<Boid>();
		if ( visibleNeighbours.IndexOf( bObj ) == -1 && bObj != this ) visibleNeighbours.Add( bObj );
		//if ( bObj == this ) Debug.Log( "NOT ADDING" );
	}

	void OnTriggerExit ( Collider obj )
	{
		Boid bObj = obj.GetComponentInParent<Boid>();
		int index = visibleNeighbours.IndexOf( bObj );
		if ( index != -1 ) visibleNeighbours.RemoveAt( index );
		//if ( this.name == "Boid_1" ) Debug.Log( "EXIT:: " + visibleNeighbours.Count );
	}

	/* MONOBEHAVIOUR */

	void Update () {

		if ( !isAlive ) return;

		List<Vector3> forces = new List<Vector3>();

		forces.Add( SeekTarget( app.targetPosition ) );
		if ( visibleNeighbours.Count > 0 ) forces.Add( Flock() );
		//forces.Add( Wobble() );

		acceleration = CollectForces( forces );
		_velocity = Vector3.ClampMagnitude( _velocity + acceleration, app.maximumSpeed );


		UpdateTransform();
	}


	/* PRIVATE */

	private void UpdateTransform ()
	{
		// Update position
		Vector3 position = transform.position;
		position += _velocity;
		transform.position = position;

		// Update rotation
		if ( _velocity.magnitude > 0 )
		{
			transform.rotation = Quaternion.LookRotation( _velocity );
		}
	}

	// add up the various steering forces and divide by mass
	private Vector3 CollectForces ( List<Vector3> forces )
	{
		Vector3 vec = Vector3.zero;

		for ( var i = 0; i < forces.Count; i++ )
		{
			vec += forces[ i ];
		}

		return vec / boidMass;
	}


	/* BEHAVIOURS */

	private Vector3 Wobble ()
	{
		float wobbleAmount = 10f;
		return new Vector3(
			Random.Range( -wobbleAmount, wobbleAmount ),
			Random.Range( -wobbleAmount, wobbleAmount ),
			Random.Range( -wobbleAmount, wobbleAmount )
		);
	}

	// flock behaviours, based on Reynolds' boids
	private Vector3 Flock ()
	{
		Vector3 flockForce = Vector3.zero;
		flockForce += SeparateFromNeighbours();
		flockForce += AlignToNeighbours();
		flockForce += CohereToNeighbours();
		return flockForce;
	}

	// steer toward a target in 3d space
	private Vector3 SeekTarget ( Vector3 targetPosition )
	{
		Vector3 desiredVelocity = targetPosition - transform.position;

		desiredVelocity = desiredVelocity.normalized * app.maximumSpeed;

		return Vector3.ClampMagnitude( desiredVelocity - _velocity, app.maximumTurnSpeed );
	}

	// steer toward a target and decelerate when within distance [_arriveDistance]
	private Vector3 ArriveAtTarget ( Vector3 targetPosition )
	{
		Vector3 desiredVelocity = targetPosition - transform.position;
		float distance = desiredVelocity.sqrMagnitude;

		if ( distance < _arriveDistance )
		{
			desiredVelocity = desiredVelocity.normalized * Mathf.Lerp( 0, app.maximumSpeed, Mathf.InverseLerp( 0, 3, distance ) );
		}
		else
		{
			desiredVelocity = desiredVelocity.normalized * app.maximumSpeed;
		}

		return Vector3.ClampMagnitude( desiredVelocity - _velocity, app.maximumTurnSpeed );
	}

	// steer away from any neighbours within distance [app.separation]
	private Vector3 SeparateFromNeighbours ()
	{
		Vector3 combinedVelocities = Vector3.zero;
		int ct = 0;
		float distance;

		for ( int i = 0; i < visibleNeighbours.Count; i++ )
		{
			//distance = Vector3.SqrMagnitude( transform.position - neighbours[ i ].gameObject.transform.position );
			distance = Vector3.SqrMagnitude( transform.position - visibleNeighbours[ i ].transform.position );
			if ( distance < ( app.separation * app.separation ) )
			{
				combinedVelocities += Vector3.Normalize( transform.position - visibleNeighbours[ i ].gameObject.transform.position ) / distance;
				ct++;
			}
		}

		if ( ct > 0 )
		{
			combinedVelocities = Vector3.Normalize( combinedVelocities / ct ) * app.maximumSpeed;
			combinedVelocities = Vector3.ClampMagnitude( combinedVelocities - _velocity, app.maximumTurnSpeed );
		}

		return combinedVelocities;
	}

	// try to match neighbours' velocities within distance [app.alignment]
	private Vector3 AlignToNeighbours ()
	{
		Vector3 combinedVelocities = Vector3.zero;
		int ct = 0;
		float distance;

		for ( int i = 0; i < visibleNeighbours.Count; i++ )
		{
			distance = Vector3.SqrMagnitude( transform.position - visibleNeighbours[ i ].gameObject.transform.position );
			if ( distance < ( app.alignment * app.alignment ) )
			{
				combinedVelocities += visibleNeighbours[ i ].velocity;
				ct++;
			}
		}

		if ( ct > 0 )
		{
			combinedVelocities = Vector3.Normalize( combinedVelocities / ct ) * app.maximumSpeed;
			combinedVelocities = Vector3.ClampMagnitude( combinedVelocities - _velocity, app.maximumTurnSpeed );
		}

		return combinedVelocities;
	}

	// try to find even spacing between neighbours within distance [app.cohesion]
	private Vector3 CohereToNeighbours ()
	{
		Vector3 combinedPositions = Vector3.zero;
		int ct = 0;
		float distance;

		for ( int i = 0; i < visibleNeighbours.Count; i++ )
		{
			distance = Vector3.SqrMagnitude( transform.position - visibleNeighbours[ i ].gameObject.transform.position );
			if ( distance < ( app.cohesion * app.cohesion ) )
			{
				combinedPositions += visibleNeighbours[ i ].transform.position;
				ct++;
			}
		}

		if ( ct > 0 )
		{
			//combinedPositions = SeekTarget( combinedPositions / ct );
			combinedPositions = ArriveAtTarget( combinedPositions / ct );
		}

		return combinedPositions;
	}




}
