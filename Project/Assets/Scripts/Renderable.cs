using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renderable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		Material material = new(GetComponent<Renderer>().material)
		{
			renderQueue = 3000
		};
		GetComponent<Renderer>().material = material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
