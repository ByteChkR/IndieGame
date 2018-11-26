using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{

    public int MaxHealth = 100;
    public float Health;
    public int MaxCombo = 10;
    public float Combo = 0f;
    public int Gold = 0;

    public Image HealthBar;
    public Image ComboBar;
    public Text HealthAmount;
    public Text ComboAmount;
    public Text GoldAmount;

    // Use this for initialization
    void Start()
    {
        Health = MaxHealth;
    }


    void InitializeHudHealth(int pMaxHealth, int pHealth = 0)
    {
        MaxHealth = pMaxHealth;
        if (!(pHealth == Health || pHealth == 0))
        {
            UpdateHealth(pHealth);
        }
    }

    void InitializeHudCombo(int pMaxCombo, float pCombo = 0)
    {
        MaxCombo = pMaxCombo;
        UpdateCombo(pCombo);
    }
    void UpdateHealth(int pHealth)
    {
        Health = pHealth;
        HealthAmount.text = Health + " / " + MaxHealth;
        HealthBar.transform.localScale = new Vector3(Health / MaxHealth, 1, 1);
    }

    void UpdateCombo(float pCombo)
    {
        Combo = pCombo;
        ComboAmount.text = Combo + " / " + MaxCombo;
        ComboBar.transform.localScale = new Vector3(Combo / MaxCombo, 1, 1);
    }

    void UpdateGold(int pGold)
    {
        Gold = pGold;
        GoldAmount.text = "GOLD : " + Gold;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Unit.Player == null) return;
        ComboAmount.text = (int)Unit.Player.Stats.CurrentCombo + " / " + Unit.Player.Stats.MaxCombo;
        HealthAmount.text = (int)Unit.Player.Stats.CurrentHealth + " / " + Unit.Player.Stats.MaxHealth;
        GoldAmount.text = "GOLD : " + Unit.Player.Stats.CurrentGold;
        HealthBar.transform.localScale = new Vector3(Unit.Player.Stats.CurrentHealth / Unit.Player.Stats.MaxHealth, 1, 1);
        ComboBar.transform.localScale = new Vector3(Unit.Player.Stats.CurrentCombo / Unit.Player.Stats.MaxCombo, 1, 1);
    }
}
