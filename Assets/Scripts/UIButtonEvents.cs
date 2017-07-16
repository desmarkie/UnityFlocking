using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonEvents : MonoBehaviour, IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
	public bool isDown = false;

	public void OnPointerDown ( PointerEventData data )
	{
		isDown = true;
	}

	public void OnPointerExit ( PointerEventData data )
	{
		isDown = false;
	}

	public void OnPointerUp ( PointerEventData data )
	{
		isDown = false;
	}
}