using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour {

    private float _checkpointGold;
    private Vector3 _checkpointPosition;
    //private Weapon[] _checkpointWeapons = new Weapon[2];
    private Weapon _checkpointWeapon;
    public int Index;

    public void SetPlayer(int weaponID)
    {
        PlayerSpawner.instance.transform.position = new Vector3(transform.position.x, PlayerSpawner.instance.transform.position.y, transform.position.z);
        PlayerSpawner.instance.StartSpawn(weaponID);
    }

	// Use this for initialization
	void Start () {
	}
	
    public void TakeCheckpoint(float pGold, Vector3 pPosition, Weapon pWeapon/*Weapon[] pWeapons*/)
    {
        if (AdditiveLevelManager.instance.HighestCheckpoint >= Index) return;
        _checkpointGold = pGold;
        _checkpointPosition = pPosition;
        //_checkpointWeapons = pWeapons;
        _checkpointWeapon = pWeapon;
        AdditiveLevelManager.instance.LastGold = _checkpointGold;
        AdditiveLevelManager.instance.HighestCheckpoint = Index;
        AdditiveLevelManager.instance._lastWeaponID = pWeapon.ID;


    }

    public float GetCheckpointGold()
    {
        return _checkpointGold;
    }

    public Vector3 GetCheckpointPos()
    {
        return _checkpointPosition;
    }

    public Weapon GetCheckpointWeapon()
    {
        return _checkpointWeapon;
    }

    

    // Update is called once per frame
    void Update () {
		
	}
}
