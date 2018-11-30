﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

    public Unit Enemy;
    public Image HealthBar;
    private float _fill;
    private float _currentHealth;
    Camera _cam;

	// Use this for initialization
	void Start () {
        _cam = Camera.main;
        transform.Rotate(new Vector3(0, 0, 0));
    }
    void RotateHealthBar()
    {
        transform.LookAt(_cam.transform.position);
        transform.Rotate(new Vector3(0, 180, 0));
    }
    

    // Update is called once per frame
    void LateUpdate () {
        //RotateHealthBar();
        HealthBar.transform.localScale = new Vector3(Enemy.Stats.CurrentHealth / Enemy.Stats.MaxHealth, 1, 1);
    }
}
