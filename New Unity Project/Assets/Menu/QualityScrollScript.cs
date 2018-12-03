using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class QualityScrollScript : MonoBehaviour, IHorizontalySelectable
{

    private int _currentIndex;
    private List<string> _options = new List<string>();

    public Button leftButton;
    public Button rightButton;
    public Text qualityText;

    // Use this for initialization
    void Start()
    {
        _currentIndex = 0;
 
        _options.Add("Very Low");
        _options.Add("Low");
        _options.Add("Medium");
        _options.Add("High");
        _options.Add("Very High");
        _options.Add("Ultra");

        _currentIndex = QualitySettings.GetQualityLevel();
        qualityText.text = _options[_currentIndex];

        Debug.Log(_options.Count);
        for (int i = 0; i < _options.Count; i++)
        {
            Debug.Log(_options[i]);
        }

    }

    public int GetQualityIndex()
    {
        return _currentIndex;
    }

    public void ScrollLeft()
    {
        _currentIndex--;
        if (_currentIndex > 0)
        {
            qualityText.text = _options[_currentIndex];
        }
        else
        {
            _currentIndex = _options.Count - 1;
            qualityText.text = _options[_currentIndex];
        }
    }

    public void ScrollRight()
    {
        _currentIndex++;
        if (_currentIndex < _options.Count)
        {
            qualityText.text = _options[_currentIndex];
        }
        else
        {
            _currentIndex = 0;
            qualityText.text = _options[_currentIndex];
        }
    }
}
