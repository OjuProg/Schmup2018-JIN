using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBasicBulletGun : BulletGun {

    public void RandomizeEnemyShootingBehavior(float enemySpeed)
    {
        // Randomized values
        int bulletRandomization = Random.Range(0, 5);
        float cooldownRandomization = Random.Range(0.3f, 3f);

        // Randomizing the bullet selection.
        for(int i = 0; i < bulletRandomization; i++)
        {
            BulletRotation();
        }

        // Bullet value data
        currentBulletData.cooldown = cooldownRandomization;
        currentBulletData.speed.x = -(enemySpeed + 2);
        if(currentBulletType == BULLETTYPE.DIAGONALE)
        {
            currentBulletData.speed.y = currentBulletData.speed.x;
        }
        else
        {
            if(currentBulletType == BULLETTYPE.SPIRAL)
            {
                numberOfSpiralBullets = Random.Range(2, 6);
            }
            currentBulletData.speed.y = 0;
        }
    }

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
