using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : Ability {

    public float delayPerProjectile;
    public int projectileCount;
    public GameObject Projectile;
    public float DistanceBetweenProjectiles;
    public float StartDistance;
    float ActualDistance
    {
        get
        {
            return StartDistance + DistanceBetweenProjectiles * currentCount;
        }
    }
    int currentCount = 0;
    float timeInitialized;
	// Use this for initialization
	void Start () {
		
	}

    public override void Initialize(int source, Vector3 target)
    {
        base.Initialize(source, target);
        timeInitialized = Time.realtimeSinceStartup;

    }

    public override void Update()
    {
        base.Update();
    }

    public void FixedUpdate()
    {
        if (currentCount < (int)((Time.realtimeSinceStartup - timeInitialized) / delayPerProjectile))
        {
           
            Ability s = Instantiate(Projectile, transform.position, transform.rotation).GetComponent<Ability>();
            s.Initialize(Source.gameObject.GetInstanceID(), transform.position + transform.forward * ActualDistance);
            currentCount++;
        }
        if(currentCount >= projectileCount) Destroy(gameObject);
    }

}
