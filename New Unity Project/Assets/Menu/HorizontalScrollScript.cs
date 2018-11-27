using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HorizontalScrollScript : MonoBehaviour, IHorizontalySelectable
{

    private Resolution[] _resolutions;
    private int _currentIndex;
    private List<string> _options = new List<string>();

    public Button leftButton;
    public Button rightButton;
    public Text resolutionText;

    // Use this for initialization
    void Start () {
        _resolutions = Screen.resolutions;

        int currentResolutionIndex = 0;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height;
            _options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width && _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        _currentIndex = currentResolutionIndex;
        resolutionText.text = _options[_currentIndex];

        Debug.Log(_options.Count);
        for(int i = 0; i < _options.Count; i++)
        {
            Debug.Log(_options[i]);
        }

    }

    public int GetResolutionIndex()
    {
        return _currentIndex;
    }

    public void ScrollLeft()
    {
        _currentIndex--;
        if (_currentIndex > 0)
        {
            resolutionText.text = _options[_currentIndex];
        }
        else
        {
            _currentIndex = _options.Count -1;
            resolutionText.text = _options[_currentIndex];
        }
    }

    public void ScrollRight()
    {
        _currentIndex++;
        if (_currentIndex < _options.Count)
        {
            resolutionText.text = _options[_currentIndex];
        }
        else
        {
            _currentIndex = 0;
            resolutionText.text = _options[_currentIndex];
        }
    }
}
