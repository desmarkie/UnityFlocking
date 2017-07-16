using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AppController : MonoBehaviour {

	[Header( "Audio Track" )]
	[SerializeField]
	private AudioSource soundTrack;

	[Header("Prefabs")]

	[SerializeField]
	private GameObject boidPrefab;

	[SerializeField]
	private GameObject targetObject;
	public Vector3 targetPosition
	{
		get { return targetObject.transform.position; }
	}


	[Header("Setup")]

	[SerializeField]
	private int numberOfBoids = 50;

	[Range( 10f, 100f )]
	[SerializeField]
	private float minMass = 20f;

	[Range( 10f, 100f )]
	[SerializeField]
	private float maxMass = 20f;

	[Range( 15f, 100f )]
	[SerializeField]
	private float visionSize = 15f;

	[SerializeField]
	private bool randomStartPositions = false;


	[Header( "Boid Options" )]

	[SerializeField]
	private float maxSpeed = 0.5f;
	public float maximumSpeed
	{
		get { return maxSpeed; }
	}

	[SerializeField]
	private float maxTurnSpeed = 1f;
	public float maximumTurnSpeed
	{
		get { return maxTurnSpeed; }
	}

	[SerializeField]
	private bool showTrail = false;

	[SerializeField]
	private bool showMesh = true;


	[Header( "Flocking Constraints" )]

	[Range( 0.01f, 100f )]
	[SerializeField]
	private float separationDistance = 2f;
	public float separation
	{
		get { return separationDistance; }
	}

	[Range( 0.01f, 100f )]
	[SerializeField]
	private float alignmentDistance = 4f;
	public float alignment
	{
		get { return alignmentDistance; }
	}

	[Range( 0.01f, 100f )]
	[SerializeField]
	private float cohesionDistance = 4f;
	public float cohesion
	{
		get { return cohesionDistance; }
	}



	[Header( "Target Options" )]

	[SerializeField]
	private Vector3 sinSpeed = new Vector3();

	[SerializeField]
	private float maxDistance = 10f;

	[SerializeField]
	private bool showTargetMesh = false;


	private List<Boid> boids;
	private List<Boid> deadBoids;

	private int totalBoidCount = 0;


	private Vector3 targetMotion = Vector3.zero;

	private FlockingCameraController camControl;
	private CanvasController canvasController;

	private float initialSpeed;
	private float initialTurnSpeed;
	private bool initialShowMeshes;
	private bool initialShowTails;
	private float initialSep;
	private float initialAli;
	private float initialCoh;
	private float initialX;
	private float initialY;
	private float initialZ;
	private float initialDistance;
	private GameObject initialMesh;
	private int initialBoids;
	private float initialMin;
	private float initialMax;
	private float initialVision;
	private bool initialRandom;

	private bool audioMode = false;
	private bool simulationHasStarted = false;

	private float timeSinceLastCameraChange = 0f;

	private bool _autoCamera = false;
	public bool autoCamera
	{
		get { return _autoCamera; }
		set { _autoCamera = value; }
	}



	public void ShowBoidTrails ( bool val )
	{
		if ( boids.Count == 0 ) return;
		for ( int i = 0; i < boids.Count; i++ )
		{
			boids[ i ].ShowTrail( val );
		}
	}

	public void ShowBoidMeshes ( bool val )
	{
		if ( boids.Count == 0 ) return;
		for ( int i = 0; i < boids.Count; i++ )
		{
			boids[ i ].ShowMesh( val );
		}
	}

	public void UpdateMaxSpeed ( float val )
	{
		maxSpeed = Mathf.Lerp( 0.01f, 100f, val );
	}

	public void UpdateMaxTurningSpeed ( float val )
	{
		maxTurnSpeed = Mathf.Lerp( 0.01f, 100f, val );
	}

	public void UpdateSeparation ( float val )
	{
		separationDistance = Mathf.Lerp( 0.1f, 100f, val );
	}

	public void UpdateAlignment ( float val )
	{
		alignmentDistance = Mathf.Lerp( 0.1f, 100f, val );
	}

	public void UpdateCohesion ( float val )
	{
		cohesionDistance = Mathf.Lerp( 0.1f, 100f, val );
	}

	public void UpdateSinX ( float val )
	{
		sinSpeed.x = Mathf.Lerp( 0f, 360f, val );
	}

	public void UpdateSinY ( float val )
	{
		sinSpeed.y = Mathf.Lerp( 0f, 360f, val );
	}

	public void UpdateSinZ ( float val )
	{
		sinSpeed.z = Mathf.Lerp( 0f, 360f, val );
	}

	public void UpdateRadius ( float val )
	{
		maxDistance = Mathf.Lerp( 10f, 360f, val );
	}

	public void ShowTargetMesh ( bool val )
	{
		targetObject.GetComponent<MeshRenderer>().enabled = val;
	}

	public void UpdateBoidCount ( float val )
	{
		numberOfBoids = ( int ) Mathf.Lerp( 1, 300, val );
	}

	public void UpdateMinMass ( float val )
	{
		minMass = Mathf.Lerp( 0.1f, 100f, val );
	}

	public void UpdateMaxMass ( float val )
	{
		maxMass = Mathf.Lerp( 0.1f, 100f, val );
	}

	public void UpdateVision ( float val )
	{
		visionSize = Mathf.Lerp( 1f, 100f, val );
	}

	public void UpdateRandomStart ( bool val )
	{
		randomStartPositions = val;
	}

	public void RestartSimulation ()
	{
		RemoveBoids();
		StartSimulation();
	}

	public void StartAudioMode ()
	{
		timeSinceLastCameraChange = 0f;
		audioMode = true;
		SetCameraMode( FlockingCameraController.STATIC, false );
		simulationHasStarted = false;
		RestartSimulation();
		canvasController.PlayIntro( true );
	}

	public void StartInteractiveMode ()
	{
		audioMode = false;
		simulationHasStarted = false;
		SetStartStates();
		maxSpeed = 6f;
		maxTurnSpeed = 5f;
		separationDistance = 20f;
		alignmentDistance = 24f;
		cohesionDistance = 8f;
		sinSpeed.x = 60f;
		sinSpeed.y = 90f;
		sinSpeed.z = 120f;
		maxDistance = 140f;
		RestartSimulation();
		canvasController.PlayIntro( false );
		SetCameraMode( FlockingCameraController.STATIC );
	}

	public void CloseApplication ()
	{
		Application.Quit();
	}

	private void RemoveBoids ()
	{
		Boid boid;
		while ( boids.Count > 0 )
		{
			boid = boids[ 0 ];
			boid.isAlive = false;
			boids.RemoveAt( 0 );
			deadBoids.Add( boid );
		}
	}

	private Boid GetBoid ( Vector3 pos )
	{
		Boid boid;
		if ( deadBoids.Count > 0 )
		{
			boid = deadBoids[ 0 ];
			deadBoids.RemoveAt( 0 );
			boid.transform.position = pos;
			return boid;
		}
		else
		{
			GameObject obj = Instantiate( boidPrefab );
			obj.name = "Boid_" + totalBoidCount;
			totalBoidCount++;
			boid = obj.GetComponent<Boid>();
			return boid;
		}
	}

	private void StartSimulation ()
	{
		for ( int i = 0; i < numberOfBoids; i++ )
		{
			Vector3 pos = Vector3.zero;
			if ( randomStartPositions )
			{
				pos = new Vector3(
					Random.Range( -maxDistance, maxDistance ),
					Random.Range( -maxDistance, maxDistance ),
					Random.Range( -maxDistance, maxDistance )
				);
			}

			Boid boid = GetBoid( pos );

			boid.InitialiseBoid(
				this,
				Random.Range( minMass, maxMass ),
				visionSize
			);

			boid.ShowMesh( showMesh );
			boid.ShowTrail( showTrail );

			boids.Add( boid );
		}

		//for ( int i = 0; i < numberOfBoids; i++ )
		//{
		//	boids[ i ].SetNeighbours( boids );
		//}


		camControl.SetTargetBoid( boids[ Random.Range( 0, numberOfBoids ) ] );

		if ( audioMode )
		{
			timeSinceLastCameraChange = 0f;
			soundTrack.Play();
		}

		simulationHasStarted = true;
	}

	private void SetStartStates ()
	{
		canvasController.SetInitialStates(
			Mathf.Lerp( 0, 1, Mathf.InverseLerp( 0.01f, 100f, initialSpeed ) ),
			Mathf.Lerp( 0, 1, Mathf.InverseLerp( 0.01f, 100f, initialTurnSpeed ) ),
			initialShowMeshes,
			initialShowTails,
			Mathf.Lerp( 0, 1, Mathf.InverseLerp( 0.1f, 100f, initialSep ) ),
			Mathf.Lerp( 0, 1, Mathf.InverseLerp( 0.1f, 100f, initialAli ) ),
			Mathf.Lerp( 0, 1, Mathf.InverseLerp( 0.1f, 100f, initialCoh ) ),
			Mathf.Lerp( 0, 1, Mathf.InverseLerp( 0f, 360f, initialX ) ),
			Mathf.Lerp( 0, 1, Mathf.InverseLerp( 0f, 360f, initialY ) ),
			Mathf.Lerp( 0, 1, Mathf.InverseLerp( 0f, 360f, initialZ ) ),
			Mathf.Lerp( 0, 1, Mathf.InverseLerp( 0f, 360f, initialDistance ) ),
			initialMesh,
			Mathf.Lerp( 0, 1, Mathf.InverseLerp( 1f, 300f, initialBoids ) ),
			Mathf.Lerp( 0, 1, Mathf.InverseLerp( 0f, 100f, initialMin ) ),
			Mathf.Lerp( 0, 1, Mathf.InverseLerp( 0f, 100f, initialMax ) ),
			Mathf.Lerp( 0, 1, Mathf.InverseLerp( 0f, 100f, initialVision ) ),
			initialRandom
		);
	}

	void Start () {

		initialSpeed = maxSpeed;
		initialTurnSpeed = maxTurnSpeed;
		initialShowMeshes = showMesh;
		initialShowTails = showTrail;
		initialSep = separation;
		initialAli = alignment;
		initialCoh = cohesion;
		initialX = sinSpeed.x;
		initialY = sinSpeed.y;
		initialZ = sinSpeed.z;
		initialDistance = maxDistance;
		initialMesh = targetObject;
		initialBoids = numberOfBoids;
		initialMin = minMass;
		initialMax = maxMass;
		initialVision = visionSize;
		initialRandom = randomStartPositions;

		// update UI to reflect settings
		canvasController = GetComponent<CanvasController>();

		SetStartStates();

	
		// create new Boid list
		boids = new List<Boid>();
		deadBoids = new List<Boid>();

		// show/hide target object
		targetObject.GetComponent<Renderer>().enabled = showTargetMesh;

		camControl = GetComponent<FlockingCameraController>();
		camControl.SetTargetObject( targetObject );


	}
	

	void Update () {

		if ( audioMode || autoCamera ) timeSinceLastCameraChange += Time.deltaTime;
		if ( timeSinceLastCameraChange >= 12f )
		{
			timeSinceLastCameraChange = 0f;
			SetCameraMode( ( camControl.GetCameraMode() + 1 ) % 4, false );
		}

		// using sin for the target's motion
		targetMotion += sinSpeed * Time.deltaTime;

		targetMotion.x %= 360f;
		targetMotion.y %= 360f;
		targetMotion.z %= 360f;

		Vector3 vec = new Vector3(
			Mathf.Sin( Mathf.Deg2Rad * targetMotion.x ) * maxDistance,
			Mathf.Sin( Mathf.Deg2Rad * targetMotion.y ) * maxDistance,
			Mathf.Sin( Mathf.Deg2Rad * targetMotion.z ) * maxDistance
		);

		targetObject.transform.position = vec;

		// switch camera modes
		if ( Input.GetKeyDown( KeyCode.Alpha1 ) ) SetCameraMode( FlockingCameraController.STATIC );
		if ( Input.GetKeyDown( KeyCode.Alpha2 ) ) SetCameraMode( FlockingCameraController.BOID_FOLLOW );
		if ( Input.GetKeyDown( KeyCode.Alpha3 ) ) SetCameraMode( FlockingCameraController.TARGET_FOLLOW );
		if ( Input.GetKeyDown( KeyCode.Alpha4 ) ) SetCameraMode( FlockingCameraController.WATCH_FROM_CENTER );
		if ( Input.GetKeyDown( KeyCode.Alpha5 ) ) SetCameraMode( FlockingCameraController.USER_CONTROL );
		if ( Input.GetKeyDown( KeyCode.A ) ) autoCamera = !autoCamera;
		if ( Input.GetKeyDown( KeyCode.Escape ) )
		{
			simulationHasStarted = false;
			soundTrack.Stop();
			soundTrack.time = 0f;
			canvasController.ShowStartScreen();
		}

		if ( audioMode ) UpdateAudioAnalysis();

		if ( audioMode && soundTrack.time >= 130f && simulationHasStarted )
		{
			simulationHasStarted = false;
			audioMode = false;
			canvasController.ShowStartScreen();
		}


	}

	private void UpdateAudioAnalysis ()
	{
		float[] spectrum = new float[ 256 ];
		float val = 0;
		float lowBucket = 0;
		float midBucket = 0;
		float highBucket = 0;
		int breakOne = 25;
		int breakTwo = 145;
		soundTrack.GetSpectrumData( spectrum, 0, FFTWindow.Rectangular );
		for ( int i = 0; i < 256; i++ )
		{
			if ( i < breakOne ) lowBucket += spectrum[ i ];
			else if ( i >= breakOne && i < breakTwo ) midBucket += spectrum[ i ];
			else highBucket += spectrum[ i ];
			val += spectrum[ i ];
		}
		maxDistance = (val * val) * 23;

		maxSpeed = Mathf.Pow(lowBucket,3) * 10;
		maxTurnSpeed = midBucket * 40;
		separationDistance = 10 + (Mathf.Pow(highBucket, 5) * 20);
	}

	private void SetCameraMode ( int cameraMode, bool manual = true )
	{
		if ( manual && audioMode ) return;
		if ( manual ) _autoCamera = false;
		camControl.SetCameraMode( cameraMode );
		if ( !audioMode ) canvasController.SetCameraMode( cameraMode );
	}

	private Vector3 GetRandomVector ( float min, float max )
	{
		return new Vector3(
			Random.Range( min, max ),
			Random.Range( min, max ),
			Random.Range( min, max )
		);
	}

}
