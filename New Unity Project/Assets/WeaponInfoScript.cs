using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoScript : MonoBehaviour {

    public Image icon;
    public Text prize;
    public Text description;

    private int _goldCost;

	// Use this for initialization
	void Start () {

	}

    private void SetCost(int pGold)
    {
        _goldCost = pGold;
        prize.text = "PRIZE: " + _goldCost;
    }

    void Awake()
    {
        SetCost(transform.parent.GetComponent<Weapon>().GoldValue);
        Debug.Log(_goldCost);
    }
}
