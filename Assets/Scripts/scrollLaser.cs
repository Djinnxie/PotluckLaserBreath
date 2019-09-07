using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollLaser : MonoBehaviour
{
    public float scrollSpeed = 2.5f;

    // Update is called once per frame
    void Update()
    {
        float offsetX = Time.time * - scrollSpeed;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(offsetX, 0);
    }
}
