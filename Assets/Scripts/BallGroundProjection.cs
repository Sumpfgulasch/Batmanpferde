using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGroundProjection : MonoBehaviour
{
    public Transform decal;
    Material decalMaterial;
    float minHeight = 0f;
    float maxHeight = 3f;
    int layerMask = 1 << 6;

    void Start()
    {
        if(decal != null)
        {
            decalMaterial = decal.GetComponent<MeshRenderer>().material;
        }
        else
        {
            Debug.Log("Assign a decalTransform");
        }
    }

    void Update()
    {
        if (this.transform.hasChanged)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity, layerMask))
            {
                decalMaterial.SetFloat("_heightBlend", remap(hit.distance, minHeight, maxHeight, 0f, 1f));
                decal.transform.position = new Vector3(transform.position.x, transform.position.y - hit.distance + 0.1f, transform.position.z);
            }
            this.transform.hasChanged = false;
        }
    }
    //map(value, low1, high1, low2, high2);
    float remap(float value, float from1, float from2, float to1, float to2)
    {
        return to1 + (value - from1) * (to2 - to1) / (from2 - from1);
    }
}
