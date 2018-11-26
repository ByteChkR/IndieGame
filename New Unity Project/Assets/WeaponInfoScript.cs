using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoScript : MonoBehaviour {

    public GameObject Weapon;
    public GameObject InfoBox;
    public Image Icon;
    public Text Prize;
    public Text Description;

    private int _goldCost;

	// Use this for initialization
	void Start () {

	}

    public void SetCost(int pGold)
    {
        _goldCost = pGold;
        if (_goldCost > 0) {
            Prize.text = "PRIZE: " + _goldCost;
        }
        else
        {
            Prize.text = null;
        }
    }

    void Awake()
    {
        SetCost(transform.parent.GetComponent<Weapon>().GoldValue);
        Debug.Log(_goldCost);
    }

    void RotateInfoBox()
    {
        InfoBox.transform.rotation = Quaternion.Euler(45, -Weapon.transform.rotation.y, 0);
    }

    void Update()
    {
        RotateInfoBox();
    }
}
