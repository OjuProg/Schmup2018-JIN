using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemyBasicEngine : MonoBehaviour {

    public Engine engine;
    public Vector2 ennemiSpeed;

    public void Start()
    {
        engine = GetComponent<Engine>();
        engine.speed = ennemiSpeed;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
