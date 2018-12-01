using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyableHealthScript : MonoBehaviour {

    public int cost;
    public int health;
    public HealthInfoScript healthInfoScript;

	// Use this for initialization
	void Start () {
		
	}
    public void ActivateInfoBox()
    {
        healthInfoScript.gameObject.SetActive(true);
    }

    public void DeactivateInfoBox()
    {
        healthInfoScript.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update () {
		
	}
}
