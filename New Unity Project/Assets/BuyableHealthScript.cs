using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyableHealthScript : MonoBehaviour {

    public int cost;
    public int health;
    public HealthInfoScript healthInfoScript;
    public Text prizeText;

    void Awake()
    {
        prizeText.text = "Prize: " + cost;
    }

    public void ActivateInfoBox()
    {
        healthInfoScript.gameObject.SetActive(true);
    }

    public void DeactivateInfoBox()
    {
        healthInfoScript.gameObject.SetActive(false);
    }

}
