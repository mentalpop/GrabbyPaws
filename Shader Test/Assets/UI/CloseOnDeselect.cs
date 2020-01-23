using UnityEngine.EventSystems;
using UnityEngine;

public class CloseOnDeselect : MonoBehaviour, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool mouseIsOver = false;

    public delegate void DeselectEvent ();
	public event DeselectEvent OnDeselected = delegate { };
    
	private void OnEnable() {
		EventSystem.current.SetSelectedGameObject(gameObject);
	}
	
	public void OnDeselect(BaseEventData eventData) {
//Close the Window on Deselect only if a click occurred outside this panel
		if (!mouseIsOver)
			OnDeselected.Invoke();
	}

	public void OnPointerEnter(PointerEventData eventData) {
		mouseIsOver = true;
		EventSystem.current.SetSelectedGameObject(gameObject);
	}
		
	public void OnPointerExit(PointerEventData eventData) {
		mouseIsOver = false;
		EventSystem.current.SetSelectedGameObject(gameObject);
	}
}