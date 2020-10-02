using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAdjustLayerOrder : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sortingOrder += Random.Range(-3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
