using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

    public Unit Enemy;
    public Image HealthBar;
    private float _fill;
    private float _currentHealth;

	// Use this for initialization
	void Start () {
	}
    void RotateHealthBar()
    {
        transform.rotation = Quaternion.Euler(40, -Enemy.transform.rotation.y, 0);
    }

    // Update is called once per frame
    void LateUpdate () {
        RotateHealthBar();
        HealthBar.transform.localScale = new Vector3(Enemy.Stats.CurrentHealth / Enemy.Stats.MaxHealth, 1, 1);
    }
}
