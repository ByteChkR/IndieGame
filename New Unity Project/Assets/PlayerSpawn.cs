using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerSpawn : MonoBehaviour
{

    bool a = true;
    public string CameraIntroKey = "";
    // Use this for initialization
    void Start()
    {

        CameraController.instance.Load(CameraIntroKey);
        CameraViewLock.instance.start = false;
        if (AdditiveLevelManager.instance.HighestCheckpoint > 0)
            GameObject.FindObjectsOfType<CheckpointScript>().First(x => x.Index == AdditiveLevelManager.instance.HighestCheckpoint).SetPlayer(AdditiveLevelManager.instance._lastWeaponID);
        else
            PlayerSpawner.instance.StartSpawn(AdditiveLevelManager.instance._lastWeaponID);

    }

    private void FixedUpdate()
    {
        if (a && !CameraController.instance.start && PlayerSpawner.instance.Spawned)
        {

            Unit.Player.transform.SetPositionAndRotation(transform.position, transform.rotation);

            Unit.Player.ToggleUnitMovement(true);
            SpeakerSound.UseSpeaker3D = true;


            CameraViewLock.instance.start = true;
            

            a = false;
        }
    }

}
