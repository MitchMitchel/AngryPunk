using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject[] food;
    [SerializeField] GameObject[] enemy;
    [SerializeField] float spawnDeelay = 3f;
    

    
    void Start()
    {
        InvokeRepeating("SpawnerFood", 0, spawnDeelay); 
    }

    
    void Update()
    {
        
    }
    void SpawnerFood()
    {
        int spawnIndex = Random.RandomRange(0, food.Length);
        float spawnPos = Random.RandomRange(-6.80f, 6.80f);
        Vector2 posSpawn = new Vector2(spawnPos, transform.position.y);
        Instantiate(food[spawnIndex],posSpawn , Quaternion.identity);

        
    }
    


}
