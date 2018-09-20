using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

    public Vector2 position;
    public Vector2 speed;

    private BaseAvatar avatar;

    // Player Boundaries
    private float minX, maxX, minY, maxY;

    public void Start()
    {
        // Getting the Avatar
        avatar = GetComponent<BaseAvatar>();

        if(avatar.GetType() == typeof(PlayerAvatar))
        {
            // Getting the camera boundaries
            float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
            float screenWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
            Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
            Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

            // Getting the image size
            float imageHeight = 0f;
            float imageWidth = 0f;

            /* DUDE TRY TO CORRECT THAT.
            Rect imageRectTransform = avatar.GetComponent<SpriteRenderer>().sprite.rect;
            Debug.Log((imageRectTransform == null).ToString());
            if (imageRectTransform != null)
            {
                imageHeight = imageRectTransform.height;
                imageWidth = imageRectTransform.width;
            }
            Debug.Log("Heigh: " + imageHeight.ToString() + "Width: " + imageWidth.ToString());
            */

            // Setting the boundaries
            minX = bottomCorner.x + imageWidth / 2;
            maxX = bottomCorner.x + screenWidth / 3 - imageWidth / 2;
            minY = bottomCorner.y + imageHeight / 2;
            maxY = topCorner.y - imageHeight / 2;

            transform.position = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, 0);
        }
        position = transform.position;
    }

    public void Update()
    {
        position += speed * avatar.maxSpeed * Time.deltaTime;

        // Check for camera boundaries.
        if (avatar.GetType() == typeof(PlayerAvatar))
        {
            if (position.x >= maxX)
            {
                position.x = maxX;
            }
            if (position.x <= minX)
            {
                position.x = minX;
            }
            if (position.y >= maxY)
            {
                position.y = maxY;
            }
            if (position.y <= minY)
            {
                position.y = minY;
            }
        }
        transform.position = position;
    }
}
