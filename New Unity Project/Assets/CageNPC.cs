using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CageNPC : MonoBehaviour {

    public GameObject DialogueTriggerPrefab;
    public Vector3 prefabOffSet;
    public GameObject canvasCage;
    public Slider progressBar;
    public float maxTimeToState = 4;
    private float _currentTimeOn = 0 ;
    private bool _isSaved = false;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      
        if (Input.GetKeyDown(KeyCode.B))
        {
            ResetTime();
        }

    }

    private void LateUpdate()
    {
        if (_isSaved == true)
        {
            return;
        }

        progressBar.value = _currentTimeOn / maxTimeToState;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _currentTimeOn += Time.deltaTime;
            if (_currentTimeOn > maxTimeToState)
            {
                CreateCoins();
                CreateDialoguePrefab();
                AddCrystal();
                Destroy(canvasCage.gameObject);
                Destroy(this);

            }
        }
     
       

    }

    private void OnTriggerExit(Collider other)
    {
        
        if(other.tag == "Player")
        {
            ResetTime();
        }
    }

    private void CreateCoins()
    {

    }

    private void CreateDialoguePrefab()
    {
        Instantiate(DialogueTriggerPrefab, transform.position + prefabOffSet, transform.rotation);
    }

    public void ResetTime()
    {
        _currentTimeOn = 0;
    }

    private void AddCrystal()
    {

    }


}
