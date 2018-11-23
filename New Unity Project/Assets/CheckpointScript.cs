using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour {

    private float _checkpointGold;
    private Vector3 _checkpointPosition;
    private Weapon[] _checkpointWeapons = new Weapon[2];
    public int index;


	// Use this for initialization
	void Start () {
		
	}
	
    public void TakeCheckpoint(float pGold, Vector3 pPosition, Weapon[] pWeapons)
    {
        _checkpointGold = pGold;
        _checkpointPosition = pPosition;
        _checkpointWeapons = pWeapons;
    }

    public float GetCheckpointGold()
    {
        return _checkpointGold;
    }

    public Vector3 GetCheckpointPos()
    {
        return _checkpointPosition;
    }


// Update is called once per frame
void Update () {
		
	}
}
