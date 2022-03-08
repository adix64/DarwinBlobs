using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    Camera camera;
    public PlayerControl player;
    public GameObject otherBlobPrefab;
    public Transform otherBlobsContainer;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        StartCoroutine(SpawnSomeOtherBlobs(1f));
    }
    private IEnumerator SpawnSomeOtherBlobs(float t)
    {
        yield return new WaitForSeconds(t);
        var playerViewportPos = camera.WorldToViewportPoint(player.transform.position);
        for (float x = -0.25f; x <= 1.25f; x += 0.25f)
        {
            for (float z = -0.25f; z <= 1.25f; z += 0.25f)
            {
                if (x >= 0f && x <= 1f || z >= 0f && z <= 1f) continue;
                Vector3 newPos = camera.ViewportToWorldPoint(new Vector3(x, z, playerViewportPos.z));
                newPos.y = player.transform.position.y;
                var go = Instantiate(otherBlobPrefab, newPos, Quaternion.identity, otherBlobsContainer);
                go.GetComponent<OtherBlob>().blobSize = Mathf.Max(1f, Random.Range((int)player.blobSize - 3f, player.blobSize + 3f));
            }
        }
        yield return StartCoroutine(SpawnSomeOtherBlobs(Random.Range(2f, 5f)));
    }
}
