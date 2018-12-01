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
        _prize.text = "Prize: " + _goldCost;
    }

    void Awake()
    {
        SetCost(transform.parent.GetComponent<BuyableHealthScript>().cost);
    }
    void RotateInfoBox()
    {
        _infoBox.transform.rotation = Quaternion.Euler(45, -_buyAbleHealth.transform.rotation.y - 45, 0);
    }

    // Update is called once per frame
    void LateUpdate () {
        RotateInfoBox();
	}
}
