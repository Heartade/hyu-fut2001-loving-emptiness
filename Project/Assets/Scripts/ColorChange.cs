using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public GameObject trail;
    public GameObject drawPos;
    GameObject currentTrail;
    TrailRenderer controllerTrailRenderer;
    Color blue = new Color(152f / 255f, 218 / 255f, 1);
    Color pink = new Color(1, 198f / 255f, 198f / 255f);
    GradientColorKey[] blueGradient;
    GradientColorKey[] pinkGradient;
    GradientColorKey[] currentGradient;
    GradientAlphaKey[] drawAlphaKeys = new GradientAlphaKey[4];
    GradientAlphaKey[] trailAlphaKeys = new GradientAlphaKey[4];
    Color currentColor = Color.blue;
    Renderer rend;
    Material def;
    Material onHover;
    Material onSelect;
    bool isSelected = false;
    bool isHover = false;
    public float drawDist = 0;
    float currentDrawDist = 0;
    // Start is called before the first frame update
    void Start()
    {
        blueGradient = new GradientColorKey[] { new(blue, 0.0f), new(blue, 1.0f) };
        pinkGradient = new GradientColorKey[] { new(pink, 0.0f), new(pink, 1.0f) };
        drawAlphaKeys = new GradientAlphaKey[] { new(0.0f, 0.0f), new(1.0f, 0.1f), new(1.0f, 0.9f), new(0.0f, 1.0f) };
        trailAlphaKeys = new GradientAlphaKey[] { new(0.0f, 0.0f), new(0.8f, 0.1f), new(0.8f, 0.9f), new(0.0f, 1.0f) };
        
        currentColor = blue;
        currentGradient = blueGradient;

        controllerTrailRenderer = GetComponent<TrailRenderer>();
        controllerTrailRenderer.colorGradient = new ()
        {
            colorKeys = blueGradient,
            alphaKeys = trailAlphaKeys
        };

        rend = GetComponent<MeshRenderer>();
        def = rend.material;
		onHover = new (def)
		{
			color = Color.red
		};
		onSelect = new (def)
		{
			color = Color.green
		};
		//GetComponent<TrailRenderer>().time = Mathf.Infinity;
	}

    public void OnHoverEnter()
    {
        isHover = true;
    }

    public void OnHoverExit()
    {
		isHover = false;
	}

    public void OnSelectEnter()
    {
        isSelected = true;
        currentTrail = Instantiate(trail, transform.position, transform.rotation);
        TrailRenderer drawRenderer = currentTrail.GetComponent<TrailRenderer>();
		drawRenderer.colorGradient = new()
		{
			colorKeys = currentGradient,
			alphaKeys = drawAlphaKeys
		};
        drawRenderer.time = 60;
	}

    IEnumerator destroyTrail(GameObject trail, float trailDist)
    {
        yield return new WaitForSeconds(60);
        Destroy(trail);
        drawDist -= trailDist;
    }

    public void OnSelectExit()
    {
        StartCoroutine(destroyTrail(currentTrail, currentDrawDist));
        currentDrawDist = 0;
        if ( currentColor == blue ) {
            currentColor = pink;
            currentGradient = pinkGradient;
        }
        else {
            currentColor = blue;
            currentGradient = blueGradient;
        }
        controllerTrailRenderer.colorGradient = new()
        {
            alphaKeys = trailAlphaKeys,
            colorKeys = currentGradient
        };
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
			rend.material = onSelect;
            float dist = Vector3.Distance(drawPos.transform.position, currentTrail.transform.position);
            currentDrawDist += dist;
            drawDist += dist;
            currentTrail.transform.position = drawPos.transform.position;
		}
		else if (isHover)
        {
			rend.material = onHover;
		}
		else
        {
			rend.material = def;
		}
    }
}
