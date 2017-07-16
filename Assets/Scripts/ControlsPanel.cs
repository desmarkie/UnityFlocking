using UnityEngine.UI;
using UnityEngine;

public class ControlsPanel : MonoBehaviour {

	[Header("Boid Controls")]

	[SerializeField]
	private SlideControl maxSpeedSlider;

	[SerializeField]
	private SlideControl maxTurnSpeedSlider;

	[SerializeField]
	private Toggle tailsToggle;

	[SerializeField]
	private Toggle meshToggle;

	[Header( "Flocking Controls" )]

	[SerializeField]
	private SlideControl separationSlider;

	[SerializeField]
	private SlideControl alignmentSlider;

	[SerializeField]
	private SlideControl cohesionSlider;

	[Header( "Target Controls" )]

	[SerializeField]
	private SlideControl xSlider;

	[SerializeField]
	private SlideControl ySlider;

	[SerializeField]
	private SlideControl zSlider;

	[SerializeField]
	private SlideControl distanceSlider;

	[SerializeField]
	private Toggle targetMeshToggle;

	[Header( "Simulation Controls" )]

	[SerializeField]
	private SlideControl boidCountSlider;

	[SerializeField]
	private SlideControl minMassSlider;

	[SerializeField]
	private SlideControl maxMassSlider;

	[SerializeField]
	private SlideControl visionSizeSlider;

	[SerializeField]
	private Toggle randomStartToggle;


	public void SetMaxSpeed ( float val )
	{
		maxSpeedSlider.ratio = val;
	}

	public void SetMaxTurnSpeed ( float val )
	{
		maxTurnSpeedSlider.ratio = val;
	}

	public void SetTailsToggle ( bool val )
	{
		tailsToggle.isOn = val;
	}

	public void SetMeshToggle ( bool val )
	{
		meshToggle.isOn = val;
	}

	public void SetSeparation ( float val )
	{
		separationSlider.ratio = val;
	}

	public void SetAlignment ( float val )
	{
		alignmentSlider.ratio = val;
	}

	public void SetCohesion ( float val )
	{
		cohesionSlider.ratio = val;
	}

	public void SetXSpeed ( float val )
	{
		xSlider.ratio = val;
	}

	public void SetYSpeed ( float val )
	{
		ySlider.ratio = val;
	}

	public void SetZSpeed ( float val )
	{
		zSlider.ratio = val;
	}

	public void SetMaxDistance ( float val )
	{
		distanceSlider.ratio = val;
	}

	public void SetTargetMeshToggle ( bool val )
	{
		targetMeshToggle.isOn = val;
	}

	public void SetBoidCount ( float val )
	{
		boidCountSlider.ratio = val;
	}

	public void SetMinMass ( float val )
	{
		minMassSlider.ratio = val;
	}

	public void SetMaxMass ( float val )
	{
		maxMassSlider.ratio = val;
	}

	public void SetVisionSize ( float val )
	{
		visionSizeSlider.ratio = val;
	}

	public void SetRandomStarts ( bool val )
	{
		randomStartToggle.isOn = val;
	}

}
