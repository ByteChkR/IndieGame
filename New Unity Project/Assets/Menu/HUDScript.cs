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
    public bool BossBarTest;
    public float TestBossCurrentHP; 
    public float TestMaxBossHP;
    public Image HealthBar;
    public Image ComboBar;
    public Image BossHealthBar;
    public GameObject BossHealth;
    public Text HealthAmount;
    public Text ComboAmount;
    public Text GoldAmount;

    public static HUDScript instance;

    private Unit _boss;
    private float _playerHealthNewScale;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        GoldAmount.text = " X " + Gold;
    }

    public void SetBoss(Unit pBoss)
    {
        _boss = pBoss;
    }

    public void RemoveBoss()
    {
        _boss = null;
    }

    private void UpdateBossHealth()
    {
        if(_boss != null)
        {
            BossHealth.SetActive(true);
            float newScale = _boss.Stats.CurrentHealth / _boss.Stats.MaxHealth;
            if (newScale < 0) newScale = 0;
            BossHealthBar.transform.localScale = new Vector3(newScale, 1, 1);
        }
        else if(BossBarTest)
        {
            BossHealth.SetActive(true);
            float newScale = TestBossCurrentHP / TestMaxBossHP;
            if (newScale < 0) newScale = 0;
            BossHealthBar.transform.localScale = new Vector3(newScale, 1, 1);
        }
        else
        {
            BossHealth.SetActive(false);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Unit.Player == null) return;
        ComboAmount.text = (int)Unit.Player.Stats.CurrentCombo + " / " + Unit.Player.Stats.MaxCombo;
        HealthAmount.text = (int)Unit.Player.Stats.CurrentHealth + " / " + Unit.Player.Stats.MaxHealth;
        GoldAmount.text = " X " + Unit.Player.Stats.CurrentGold;
        _playerHealthNewScale = Unit.Player.Stats.CurrentHealth / Unit.Player.Stats.MaxHealth;
        if (_playerHealthNewScale < 0) _playerHealthNewScale = 0;
        HealthBar.transform.localScale = new Vector3(Unit.Player.Stats.CurrentHealth / Unit.Player.Stats.MaxHealth, 1, 1);
        ComboBar.transform.localScale = new Vector3(Unit.Player.Stats.CurrentCombo / Unit.Player.Stats.MaxCombo, 1, 1);
        UpdateBossHealth();
    }
}
