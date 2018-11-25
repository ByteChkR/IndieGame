using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoScript : MonoBehaviour {

    public GameObject weapon;
    public GameObject InfoBox;
    public Image icon;
    public Text prize;
    public Text description;

    private int _goldCost;

	// Use this for initialization
	void Start () {

	}

    public void SetCost(int pGold)
    {
        _goldCost = pGold;
        if (_goldCost > 0) {
            prize.text = "PRIZE: " + _goldCost;
        }
        else
        {
            prize.text = null;
        }
    }

    void Awake()
    {
        SetCost(transform.parent.GetComponent<Weapon>().GoldValue);
        Debug.Log(_goldCost);
    }

    void RotateInfoBox()
    {
        InfoBox.transform.rotation = Quaternion.Euler(45, -weapon.transform.rotation.y, 0);
        Debug.Log("checking for spam");
    }

    void Update()
    {
        RotateInfoBox();
    }
}
