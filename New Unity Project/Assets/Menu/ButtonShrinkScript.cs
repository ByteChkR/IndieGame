using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonShrinkScript : MonoBehaviour, ISelectHandler, IDeselectHandler {

    private static Vector3 _normalScale = new Vector3(1, 1, 1);
    private static Vector3 _highlightedScale = new Vector3(0.9f, 0.9f, 0.9f);
    private static Vector3 _pressedScale = new Vector3(0.8f, 0.8f, 0.8f);

    // Use this for initialization
    void Start () {

	}

    public void OnSelect(BaseEventData eventData)
    {
        gameObject.transform.localScale = _pressedScale;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        gameObject.transform.localScale = _normalScale;
    }

    public void OnHoverEnter()
    {
        gameObject.transform.localScale = _highlightedScale;
    }
    public void OnHoverExit()
    {
        gameObject.transform.localScale = _normalScale;
    }
}
