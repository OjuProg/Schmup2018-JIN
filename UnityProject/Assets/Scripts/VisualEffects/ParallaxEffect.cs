using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxEffect : MonoBehaviour {

    [SerializeField]
	private List<MovingBackgroundElement> backgroundElements;

    void Update()
    {
        for(int index = 0; index < backgroundElements.Count; index++)
        {
            MovingBackgroundElement element = backgroundElements[index];
            if (element != null && element.backgroundElement != null)
            {
                Renderer renderer = element.backgroundElement.GetComponent<Renderer>();
                if(renderer != null)
                {
                    Vector2 offset = new Vector2(Time.time * element.speed, 0);
                    renderer.material.mainTextureOffset = offset;
                }
            }
        }
    }

}
