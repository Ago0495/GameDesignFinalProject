using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSort : MonoBehaviour
{
    public void Start()
    {
        sortLayers();
    }
    public void sortLayers()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = -1 * (int)(transform.position.y * 10);
    }
}
