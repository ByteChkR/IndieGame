using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour {

    public Unit Enemy;
    public Image HealthBar;
    private float _fill;
    Camera _cam;

	// Use this for initialization
	void Start () {
        _cam = Camera.main;
        transform.forward = -CameraViewLock.instance.Cam.transform.forward;
    }
    void RotateHealthBar()
    {

      //  transform.forward = -CameraViewLock.instance.Cam.transform.forward;
        //transform.LookAt(_cam.transform.position);
        //transform.Rotate(new Vector3(0, 180, 0));
    }
    

    // Update is called once per frame
    void LateUpdate () {
        RotateHealthBar();
        _fill = Enemy.Stats.CurrentHealth / Enemy.Stats.MaxHealth;
        if (_fill < 0) _fill = 0;
        HealthBar.transform.localScale = new Vector3(_fill, 1, 1);
    }
}
