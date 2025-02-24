using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] GameObject woodPlatform;
    [SerializeField] float spawnDelay;

    public void SetSpawn()
    {
        StartCoroutine(SpawnPlatforms());
    }
    IEnumerator SpawnPlatforms()
    {
        yield return new WaitForSeconds(spawnDelay);
        woodPlatform.SetActive(true);
    }

}
