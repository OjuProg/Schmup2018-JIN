using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

    public Vector2 position;
    public Vector2 speed;

    private BaseAvatar avatar;

    public void Start()
    {
        avatar = GetComponent<BaseAvatar>();
        position = transform.position;
    }

    public void Update()
    {
        position += speed * avatar.maxSpeed * Time.deltaTime;
        transform.position = position;
    }
}
