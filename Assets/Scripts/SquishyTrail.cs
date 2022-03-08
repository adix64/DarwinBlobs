using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishyTrail : MonoBehaviour
{
    public Transform trailPivot;

    public float followSpeed = 0.1f;
    public float maxDistOriginal = 1.6f;
    public float rSpeed= 20f;
    MeshRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        trailPivot.parent = null;
    }
	private void OnDestroy()
	{
        Destroy(trailPivot.gameObject);
	}
	// Update is called once per frame
	void Update()
    {
        Vector3 toTrail = (transform.position - trailPivot.position);
        trailPivot.position += toTrail.normalized * Time.deltaTime * followSpeed;
        float maxDist = maxDistOriginal * transform.parent.localScale.x;
        if (toTrail.magnitude > maxDist)
			trailPivot.position = transform.position - toTrail.normalized * maxDist;

        //if(toTrail)
		renderer.materials[0].SetVector("_PrevPos", transform.InverseTransformPoint( trailPivot.position));
		renderer.materials[0].SetFloat("_BlobSize", transform.parent.localScale.x);
    }
}
