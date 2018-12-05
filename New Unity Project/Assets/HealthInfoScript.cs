using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthInfoScript : MonoBehaviour {

    [SerializeField, Tooltip("Insert Tooltip")]
    private GameObject _buyAbleHealth;
    [SerializeField, Tooltip("Insert Tooltip")]
    private GameObject _infoBox;
    [SerializeField, Tooltip("Insert Tooltip")]
    private Image _icon;
    [SerializeField, Tooltip("Insert Tooltip")]
    private Text _prize;
    [SerializeField, Tooltip("Insert Tooltip")]
    private Text _description;

    private int _goldCost;

    // Use this for initialization
    void Start () {
		
	}
	
    void SetCost(int pCost)
    {
        _goldCost = pCost;
        _prize.text = "Price: " + _goldCost;
    }

    void Awake()
    {
        SetCost(transform.parent.GetComponent<BuyableHealthScript>().cost);
    }
    void RotateInfoBox()
    {
        _infoBox.transform.position = _buyAbleHealth.transform.position + Vector3.up * 2;
        _infoBox.transform.forward = Camera.main.transform.forward;
    }

    // Update is called once per frame
    void LateUpdate () {
        RotateInfoBox();
	}
}
