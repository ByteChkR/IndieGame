using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponInfoScript : MonoBehaviour {

    public Image icon;
    public Text goldCost;
    public Text description;

    private int _goldCost;

	// Use this for initialization
	void Start () {

	}

    private void SetCost(int pGold)
    {
        _goldCost = pGold;
        goldCost.text = "PRIZE: " + goldCost;
    }

    void Awake()
    {
        SetCost(transform.parent.GetComponent<Weapon>().GoldValue);
        Debug.Log(_goldCost);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
