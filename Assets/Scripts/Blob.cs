using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public float blobSize = 1f;
    public float baseSpeed = 1f;
    protected Rigidbody rigidbody;
    bool isDissolving = false;
    bool isEvolving = false;
    float timeSinceStartedEvolving = 0f;
    float timeSinceStartedDissolving = 0f;
    protected Vector3 direction;
	protected TrailRenderer trailRenderer;
	AnimationCurve initWidthCurve;
	float[] initWidthValues;
	float initTrailFadeTime;
    protected void GetCommonComponents()
	{
		rigidbody = GetComponent<Rigidbody>();
		transform.localScale = blobSize * Vector3.one;
		trailRenderer = GetComponent<TrailRenderer>();
		InitTrailRenderer();
	}

	private void InitTrailRenderer()
	{
		initWidthCurve = trailRenderer.widthCurve;
		initWidthValues = new float[trailRenderer.widthCurve.length];
		for (int i = 0; i < initWidthValues.Length; i++)
			initWidthValues[i] = initWidthCurve.keys[i].value;
		initTrailFadeTime = trailRenderer.time;
	}

	protected void BlobUpdate()
	{
		rigidbody.velocity = direction.normalized * (baseSpeed + blobSize);
		HandleDissolve();
		HandleEvolve();
		HandleTrailRenderer();
	}

	private void HandleTrailRenderer()
	{
		trailRenderer.widthMultiplier = transform.localScale.x * 2f;
		//trailRenderer.time = initTrailFadeTime * transform.localScale.x;
	}

	private void HandleDissolve()
	{
		if (!isDissolving)
			return;
		timeSinceStartedDissolving += Time.deltaTime;
		transform.localScale = Vector3.Lerp(transform.localScale,
											Vector3.zero,
											timeSinceStartedDissolving);
		if (timeSinceStartedDissolving > 1f)
		{
			isDissolving = false;
			Destroy(gameObject);
		}
	}
	private void HandleEvolve()
	{
		if (!isEvolving)
			return;
		timeSinceStartedEvolving += Time.deltaTime;
		transform.localScale = Vector3.Lerp(transform.localScale,
											Vector3.one * (blobSize + 0.5f),
											timeSinceStartedEvolving);
		if (timeSinceStartedEvolving > 1f)
		{
			isEvolving = false;
			blobSize += 0.5f;
			transform.localScale = blobSize * Vector3.one;
		}
	}
	protected void Dissolve()
    {
        isDissolving = true;
        timeSinceStartedDissolving = 0f;
    }
    protected void Evolve()
    {
        isEvolving = true;
        timeSinceStartedEvolving = 0f;
    }
    protected void HandleCollision(Collision collision)
	{
        var otherBlob = collision.gameObject.GetComponent<Blob>();
        if (otherBlob == null)
            return;
        if (otherBlob.blobSize < blobSize)
        {
            otherBlob.Dissolve();
            Evolve();
        }
	}
}
