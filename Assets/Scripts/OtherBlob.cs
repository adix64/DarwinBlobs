using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBlob : Blob
{
    Vector3 newDir = Vector3.forward;
    float newWait = 0.5f;
    public Color[] possibleColors;
    // Start is called before the first frame update
    void Start()
    {
        GetCommonComponents();
        var renderer = transform.GetChild(0).GetComponent<MeshRenderer>();
        int colorIndex = Random.Range(0, possibleColors.Length);
        renderer.materials[0].SetColor("_Color", possibleColors[colorIndex]);

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(possibleColors[colorIndex], 0.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(0f, 0.0f), new GradientAlphaKey(1f,.5f), new GradientAlphaKey(0f, 1.0f) }
        );


        trailRenderer.colorGradient = gradient;
        //trailRenderer.startColor = trailRenderer.endColor = possibleColors[colorIndex];
        StartCoroutine(ChangeDir(newWait));
    }
    IEnumerator ChangeDir (float t)
    {
        yield return new WaitForSeconds(t);
        newDir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        newWait = Random.Range(0.5f, 5f);
        yield return StartCoroutine(ChangeDir(newWait));
    }
    // Update is called once per frame
    void Update()
    {
        direction = Vector3.Slerp(direction, newDir, newWait * 3f * Time.deltaTime);
        transform.forward = direction;
        BlobUpdate();
    }
}
