using UnityEngine;
using UnityEngine.UI;
using NDTweener;
using TMPro;

public class CanvasController : MonoBehaviour
{
	[SerializeField]
	private GameObject titlePanel;

	[SerializeField]
	private GameObject cameraInfoPanel;

	[SerializeField]
	private GameObject keyboardInfoPanel;

	[SerializeField]
	private Button controlsButton;

	[SerializeField]
	private ControlsPanel controlsPanel;

	[SerializeField]
	private GameObject panelHolder;

	[SerializeField]
	private GameObject buttonHolder;

	[SerializeField]
	private GameObject pageIndicator;

	[SerializeField]
	private GameObject musicButton;

	[SerializeField]
	private GameObject autoButton;

	[SerializeField]
	private GameObject quitButton;

	[SerializeField]
	private GameObject cameraControlsInfo;

	private string[] cameraTypes;

	private bool controlsOpen = false;

	private bool showingKeyboardControls = false;



	public void SetInitialStates (
								  float maxSpeed,
								  float maxTurnSpeed,
								  bool showMeshes,
								  bool showTails,
								  float sep,
								  float ali,
								  float coh,
								  float xSpeed,
								  float ySpeed,
								  float zSpeed,
								  float maxDistance,
								  bool targetMesh,
								  float boidCount,
								  float minMass,
								  float maxMass,
								  float visionSize,
								  bool randomStart
								 )
	{
		

		controlsPanel.SetMaxSpeed( maxSpeed );
		controlsPanel.SetMaxTurnSpeed( maxTurnSpeed );
		controlsPanel.SetMeshToggle( showMeshes );
		controlsPanel.SetTailsToggle( showTails );
		controlsPanel.SetSeparation( sep );
		controlsPanel.SetAlignment( ali );
		controlsPanel.SetCohesion( coh );
		controlsPanel.SetXSpeed( xSpeed );
		controlsPanel.SetYSpeed( ySpeed );
		controlsPanel.SetZSpeed( zSpeed );
		controlsPanel.SetMaxDistance( maxDistance );
		controlsPanel.SetTargetMeshToggle( targetMesh );
		controlsPanel.SetBoidCount( boidCount );
		controlsPanel.SetMinMass( minMass );
		controlsPanel.SetMaxMass( maxMass );
		controlsPanel.SetVisionSize( visionSize );
		controlsPanel.SetRandomStarts( randomStart );
	}

	public void SetCameraMode ( int cameraMode )
	{
		cameraInfoPanel.GetComponentInChildren<TextMeshProUGUI>().SetText( cameraTypes[ cameraMode ] );

		NDTween.RemoveAllTweens( cameraInfoPanel, true );

		NDTweenOptions opts = new NDTweenOptions();
		opts.clearCurrentTweens = false;
		opts.easing = Easing.quartOut;

		NDUITween.AlphaTo( cameraInfoPanel, 0.3f, 1f, opts );

		opts.delay = 2.5f;

		NDUITween.AlphaTo( cameraInfoPanel, 1f, 0f, opts );

		ShowKeyboardControls( cameraMode == FlockingCameraController.USER_CONTROL );

	}

	private void ShowKeyboardControls ( bool val )
	{
		if ( val == showingKeyboardControls ) return;
		showingKeyboardControls = val;

		float alpha = val ? 1f : 0f;
		NDUITween.AlphaTo( keyboardInfoPanel, 0.5f, alpha, Easing.quartOut );
	}

	public void ShowPageOne ()
	{
		Vector2 pos = panelHolder.GetComponent<RectTransform>().anchoredPosition;
		pos.y = 0;
		panelHolder.GetComponent<RectTransform>().anchoredPosition = pos;

		NDTween.RemoveAllTweens( pageIndicator );
		NDUITween.To( pageIndicator, 0.3f, new Vector2( 10f, -90f ), Easing.quartOut );
	}

	public void ShowPageTwo ()
	{
		Vector2 pos = panelHolder.GetComponent<RectTransform>().anchoredPosition;
		pos.y = 1200;
		panelHolder.GetComponent<RectTransform>().anchoredPosition = pos;

		NDTween.RemoveAllTweens( pageIndicator );
		NDUITween.To( pageIndicator, 0.3f, new Vector2( 10f, -150f ), Easing.quartOut );
	}

	public void ShowPageThree ()
	{
		Vector2 pos = panelHolder.GetComponent<RectTransform>().anchoredPosition;
		pos.y = 2400;
		panelHolder.GetComponent<RectTransform>().anchoredPosition = pos;

		NDTween.RemoveAllTweens( pageIndicator );
		NDUITween.To( pageIndicator, 0.3f, new Vector2( 10f, -210f ), Easing.quartOut );
	}

	private void HideCameraInfo ()
	{
		NDUITween.AlphaTo( cameraInfoPanel, 1f, 0f, Easing.quartOut );
	}

	void Start ()
	{
		cameraTypes = new string[] { "Static Camera", "Watch Camera", "Follow Camera", "Lookback Camera", "Free Camera" };

		controlsPanel.transform.position = new Vector3( -640f, 0f, 0f );

		NDUITween.AlphaTo( cameraInfoPanel, 0f, 0f );
		NDUITween.AlphaTo( keyboardInfoPanel, 0f, 0f );

	}

	public void PlayIntro ( bool audioMode )
	{
		// fade out title screen
		musicButton.SetActive( false );
		autoButton.SetActive( false );
		quitButton.SetActive( false );

		cameraControlsInfo.SetActive( !audioMode );

		NDTweenOptions opts = new NDTweenOptions();
		opts.clearCurrentTweens = false;

		opts.easing = Easing.quartOut;

		NDUITween.AlphaTo( 
     		titlePanel,
			4f,
			0f, 
			opts
        );

		if ( !audioMode )
		{
			opts.delay = 2f;
			NDUITween.To(
				buttonHolder,
				0.5f,
				new Vector2( 640f, 60f ),
				opts
			);
		}
		
	}

	public void ShowStartScreen ()
	{
		NDTweenWorker tween = NDUITween.AlphaTo(
			titlePanel,
			4f,
			1f,
			Easing.quartOut
		);

		NDUITween.To(
			buttonHolder,
			0.5f,
			new Vector2( 640f, 0f ),
			Easing.quartOut
		);

		Vector3 pos = Vector3.zero;
		pos.x = -640f;

		NDTween.RemoveAllTweens( controlsPanel.gameObject );
		NDUITween.To( controlsPanel.gameObject, 0.5f, pos, Easing.quartOut );

		tween.OnTweenComplete += HandleStartShown;

	}

	private void HandleStartShown()
	{

		musicButton.SetActive( true );
		autoButton.SetActive( true );
		quitButton.SetActive( true );
	}

	public void HandleControlsClick ()
	{
		Debug.Log("Controls clicked!");
		controlsOpen = !controlsOpen;
		UpdateControlPanelPosition();
	}

	private void UpdateControlPanelPosition ()
	{
		Vector3 pos = Vector3.zero;

		if ( controlsOpen ) pos.x = 0f;
		else pos.x = -640f;

		NDTween.RemoveAllTweens( controlsPanel.gameObject );
		NDUITween.To( controlsPanel.gameObject, 0.5f, pos, Easing.quartOut );

		pos = new Vector3( 640f, 60f );
		if ( controlsOpen ) pos.y = 240f;

		NDTween.RemoveAllTweens( buttonHolder );
		NDUITween.To( buttonHolder, 0.5f, pos, Easing.quartOut );
	}
}
