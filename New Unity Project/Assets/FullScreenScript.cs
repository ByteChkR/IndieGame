using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FullScreenScript : MonoBehaviour, IHorizontalySelectable {

    private bool _isFullScreen;
    private List<string> _options = new List<string>();

    public Button leftButton;
    public Button rightButton;

    public Text FullScreenText;

	// Use this for initialization
	void Start () {
        _isFullScreen = Screen.fullScreen;
        
        _options.Add("YES");
        _options.Add("NO");

        if (_isFullScreen) FullScreenText.text = _options[0];
        else FullScreenText.text = _options[1];
	}

    private void ChangeFullScreen()
    {
        if (_isFullScreen)
        {
            _isFullScreen = !_isFullScreen;
            FullScreenText.text = _options[1];
        }
        else
        {
            _isFullScreen = !_isFullScreen;
            FullScreenText.text = _options[0];
        }
    }

    public void ScrollRight()
    {
        ChangeFullScreen();
    }

    public void ScrollLeft()
    {
        ChangeFullScreen();
    }

    public bool GetIsFullscreen()
    {
        return _isFullScreen;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
