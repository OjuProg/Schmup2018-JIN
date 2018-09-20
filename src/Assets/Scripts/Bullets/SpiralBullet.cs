using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralBullet : Bullet
{
    public override void UpdatePosition()
    {
        transform.Translate(new Vector3(speed.x, speed.y, 0) * Time.deltaTime);
    }
}
