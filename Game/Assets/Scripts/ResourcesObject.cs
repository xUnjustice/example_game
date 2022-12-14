using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesObject : MonoBehaviour
{
    [SerializeField] private int resourceAmount;
    [SerializeField] private int amountHits;
    [SerializeField] private float hitSmoothness;
    [SerializeField] private float hitScale;

    private float targetScale;
    private int hits;
    // Start is called before the first frame update
    void Start()
    {
        targetScale =2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(
            Mathf.Lerp(transform.localScale.x, targetScale, Time.deltaTime * hitSmoothness),
            Mathf.Lerp(transform.localScale.y, targetScale, Time.deltaTime * hitSmoothness),
            Mathf.Lerp(transform.localScale.z, targetScale, Time.deltaTime * hitSmoothness)

            );

    }

    public int Collect()
    {
        hits++;
        transform.localScale = Vector3.one * hitScale;// (hitscale, hitscale,hitscale)
        if (hits == amountHits)
        {
            Destroy(gameObject, 1);
            targetScale = 0;
            return resourceAmount;
        }
        return 0;

    }
   
}
