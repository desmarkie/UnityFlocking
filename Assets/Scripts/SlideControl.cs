using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlideControl : MonoBehaviour {

	[SerializeField]
	private UIButtonEvents leftArrow;

	[SerializeField]
	private UIButtonEvents rightArrow;

	[SerializeField]
	private Slider dragHandle;

	[SerializeField]
	private TextMeshProUGUI slideLabel;

	private float _ratio = 0f;
	public float ratio
	{
		get { return _ratio; }
		set { _ratio = value; SetHandlePosition(); }
	}

	public void SetLabel ( string label )
	{
		slideLabel.SetText( label );
	}

	// Use this for initialization
	void Start () {
		dragHandle.onValueChanged.AddListener( delegate { HandleDragUpdate(); } );
	}
	
	// Update is called once per frame
	void Update () {

		if ( rightArrow.isDown || leftArrow.isDown )
		{

			if ( rightArrow.isDown ) _ratio += 0.5f * Time.deltaTime;
			else if ( leftArrow.isDown ) _ratio -= 0.5f * Time.deltaTime;

			_ratio = Mathf.Clamp( _ratio, 0f, 1f );
			SetHandlePosition();
		}



	}

	private void SetHandlePosition ()
	{
		dragHandle.value = _ratio;
	}

	public void HandleDragUpdate ()
	{
		_ratio = dragHandle.normalizedValue;
	}
}
