using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject[] ballPrefabs;
    public GameObject[] spawnPoints;
    public GameObject splineBalls;
    public float startDelay, spawnInterval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("BallSpawning", startDelay, spawnInterval);
    }
    void BallSpawning()
    {
        int randomIndex = Random.Range(0, ballPrefabs.Length);

        Vector2 randomSpawn1 = new Vector2(Random.Range(-16.8f, -15.29f), Random.Range(-8f, 7.82f));
        Vector2 randomSpawn2 = new Vector2(Random.Range(15.29f, 16.8f), Random.Range(-8f, 7.82f));
        Vector2 randomSpawn3 = new Vector2(Random.Range(-14, 14f), Random.Range(8.76f, 9.15f));
        Vector2 randomSpawn4 = new Vector2(Random.Range(-14, 14f), Random.Range(-9.15f, -8.76f));

        //this line chooses one of the randomSpawns as the spawn location each time a ball is spawned. This one line would have been useful WEEKS ago when I was struggling with spawning! >:(
        Vector2 chosenSpawn = Random.value < 0.5f ? randomSpawn1 : randomSpawn2;
        Vector2 chosenSpawn2 = Random.value < 0.5f ? randomSpawn3 : randomSpawn4;
        Destroy(Instantiate(ballPrefabs[randomIndex], chosenSpawn, Quaternion.Euler(0, 0, Random.Range(0, 360))), 3.5f);

        if (Time.time > 30)
        {
            Destroy(Instantiate(ballPrefabs[randomIndex], chosenSpawn2, Quaternion.Euler(0, 0, Random.Range(0, 360))), 3.5f);
            splineBalls.SetActive(true);
        }
    }
}
