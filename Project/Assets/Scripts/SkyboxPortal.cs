using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxPortal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Material material = new(GetComponent<Renderer>().material)
		{
			renderQueue = 2999
		};
		GetComponent<Renderer>().material = material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
