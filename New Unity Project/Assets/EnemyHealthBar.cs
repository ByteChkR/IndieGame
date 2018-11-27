using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

    public Unit Enemy;
    public Image HealthBar;
    private float _fill;
    private float _maxHealth;
    private float _currentHealth;

	// Use this for initialization
	void Start () {
        _maxHealth = Enemy.Stats.MaxHealth;
	}

	// Update is called once per frame
	void LateUpdate () {
        HealthBar.transform.localScale = new Vector3(Enemy.Stats.CurrentHealth / Enemy.Stats.MaxHealth, 1, 1);
    }
}
