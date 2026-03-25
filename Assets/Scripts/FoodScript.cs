using UnityEngine;

public class FoodScript : MonoBehaviour
{
    private GameObject playerPunk;
    private Animator animPunk;
   

    public enum Food { Banana, Apple, Stub, JunkFood, Song }
    public Food type;

    private void Start()
    {
        playerPunk = GameObject.FindWithTag("Player");

        if (playerPunk != null)
        {
            animPunk = playerPunk.GetComponent<Animator>();
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player"))
        {
            PunkScript punk = collision.GetComponent<PunkScript>();
            switch (type)
            {
                case Food.Banana:
                case Food.Apple:
                    animPunk.SetTrigger("EatFood");
                    punk.HealthPlayer(5f);
                    Destroy(gameObject);
                    break;
                       
                case Food.Stub:
                case Food.JunkFood:
                    animPunk.SetTrigger("Intoxication");
                    punk.HealthPlayer(-30f);
                    Destroy(gameObject);
                    break;    
            }
        }
    }
}
