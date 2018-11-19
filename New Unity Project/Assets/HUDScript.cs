using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour {

    public int _maxHealth = 100;
    public float _health;
    public int _maxCombo = 10;
    public float _combo = 0f;
    public int _gold = 0;

    public Image healthBar;
    public Image comboBar;
    public Text healthAmount;
    public Text comboAmount;
    public Text goldAmount;

    // Use this for initialization
    void Start () {
        _health = _maxHealth;
	}
	

    void InitializeHudHealth(int pMaxHealth, int pHealth = 0)
    {
        _maxHealth = pMaxHealth;
        if(!(pHealth == _health || pHealth == 0))
        {
            UpdateHealth(pHealth);
        }
    }

    void InitializeHudCombo(int pMaxCombo, float pCombo = 0)
    {
        _maxCombo = pMaxCombo;
        UpdateCombo(pCombo);
    }
    void UpdateHealth(int pHealth)
    {
        _health = pHealth;
        healthAmount.text = _health + " / " + _maxHealth;
        healthBar.transform.localScale = new Vector3(_health / _maxHealth,1,1);
    }

    void UpdateCombo(float pCombo)
    {
        _combo = pCombo;
        comboAmount.text = _combo + " / " + _maxCombo;
        comboBar.transform.localScale = new Vector3(_combo / _maxCombo, 1, 1);
    }

    void UpdateGold(int pGold)
    { 
        _gold = pGold;
        goldAmount.text = "GOLD : " + _gold;
    }

	// Update is called once per frame
	void Update () {
        comboAmount.text = (int)_combo + " / " + _maxCombo;
        healthAmount.text = (int)_health + " / " + _maxHealth;
        goldAmount.text = "GOLD : " + _gold;
        healthBar.transform.localScale = new Vector3(_health / _maxHealth, 1, 1);
        comboBar.transform.localScale = new Vector3(_combo / _maxCombo, 1, 1);
    }
}
