using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasicBulletGun : BulletGun {

    public new void Update()
    {
        cooldownChrono += Time.deltaTime;

        if (rotateFirePoint)
        {
            firePoint.transform.Rotate(Vector3.forward, 1);
        }

        if(canShoot)
        {
            Fire();
        }
    }
}
