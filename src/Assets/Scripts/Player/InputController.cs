using UnityEngine;

public class InputController : MonoBehaviour {

    public Engine engine;
    public BulletGun bulletGun;

    private float horizontalTranslation;
    private float verticalTranslation;

    // Update is called once per frame
    void Update () {
        horizontalTranslation = Input.GetAxis("Horizontal");
        verticalTranslation = Input.GetAxis("Vertical");

        engine.speed = new Vector2(horizontalTranslation, verticalTranslation);

        if(Input.GetKey(KeyCode.Space))
        {
            bulletGun.Fire();
        }
    }
}
