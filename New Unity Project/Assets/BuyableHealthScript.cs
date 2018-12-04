using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuyableHealthScript : MonoBehaviour {

    public int cost;
    public int health;
    public HealthInfoScript healthInfoScript;

    public void DestroyOnPickup()
    {
        Destroy(gameObject);
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
